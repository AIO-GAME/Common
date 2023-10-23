using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIO.RainbowCore;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace AIO.RainbowFolders.Settings
{
    [CreateAssetMenu(menuName = "Plugins/Project Ruleset", fileName = nameof(ProjectRuleset))]
    [HelpURL("http://www.borodar.com/stuff/rainbowfolders/docs/quickstart_v2.1.0.pdf")]
    internal class ProjectRuleset : ScriptableObject
    {
        private const string RELATIVE_PATH = "Editor/Data/RainbowFoldersRuleset.asset";

        private const string DEVEL_PATH = "Assets/Devel/Editor/Data/RainbowFoldersRuleset.asset";

        private static readonly ProjectRule RECURSIVE_RULE = new ProjectRule(ProjectRule.KeyType.Name, string.Empty);

        [FormerlySerializedAs("Folders")] public List<ProjectRule> Rules;

        private readonly Dictionary<string, ProjectRule> _rulesByName = new Dictionary<string, ProjectRule>();

        private readonly List<ProjectRule> _rulesByNameRecursive = new List<ProjectRule>();

        private readonly Dictionary<string, ProjectRule> _rulesByPath = new Dictionary<string, ProjectRule>();

        private readonly List<ProjectRule> _rulesByPathRecursive = new List<ProjectRule>();

        public static Action OnRulesetChange;

        private static ProjectRuleset _instance;

        private static ICollection<string> paths;

        public static ProjectRuleset Instance
        {
            get
            {
                if (_instance is null)
                {
                    if (paths is null)
                    {
                        paths = new List<string>();
                        foreach (var guid in AssetDatabase.FindAssets($"t:{nameof(ProjectRuleset)}",
                                     new string[] { "Packages", "Assets" }))
                        {
                            paths.Add(AssetDatabase.GUIDToAssetPath(guid));
                        }
                    }

                    foreach (var expr in paths)
                    {
                        _instance = AssetDatabase.LoadAssetAtPath<ProjectRuleset>(expr);
                        if (_instance is null) continue;
                        _instance.UpdateOrdinals();
                        OnRulesetChange =
                            (Action)Delegate.Combine(OnRulesetChange, new Action(_instance.UpdateOrdinals));
                        _instance.UpdateDictionaries();
                        OnRulesetChange =
                            (Action)Delegate.Combine(OnRulesetChange, new Action(_instance.UpdateDictionaries));
                        return _instance;
                    }
                }

                if (_instance is null)
                {
                    _instance = CreateInstance<ProjectRuleset>();
                    AssetDatabase.CreateAsset(_instance, $"Assets/Editor/Gen/Settings/{nameof(ProjectRuleset)}.asset");
                }

                return _instance;
            }
        }

        private void OnDestroy()
        {
            OnRulesetChange = (Action)Delegate.Remove(OnRulesetChange, new Action(_instance.UpdateOrdinals));
            OnRulesetChange = (Action)Delegate.Remove(OnRulesetChange, new Action(_instance.UpdateDictionaries));
        }

        public static void ShowInspector(DefaultAsset asset = null)
        {
            Selection.activeObject = Instance;
            EditorApplication.delayCall = (EditorApplication.CallbackFunction)Delegate.Combine(
                EditorApplication.delayCall, (EditorApplication.CallbackFunction)delegate
                {
                    EditorApplication.delayCall = (EditorApplication.CallbackFunction)Delegate.Combine(
                        EditorApplication.delayCall, (EditorApplication.CallbackFunction)delegate
                        {
                            foreach (var EDITOR in ProjectRulesetEditor.EDITORS)
                            {
                                EDITOR.Asset = asset;
                                EDITOR.ForceUpdate = true;
                                EDITOR.SearchTab = 0;
                                EDITOR.Repaint();
                            }
                        });
                });
        }

        public ProjectRule GetRule(ProjectRule match)
        {
            if (IsNullOrEmpty(Rules) || match == null)
            {
                return null;
            }

            return Rules.Find(x => x.Type == match.Type && x.Key == match.Key);
        }

        public ProjectRule GetRuleByPath(string folderPath, bool allowRecursive = false)
        {
            if (IsNullOrEmpty(Rules))
            {
                return null;
            }

            var assetNameFromPath = GetAssetNameFromPath(folderPath);
            ProjectRule result = null;
            if (allowRecursive)
            {
                foreach (var item in _rulesByNameRecursive.Where(nameRecursive =>
                             folderPath.Contains(string.Concat("/", nameRecursive.Key, "/"))))
                {
                    ReplaceWithHighestPriority(ref result, item, recursive: true);
                }
            }

            _rulesByName.TryGetValue(assetNameFromPath, out var value);
            ReplaceWithHighestPriority(ref result, value);
            if (allowRecursive)
            {
                foreach (var item2 in _rulesByPathRecursive.Where(pathRecursive =>
                             folderPath.StartsWith(string.Concat(pathRecursive.Key, "/"))))
                {
                    ReplaceWithHighestPriority(ref result, item2, recursive: true);
                }
            }

            _rulesByPath.TryGetValue(folderPath, out var value2);
            ReplaceWithHighestPriority(ref result, value2);
            return result;
        }

        public void UpdateRule(ProjectRule match, ProjectRule value)
        {
            Undo.RecordObject(this, "Modify Rainbow Folder Settings");
            ProjectRule rule = GetRule(match);
            if (rule != null)
            {
                if (value.HasAtLeastOneTexture())
                {
                    rule.CopyFrom(value);
                    SaveSetting();
                }
                else
                {
                    RemoveAll(match);
                }
            }
            else if (value.HasAtLeastOneTexture())
            {
                AddRule(value);
            }
        }

        public void AddRule(ProjectRule value)
        {
            Rules.Add(new ProjectRule(value));
            SaveSetting();
        }

        public void RemoveAll(ProjectRule match)
        {
            if (match != null)
            {
                Undo.RecordObject(this, "Modify Rainbow Folder Settings");
                Rules.RemoveAll(x => x.Type == match.Type && x.Key == match.Key);
                SaveSetting();
            }
        }

        public void RemoveAllByPath(string path)
        {
            ProjectRule ruleByPath = GetRuleByPath(path);
            RemoveAll(ruleByPath);
        }

        public void ChangeRuleIcons(ProjectRule value)
        {
            Undo.RecordObject(this, "Modify Rainbow Folder Settings");
            var projectRule =
                Rules.SingleOrDefault(x => x.Type == value.Type && x.Key == value.Key);
            if (projectRule == null)
            {
                AddRule(new ProjectRule(value));
                return;
            }

            projectRule.IconType = value.IconType;
            projectRule.SmallIcon = value.SmallIcon;
            projectRule.LargeIcon = value.LargeIcon;
            SaveSetting();
        }

        public void ChangeRuleBackground(ProjectRule value)
        {
            Undo.RecordObject(this, "Modify Rainbow Folder Settings");
            var projectRule = Rules.SingleOrDefault(x => x.Type == value.Type && x.Key == value.Key);
            if (projectRule == null)
            {
                AddRule(new ProjectRule(value));
                return;
            }

            projectRule.BackgroundType = value.BackgroundType;
            projectRule.BackgroundTexture = value.BackgroundTexture;
            SaveSetting();
        }

        public void ChangeRuleIconsByPath(string path, ProjectIcon icon)
        {
            ChangeRuleIcons(new ProjectRule(ProjectRule.KeyType.Path, path, icon));
        }

        public void ChangeRuleBackgroundByPath(string path, CoreBackground background)
        {
            ChangeRuleBackground(new ProjectRule(ProjectRule.KeyType.Path, path, background));
        }

        private void UpdateOrdinals()
        {
            for (int i = 0; i < Rules.Count; i++)
            {
                Rules[i].Ordinal = i;
            }
        }

        private void UpdateDictionaries()
        {
            _rulesByName.Clear();
            _rulesByNameRecursive.Clear();
            _rulesByPath.Clear();
            _rulesByPathRecursive.Clear();
            foreach (ProjectRule rule in Rules)
            {
                switch (rule.Type)
                {
                    case ProjectRule.KeyType.Name:
                    {
                        if (_rulesByName.TryGetValue(rule.Key, out var value2))
                        {
                            ReplaceWithHighestPriority(ref value2, rule);
                            _rulesByName[rule.Key] = value2;
                        }
                        else
                        {
                            _rulesByName.Add(rule.Key, rule);
                        }

                        if (rule.IsRecursive())
                        {
                            _rulesByNameRecursive.Add(rule);
                        }

                        break;
                    }
                    case ProjectRule.KeyType.Path:
                    {
                        if (_rulesByPath.TryGetValue(rule.Key, out var value))
                        {
                            ReplaceWithHighestPriority(ref value, rule);
                            _rulesByPath[rule.Key] = value;
                        }
                        else
                        {
                            _rulesByPath.Add(rule.Key, rule);
                        }

                        if (rule.IsRecursive())
                        {
                            _rulesByPathRecursive.Add(rule);
                        }

                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void ReplaceWithHighestPriority(ref ProjectRule result, ProjectRule replacement,
            bool recursive = false)
        {
            if (result == null)
            {
                result = replacement;
            }
            else if (replacement != null && replacement.Priority >= result.Priority)
            {
                if (replacement.Priority == result.Priority)
                {
                    if (replacement.Ordinal > result.Ordinal)
                    {
                        result = replacement;
                    }
                }
                else
                {
                    result = replacement;
                }
            }

            if (recursive)
            {
                result = CopyRecursiveItem(result);
            }
        }

        private static ProjectRule CopyRecursiveItem(ProjectRule rule)
        {
            RECURSIVE_RULE.CopyFrom(rule);
            if (!rule.IsIconRecursive)
            {
                RECURSIVE_RULE.IconType = ProjectIcon.None;
            }

            if (!rule.IsBackgroundRecursive)
            {
                RECURSIVE_RULE.BackgroundType = CoreBackground.None;
            }

            return RECURSIVE_RULE;
        }

        private static string GetAssetNameFromPath(string path)
        {
            int startIndex = path.LastIndexOf("/", StringComparison.Ordinal) + 1;
            return path.Substring(startIndex);
        }

        private static bool IsNullOrEmpty(ICollection collection)
        {
            if (collection != null)
            {
                return collection.Count == 0;
            }

            return true;
        }

        private void SaveSetting()
        {
            EditorUtility.SetDirty(this);
            OnRulesetChange();
        }
    }
}