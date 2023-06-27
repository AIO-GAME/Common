using System;
using System.IO;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor.UI
{
    public class DOTweenUtilityWindow : EditorWindow
    {
        private struct LocationData
        {
            public string dir;

            public string filePath;

            public string adbFilePath;

            public string adbParentDir;

            public LocationData(string srcDir)
            {
                this = default(LocationData);
                dir = srcDir;
                filePath = string.Concat(dir, EditorUtils.pathSlash, "DOTweenSettings.asset");
                Console.WriteLine("{0} -> {1}", nameof(filePath), filePath);

                adbFilePath = EditorUtils.FullPathToADBPath(filePath);
                adbParentDir = EditorUtils.FullPathToADBPath(dir.Substring(0, dir.LastIndexOf(EditorUtils.pathSlash)));
            }
        }

        private const string _Title = "DOTween Utility Panel";

        private static readonly Vector2 _WinSize = new Vector2(370f, 650f);

        public const string Id = "DOTweenVersion";

        public const string IdPro = "DOTweenProVersion";

        private static readonly float _HalfBtSize = _WinSize.x * 0.5f - 6f;

        private bool _initialized;

        private DOTweenSettings _src;

        private Texture2D _headerImg;

        private Texture2D _footerImg;

        private Vector2 _headerSize;

        private Vector2 _footerSize;

        private string _innerTitle;

        private bool _setupRequired;

        private Vector2 _scrollVal;

        private int _selectedTab;

        private string[] _tabLabels = new string[2] { "Setup", "Preferences" };

        private string[] _settingsLocation = new string[3] { "Assets > Resources", "DOTween > Resources", "Demigiant > Resources" };

        [MenuItem("Tools/Demigiant/DOTween Utility Panel")]
        private static void ShowWindow()
        {
            Open();
        }

        public static void Open()
        {
            EditorUtils.RetrieveDependenciesData(force: true);
            DOTweenUtilityWindow window = GetWindow<DOTweenUtilityWindow>(utility: true, "DOTween Utility Panel", focus: true);
            window.minSize = _WinSize;
            window.maxSize = _WinSize;
            window.ShowUtility();
            EditorPrefs.SetString("DOTweenVersion", DOTween.Version);
            EditorPrefs.SetString("DOTweenProVersion", EditorUtils.proVersion);
        }

        private bool Init()
        {
            if (_initialized)
            {
                return true;
            }

            if (_headerImg == null)
            {
                var headerImgPath = string.Concat("Package", EditorUtils.editorADBDir, "Imgs/Header.jpg");
                _headerImg = AssetDatabase.LoadAssetAtPath(headerImgPath, typeof(Texture2D)) as Texture2D;
                if (_headerImg == null) return false;

                EditorUtils.SetEditorTexture(_headerImg, FilterMode.Bilinear, 512);
                _headerSize.x = _WinSize.x;
                _headerSize.y = (int)(_WinSize.x * (float)_headerImg.height / (float)_headerImg.width);
                _footerImg = AssetDatabase.LoadAssetAtPath(
                    string.Concat("Package", EditorUtils.editorADBDir, EditorGUIUtility.isProSkin ? "Imgs/Footer.png" : "Imgs/Footer_dark.png"),
                    typeof(Texture2D)) as Texture2D;
                EditorUtils.SetEditorTexture(_footerImg, FilterMode.Bilinear, 256);
                _footerSize.x = _WinSize.x;
                _footerSize.y = (int)(_WinSize.x * (float)_footerImg.height / (float)_footerImg.width);
            }

            _initialized = true;
            return true;
        }

        private void OnHierarchyChange()
        {
            Repaint();
        }

        private void OnEnable()
        {
            _innerTitle = string.Concat("DOTween v", DOTween.Version, TweenManager.isDebugBuild ? " [Debug build]" : " [Release build]");
            if (EditorUtils.hasPro)
            {
                _innerTitle = string.Concat(_innerTitle, "\nDOTweenPro v", EditorUtils.proVersion);
            }
            else
            {
                _innerTitle += "\nDOTweenPro not installed";
            }

            Init();
            _setupRequired = EditorUtils.DOTweenSetupRequired();
        }

        private void OnDestroy()
        {
            if (_src != null)
            {
                _src.modules.showPanel = false;
                EditorUtility.SetDirty(_src);
            }
        }

        private void OnGUI()
        {
            if (!Init())
            {
                GUILayout.Space(8f);
                GUILayout.Label("Completing import process...");
                return;
            }

            Connect();
            EditorGUIUtils.SetGUIStyles(_footerSize);
            if (Application.isPlaying)
            {
                GUILayout.Space(40f);
                GUILayout.BeginHorizontal();
                GUILayout.Space(40f);
                GUILayout.Label("DOTween Utility Panel\nis disabled while in Play Mode", EditorGUIUtils.wrapCenterLabelStyle, GUILayout.ExpandWidth(expand: true));
                GUILayout.Space(40f);
                GUILayout.EndHorizontal();
            }
            else
            {
                _scrollVal = GUILayout.BeginScrollView(_scrollVal);
                if (_src.modules.showPanel)
                {
                    if (DOTweenUtilityWindowModules.Draw(this, _src))
                    {
                        _setupRequired = EditorUtils.DOTweenSetupRequired();
                        _src.modules.showPanel = false;
                        EditorUtility.SetDirty(_src);
                    }
                }
                else
                {
                    Rect rect = new Rect(0f, 0f, _headerSize.x, 30f);
                    _selectedTab = GUI.Toolbar(rect, _selectedTab, _tabLabels);
                    if (_selectedTab == 1)
                    {
                        float labelWidth = EditorGUIUtility.labelWidth;
                        EditorGUIUtility.labelWidth = 160f;
                        DrawPreferencesGUI();
                        EditorGUIUtility.labelWidth = labelWidth;
                    }
                    else
                    {
                        DrawSetupGUI();
                    }
                }

                GUILayout.EndScrollView();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_src);
            }
        }

        private void DrawSetupGUI()
        {
            Rect rect = new Rect(0f, 30f, _headerSize.x, _headerSize.y);
            GUI.DrawTexture(rect, _headerImg, ScaleMode.StretchToFill, alphaBlend: false);
            GUILayout.Space(rect.y + _headerSize.y + 2f);
            GUILayout.Label(_innerTitle, TweenManager.isDebugBuild ? EditorGUIUtils.redLabelStyle : EditorGUIUtils.boldLabelStyle);
            if (_setupRequired)
            {
                GUI.backgroundColor = Color.red;
                GUILayout.BeginVertical(GUI.skin.box);
                GUILayout.Label("DOTWEEN SETUP REQUIRED", EditorGUIUtils.setupLabelStyle);
                GUILayout.EndVertical();
                GUI.backgroundColor = Color.white;
            }
            else
            {
                GUILayout.Space(8f);
            }

            GUI.color = Color.green;
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("<b>Setup DOTween...</b>\n(add/remove Modules)", EditorGUIUtils.btSetup, GUILayout.Width(200f)))
                {
                    DOTweenUtilityWindowModules.ApplyModulesSettings();
                    _src.modules.showPanel = true;
                    EditorUtility.SetDirty(_src);
                    EditorUtils.DeleteLegacyNoModulesDOTweenFiles();
                    DOTweenDefines.RemoveAllLegacyDefines();
                    EditorUtils.DeleteDOTweenUpgradeManagerFiles();
                    return;
                }

                GUILayout.FlexibleSpace();
            }

            GUI.color = Color.white;
            GUILayout.Space(4f);
            if (EditorUtils.hasDOTweenTimelineUnityPackage)
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("<b>Import DOTweenTimeline</b>\n<b>-[ EXPERIMENTAL ]-</b>\n(requires Unity 2018.4.24 or later)", EditorGUIUtils.btSetup, GUILayout.Width(200f)))
                    {
                        if (EditorVersion.MajorVersion < 2018 || EditorVersion.MinorVersion < 4)
                        {
                            EditorUtility.DisplayDialog("Import DOTweenTimeline", "Sorry, you need to be on Unity 2018.4 or later in order to import DOTweenTimeline.", "Ooops");
                        }
                        else if (EditorUtility.DisplayDialog("Import DOTweenTimeline",
                                     "DOTweenTimeline requires Unity 2018.4.24 or later. Do not import it if you're on earlier versions.\n\nProceed and import?", "Ok", "Cancel"))
                        {
                            AssetDatabase.ImportPackage(EditorUtils.dotweenTimelineUnityPackageFilePath, interactive: true);
                        }
                    }

                    GUILayout.FlexibleSpace();
                }

                GUILayout.Space(4f);
            }

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    GUI.color = (ASMDEFManager.hasModulesASMDEF ? Color.yellow : Color.cyan);
                    if (GUILayout.Button(ASMDEFManager.hasModulesASMDEF ? "Remove ASMDEF..." : "Create ASMDEF...", EditorGUIUtils.btSetup, GUILayout.Width(200f)))
                    {
                        if (ASMDEFManager.hasModulesASMDEF)
                        {
                            if (EditorUtility.DisplayDialog("Remove ASMDEF",
                                    $"This will remove the \"DOTween/Modules/DOTween.Modules.asmdef\" file (and if you have DOTween Pro also the \"DOTweenPro/DOTweenPro.Scripts.asmdef\" and \"DOTweenPro/Editor/DOTweenPro.EditorScripts.asmdef\" files)",
                                    "Ok", "Cancel"))
                            {
                                ASMDEFManager.RemoveAllASMDEF();
                            }
                        }
                        else if (EditorUtility.DisplayDialog("Create ASMDEF",
                                     $"This will create the \"DOTween/Modules/DOTween.Modules.asmdef\" file (and if you have DOTween Pro also the \"DOTweenPro/DOTweenPro.Scripts.asmdef\" and \"DOTweenPro/Editor/DOTweenPro.EditorScripts.asmdef\" files)",
                                     "Ok", "Cancel"))
                        {
                            ASMDEFManager.CreateAllASMDEF();
                        }
                    }

                    GUI.color = Color.white;
                    GUILayout.FlexibleSpace();
                }

                GUILayout.Label(
                    "ASMDEFs are useful if you need to reference the extra DOTween modules API (like [<i>UIelement</i>].DOColor) from other ASMDEFs/Libraries instead of loose scripts, but remember to have those <b>ASMDEFs/Libraries reference DOTween ones</b>.",
                    EditorGUIUtils.wordWrapRichTextLabelStyle);
            }

            GUILayout.Space(3f);
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Website", EditorGUIUtils.btBigStyle, GUILayout.Width(_HalfBtSize)))
                {
                    Application.OpenURL("http://dotween.demigiant.com/index.php");
                }

                if (GUILayout.Button("Get Started", EditorGUIUtils.btBigStyle, GUILayout.Width(_HalfBtSize)))
                {
                    Application.OpenURL("http://dotween.demigiant.com/getstarted.php");
                }
            }

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Documentation", EditorGUIUtils.btBigStyle, GUILayout.Width(_HalfBtSize)))
                {
                    Application.OpenURL("http://dotween.demigiant.com/documentation.php");
                }

                if (GUILayout.Button("Support", EditorGUIUtils.btBigStyle, GUILayout.Width(_HalfBtSize)))
                {
                    Application.OpenURL("http://dotween.demigiant.com/support.php");
                }
            }

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Changelog", EditorGUIUtils.btBigStyle, GUILayout.Width(_HalfBtSize)))
                {
                    Application.OpenURL("http://dotween.demigiant.com/download.php");
                }

                if (GUILayout.Button("Check Updates", EditorGUIUtils.btBigStyle, GUILayout.Width(_HalfBtSize)))
                {
                    Application.OpenURL(string.Concat("http://dotween.demigiant.com/download.php?v=", DOTween.Version));
                }
            }

            GUILayout.Space(4f);
            if (GUILayout.Button(_footerImg, EditorGUIUtils.btImgStyle))
            {
                Application.OpenURL("http://www.demigiant.com/");
            }
        }

        private void DrawPreferencesGUI()
        {
            GUILayout.Space(40f);
            if (GUILayout.Button("Reset", EditorGUIUtils.btBigStyle))
            {
                _src.useSafeMode = true;
                _src.safeModeOptions.nestedTweenFailureBehaviour = NestedTweenFailureBehaviour.TryToPreserveSequence;
                _src.showUnityEditorReport = false;
                _src.timeScale = 1f;
                _src.useSmoothDeltaTime = false;
                _src.maxSmoothUnscaledTime = 0.15f;
                _src.rewindCallbackMode = RewindCallbackMode.FireIfPositionChanged;
                _src.logBehaviour = LogBehaviour.ErrorsOnly;
                _src.drawGizmos = true;
                _src.defaultRecyclable = false;
                _src.defaultAutoPlay = AutoPlay.All;
                _src.defaultUpdateType = UpdateType.Normal;
                _src.defaultTimeScaleIndependent = false;
                _src.defaultEaseType = Ease.OutQuad;
                _src.defaultEaseOvershootOrAmplitude = 1.70158f;
                _src.defaultEasePeriod = 0f;
                _src.defaultAutoKill = true;
                _src.defaultLoopType = LoopType.Restart;
                _src.debugMode = false;
                _src.debugStoreTargetId = false;
                EditorUtility.SetDirty(_src);
            }

            GUILayout.Space(8f);
            _src.useSafeMode = EditorGUILayout.Toggle("Safe Mode", _src.useSafeMode);
            if (_src.useSafeMode)
            {
                _src.safeModeOptions.nestedTweenFailureBehaviour =
                    (NestedTweenFailureBehaviour)(object)EditorGUILayout.EnumPopup(new GUIContent("└ On Nested Tween Failure", "Behaviour in case a tween inside a Sequence fails"),
                        _src.safeModeOptions.nestedTweenFailureBehaviour);
            }

            _src.timeScale = EditorGUILayout.FloatField("DOTween's TimeScale", _src.timeScale);
            _src.useSmoothDeltaTime = EditorGUILayout.Toggle("Smooth DeltaTime", _src.useSmoothDeltaTime);
            _src.maxSmoothUnscaledTime = EditorGUILayout.Slider("Max SmoothUnscaledTime", _src.maxSmoothUnscaledTime, 0.01f, 1f);
            _src.rewindCallbackMode = (RewindCallbackMode)(object)EditorGUILayout.EnumPopup("OnRewind Callback Mode", _src.rewindCallbackMode);
            GUILayout.Space(-5f);
            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUIUtility.labelWidth + 4f);
            EditorGUILayout.HelpBox(
                (_src.rewindCallbackMode == RewindCallbackMode.FireIfPositionChanged)
                    ? "When calling Rewind or PlayBackwards/SmoothRewind, OnRewind callbacks will be fired only if the tween isn't already rewinded"
                    : ((_src.rewindCallbackMode == RewindCallbackMode.FireAlwaysWithRewind)
                        ? "When calling Rewind, OnRewind callbacks will always be fired, even if the tween is already rewinded."
                        : "When calling Rewind or PlayBackwards/SmoothRewind, OnRewind callbacks will always be fired, even if the tween is already rewinded"), MessageType.None);
            GUILayout.EndHorizontal();
            _src.showUnityEditorReport = EditorGUILayout.Toggle("Editor Report", _src.showUnityEditorReport);
            _src.logBehaviour = (LogBehaviour)(object)EditorGUILayout.EnumPopup("Log Behaviour", _src.logBehaviour);
            _src.drawGizmos = EditorGUILayout.Toggle("Draw Path Gizmos", _src.drawGizmos);
            DOTweenSettings.SettingsLocation storeSettingsLocation = _src.storeSettingsLocation;
            _src.storeSettingsLocation = (DOTweenSettings.SettingsLocation)EditorGUILayout.Popup("Settings Location", (int)_src.storeSettingsLocation, _settingsLocation);
            if (_src.storeSettingsLocation != storeSettingsLocation)
            {
                if (_src.storeSettingsLocation == DOTweenSettings.SettingsLocation.DemigiantDirectory && EditorUtils.demigiantDir == null)
                {
                    EditorUtility.DisplayDialog("Change DOTween Settings Location", "Demigiant directory not present (must be the parent of DOTween's directory)", "Ok");
                    if (storeSettingsLocation == DOTweenSettings.SettingsLocation.DemigiantDirectory)
                    {
                        _src.storeSettingsLocation = DOTweenSettings.SettingsLocation.AssetsDirectory;
                        Connect(forceReconnect: true);
                    }
                    else
                    {
                        _src.storeSettingsLocation = storeSettingsLocation;
                    }
                }
                else
                {
                    Connect(forceReconnect: true);
                }
            }

            GUILayout.Space(8f);
            GUILayout.Label("DEFAULTS ▼");
            _src.defaultRecyclable = EditorGUILayout.Toggle("Recycle Tweens", _src.defaultRecyclable);
            _src.defaultAutoPlay = (AutoPlay)(object)EditorGUILayout.EnumPopup("AutoPlay", _src.defaultAutoPlay);
            _src.defaultUpdateType = (UpdateType)(object)EditorGUILayout.EnumPopup("Update Type", _src.defaultUpdateType);
            _src.defaultTimeScaleIndependent = EditorGUILayout.Toggle("TimeScale Independent", _src.defaultTimeScaleIndependent);
            _src.defaultEaseType = (Ease)(object)EditorGUILayout.EnumPopup("Ease", _src.defaultEaseType);
            _src.defaultEaseOvershootOrAmplitude = EditorGUILayout.FloatField("Ease Overshoot", _src.defaultEaseOvershootOrAmplitude);
            _src.defaultEasePeriod = EditorGUILayout.FloatField("Ease Period", _src.defaultEasePeriod);
            _src.defaultAutoKill = EditorGUILayout.Toggle("AutoKill", _src.defaultAutoKill);
            _src.defaultLoopType = (LoopType)(object)EditorGUILayout.EnumPopup("Loop Type", _src.defaultLoopType);
            GUILayout.Space(8f);
            _src.debugMode = EditorGUIUtils.ToggleButton(_src.debugMode, new GUIContent("DEBUG MODE", "Turns debug mode options on/off"), true, null);
            if (_src.debugMode)
            {
                GUILayout.BeginVertical(GUI.skin.box);
                EditorGUI.BeginDisabledGroup(!_src.useSafeMode && _src.logBehaviour != LogBehaviour.ErrorsOnly);
                _src.debugStoreTargetId = EditorGUILayout.Toggle("Store GameObject's ID", _src.debugStoreTargetId);
                GUILayout.Label(
                    "<b>Requires Safe Mode to be active + Default or Verbose LogBehaviour:</b> when using DO shortcuts stores the relative gameObject's name so it can be returned along the warning logs (helps with a clearer identification of the warning's target)",
                    EditorGUIUtils.wordWrapRichTextLabelStyle);
                EditorGUI.EndDisabledGroup();
                GUILayout.EndVertical();
            }
        }

        public static DOTweenSettings GetDOTweenSettings()
        {
            return ConnectToSource(null, createIfMissing: false, fullSetup: false);
        }

        private static DOTweenSettings ConnectToSource(DOTweenSettings src, bool createIfMissing, bool fullSetup)
        {
            var locationData = new LocationData(string.Concat(EditorUtils.dotweenDir.Replace("\\Plugins", ""), "Resources"));
            var locationData2 = new LocationData(string.Concat(EditorUtils.assetsPath, EditorUtils.pathSlash, "Resources"));
            var flag = EditorUtils.demigiantDir != null;
            var locationData3 = (flag ? new LocationData(string.Concat(EditorUtils.demigiantDir.Replace("Plugins", ""), "Resources")) : default(LocationData));
            if (src == null)
            {
                src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(locationData.adbFilePath);
                if (src == null)
                {
                    src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(locationData2.adbFilePath);
                }

                if (src == null && flag)
                {
                    src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(locationData3.adbFilePath);
                }
            }

            if (src == null)
            {
                if (!createIfMissing)
                {
                    return null;
                }

                if (!Directory.Exists(locationData.dir))
                {
                    AssetDatabase.CreateFolder(locationData.adbParentDir, "Resources");
                }

                src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(locationData.adbFilePath, createIfMissing: true);
            }

            if (fullSetup)
            {
                switch (src.storeSettingsLocation)
                {
                    case DOTweenSettings.SettingsLocation.AssetsDirectory:
                        src = MoveSrc(new LocationData[2] { locationData2, locationData3 }, locationData);
                        break;
                    case DOTweenSettings.SettingsLocation.DOTweenDirectory:
                        src = MoveSrc(new LocationData[2] { locationData, locationData3 }, locationData2);
                        break;
                    case DOTweenSettings.SettingsLocation.DemigiantDirectory:
                        src = MoveSrc(new LocationData[2] { locationData, locationData2 }, locationData3);
                        break;
                }
            }

            return src;
        }

        private void Connect(bool forceReconnect = false)
        {
            if (!(_src != null) || forceReconnect)
            {
                _src = ConnectToSource(_src, createIfMissing: true, fullSetup: true);
            }
        }

        private static DOTweenSettings MoveSrc(LocationData[] from, LocationData to)
        {
            if (!Directory.Exists(to.dir))
            {
                AssetDatabase.CreateFolder(to.adbParentDir, "Resources");
            }

            for (int i = 0; i < from.Length; i++)
            {
                LocationData locationData = from[i];
                if (File.Exists(locationData.filePath))
                {
                    AssetDatabase.MoveAsset(locationData.adbFilePath, to.adbFilePath);
                    AssetDatabase.DeleteAsset(locationData.adbFilePath);
                    if (Directory.GetDirectories(locationData.dir).Length == 0 && Directory.GetFiles(locationData.dir).Length == 0)
                    {
                        AssetDatabase.DeleteAsset(EditorUtils.FullPathToADBPath(locationData.dir));
                    }
                }
            }

            return EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to.adbFilePath, createIfMissing: true);
        }
    }
}