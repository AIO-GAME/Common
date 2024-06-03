using AIO.RainbowFolders.Settings;
using UnityEditor;
using UnityEngine;

namespace AIO.RainbowFolders
{
	internal static class RainbowFoldersIconsMenu
	{
		private const string MENU_COLORIZE = "Colors/";

		private const string MENU_TRANSPARENT = "Transparent/";

		private const string MENU_TAG = "Tags/";

		private const string MENU_TYPE = "Types/";

		private const string MENU_PLATFORM = "Platforms/";

		private const string MENU_CUSTOM = "Custom";

		private const string MENU_NONE = "None";

		private static readonly GUIContent COLOR_RED = new GUIContent("Colors/Red");

		private static readonly GUIContent COLOR_VERMILION = new GUIContent("Colors/Vermilion");

		private static readonly GUIContent COLOR_ORANGE = new GUIContent("Colors/Orange");

		private static readonly GUIContent COLOR_AMBER = new GUIContent("Colors/Amber");

		private static readonly GUIContent COLOR_YELLOW = new GUIContent("Colors/Yellow");

		private static readonly GUIContent COLOR_LIME = new GUIContent("Colors/Lime");

		private static readonly GUIContent COLOR_CHARTREUSE = new GUIContent("Colors/Chartreuse");

		private static readonly GUIContent COLOR_HARLEQUIN = new GUIContent("Colors/Harlequin");

		private static readonly GUIContent COLOR_GREEN = new GUIContent("Colors/Green");

		private static readonly GUIContent COLOR_EMERALD = new GUIContent("Colors/Emerald");

		private static readonly GUIContent COLOR_SPRING_GREEN = new GUIContent("Colors/Spring-green");

		private static readonly GUIContent COLOR_AQUAMARINE = new GUIContent("Colors/Aquamarine");

		private static readonly GUIContent COLOR_CYAN = new GUIContent("Colors/Cyan");

		private static readonly GUIContent COLOR_SKY_BLUE = new GUIContent("Colors/Sky-blue");

		private static readonly GUIContent COLOR_AZURE = new GUIContent("Colors/Azure");

		private static readonly GUIContent COLOR_CERULEAN = new GUIContent("Colors/Cerulean");

		private static readonly GUIContent COLOR_BLUE = new GUIContent("Colors/Blue");

		private static readonly GUIContent COLOR_INDIGO = new GUIContent("Colors/Indigo");

		private static readonly GUIContent COLOR_VIOLET = new GUIContent("Colors/Violet");

		private static readonly GUIContent COLOR_PURPLE = new GUIContent("Colors/Purple");

		private static readonly GUIContent COLOR_MAGENTA = new GUIContent("Colors/Magenta");

		private static readonly GUIContent COLOR_FUCHSIA = new GUIContent("Colors/Fuchsia");

		private static readonly GUIContent COLOR_ROSE = new GUIContent("Colors/Rose");

		private static readonly GUIContent COLOR_CRIMSON = new GUIContent("Colors/Crimson");

		private static readonly GUIContent TRANSPARENT_15 = new GUIContent("Transparent/15%");

		private static readonly GUIContent TRANSPARENT_25 = new GUIContent("Transparent/25%");

		private static readonly GUIContent TRANSPARENT_50 = new GUIContent("Transparent/50%");

		private static readonly GUIContent TAG_RED = new GUIContent("Tags/Red");

		private static readonly GUIContent TAG_VERMILION = new GUIContent("Tags/Vermilion");

		private static readonly GUIContent TAG_ORANGE = new GUIContent("Tags/Orange");

		private static readonly GUIContent TAG_AMBER = new GUIContent("Tags/Amber");

		private static readonly GUIContent TAG_YELLOW = new GUIContent("Tags/Yellow");

		private static readonly GUIContent TAG_LIME = new GUIContent("Tags/Lime");

		private static readonly GUIContent TAG_CHARTREUSE = new GUIContent("Tags/Chartreuse");

		private static readonly GUIContent TAG_HARLEQUIN = new GUIContent("Tags/Harlequin");

		private static readonly GUIContent TAG_GREEN = new GUIContent("Tags/Green");

		private static readonly GUIContent TAG_EMERALD = new GUIContent("Tags/Emerald");

		private static readonly GUIContent TAG_SPRING_GREEN = new GUIContent("Tags/Spring-green");

		private static readonly GUIContent TAG_AQUAMARINE = new GUIContent("Tags/Aquamarine");

		private static readonly GUIContent TAG_CYAN = new GUIContent("Tags/Cyan");

		private static readonly GUIContent TAG_SKY_BLUE = new GUIContent("Tags/Sky-blue");

		private static readonly GUIContent TAG_AZURE = new GUIContent("Tags/Azure");

		private static readonly GUIContent TAG_CERULEAN = new GUIContent("Tags/Cerulean");

		private static readonly GUIContent TAG_BLUE = new GUIContent("Tags/Blue");

		private static readonly GUIContent TAG_INDIGO = new GUIContent("Tags/Indigo");

		private static readonly GUIContent TAG_VIOLET = new GUIContent("Tags/Violet");

		private static readonly GUIContent TAG_PURPLE = new GUIContent("Tags/Purple");

		private static readonly GUIContent TAG_MAGENTA = new GUIContent("Tags/Magenta");

		private static readonly GUIContent TAG_FUCHSIA = new GUIContent("Tags/Fuchsia");

		private static readonly GUIContent TAG_ROSE = new GUIContent("Tags/Rose");

		private static readonly GUIContent TAG_CRIMSON = new GUIContent("Tags/Crimson");

		private static readonly GUIContent TYPE_ANIMATIONS = new GUIContent("Types/Animations");

		private static readonly GUIContent TYPE_AUDIO = new GUIContent("Types/Audio");

		private static readonly GUIContent TYPE_PROJECT = new GUIContent("Types/Project");

		private static readonly GUIContent TYPE_EDITOR = new GUIContent("Types/Editor");

		private static readonly GUIContent TYPE_EXTENSIONS = new GUIContent("Types/Extensions");

		private static readonly GUIContent TYPE_FLARES = new GUIContent("Types/Flares");

		private static readonly GUIContent TYPE_FONTS = new GUIContent("Types/Fonts");

		private static readonly GUIContent TYPE_MATERIALS = new GUIContent("Types/Materials");

		private static readonly GUIContent TYPE_MESHES = new GUIContent("Types/Meshes");

		private static readonly GUIContent TYPE_PHYSICS = new GUIContent("Types/Physics");

		private static readonly GUIContent TYPE_PLUGINS = new GUIContent("Types/Plugins");

		private static readonly GUIContent TYPE_PREFABS = new GUIContent("Types/Prefabs");

		private static readonly GUIContent TYPE_RAINBOW = new GUIContent("Types/Rainbow");

		private static readonly GUIContent TYPE_RESOURCES = new GUIContent("Types/Resources");

		private static readonly GUIContent TYPE_SCENES = new GUIContent("Types/Scenes");

		private static readonly GUIContent TYPE_SCRIPTS = new GUIContent("Types/Scripts");

		private static readonly GUIContent TYPE_SHADERS = new GUIContent("Types/Shaders");

		private static readonly GUIContent TYPE_TERRAINS = new GUIContent("Types/Terrains");

		private static readonly GUIContent TYPE_TEXTURES = new GUIContent("Types/Textures");

		private static readonly GUIContent PLATFORM_ANDROID = new GUIContent("Platforms/Android");

		private static readonly GUIContent PLATFORM_IOS = new GUIContent("Platforms/iOS");

		private static readonly GUIContent PLATFORM_MAC = new GUIContent("Platforms/Mac");

		private static readonly GUIContent PLATFORM_WEBGL = new GUIContent("Platforms/WebGL");

		private static readonly GUIContent PLATFORM_WINDOWS = new GUIContent("Platforms/Windows");

		private static readonly GUIContent SELECT_CUSTOM = new GUIContent("Custom");

		private static readonly GUIContent SELECT_NONE = new GUIContent("None");

		public static void ShowDropDown(Rect position, object projectItem)
		{
			GenericMenu genericMenu = new GenericMenu();
			genericMenu.AddItem(COLOR_RED, on: false, RedCallback, projectItem);
			genericMenu.AddItem(COLOR_VERMILION, on: false, VermilionCallback, projectItem);
			genericMenu.AddItem(COLOR_ORANGE, on: false, OrangeCallback, projectItem);
			genericMenu.AddItem(COLOR_AMBER, on: false, AmberCallback, projectItem);
			genericMenu.AddItem(COLOR_YELLOW, on: false, YellowCallback, projectItem);
			genericMenu.AddItem(COLOR_LIME, on: false, LimeCallback, projectItem);
			genericMenu.AddItem(COLOR_CHARTREUSE, on: false, ChartreuseCallback, projectItem);
			genericMenu.AddItem(COLOR_HARLEQUIN, on: false, HarlequinCallback, projectItem);
			genericMenu.AddSeparator("Colors/");
			genericMenu.AddItem(COLOR_GREEN, on: false, GreenCallback, projectItem);
			genericMenu.AddItem(COLOR_EMERALD, on: false, EmeraldCallback, projectItem);
			genericMenu.AddItem(COLOR_SPRING_GREEN, on: false, SpringGreenCallback, projectItem);
			genericMenu.AddItem(COLOR_AQUAMARINE, on: false, AquamarineCallback, projectItem);
			genericMenu.AddItem(COLOR_CYAN, on: false, CyanCallback, projectItem);
			genericMenu.AddItem(COLOR_SKY_BLUE, on: false, SkyBlueCallback, projectItem);
			genericMenu.AddItem(COLOR_AZURE, on: false, AzureCallback, projectItem);
			genericMenu.AddItem(COLOR_CERULEAN, on: false, CeruleanCallback, projectItem);
			genericMenu.AddSeparator("Colors/");
			genericMenu.AddItem(COLOR_BLUE, on: false, BlueCallback, projectItem);
			genericMenu.AddItem(COLOR_INDIGO, on: false, IndigoCallback, projectItem);
			genericMenu.AddItem(COLOR_VIOLET, on: false, VioletCallback, projectItem);
			genericMenu.AddItem(COLOR_PURPLE, on: false, PurpleCallback, projectItem);
			genericMenu.AddItem(COLOR_MAGENTA, on: false, MagentaCallback, projectItem);
			genericMenu.AddItem(COLOR_FUCHSIA, on: false, FuchsiaCallback, projectItem);
			genericMenu.AddItem(COLOR_ROSE, on: false, RoseCallback, projectItem);
			genericMenu.AddItem(COLOR_CRIMSON, on: false, CrimsonCallback, projectItem);
			genericMenu.AddItem(TRANSPARENT_15, on: false, Transparent15Callback, projectItem);
			genericMenu.AddItem(TRANSPARENT_25, on: false, Transparent25Callback, projectItem);
			genericMenu.AddItem(TRANSPARENT_50, on: false, Transparent50Callback, projectItem);
			genericMenu.AddItem(TAG_RED, on: false, TagRedCallback, projectItem);
			genericMenu.AddItem(TAG_VERMILION, on: false, TagVermilionCallback, projectItem);
			genericMenu.AddItem(TAG_ORANGE, on: false, TagOrangeCallback, projectItem);
			genericMenu.AddItem(TAG_AMBER, on: false, TagAmberCallback, projectItem);
			genericMenu.AddItem(TAG_YELLOW, on: false, TagYellowCallback, projectItem);
			genericMenu.AddItem(TAG_LIME, on: false, TagLimeCallback, projectItem);
			genericMenu.AddItem(TAG_CHARTREUSE, on: false, TagChartreuseCallback, projectItem);
			genericMenu.AddItem(TAG_HARLEQUIN, on: false, TagHarlequinCallback, projectItem);
			genericMenu.AddSeparator("Tags/");
			genericMenu.AddItem(TAG_GREEN, on: false, TagGreenCallback, projectItem);
			genericMenu.AddItem(TAG_EMERALD, on: false, TagEmeraldCallback, projectItem);
			genericMenu.AddItem(TAG_SPRING_GREEN, on: false, TagSpringGreenCallback, projectItem);
			genericMenu.AddItem(TAG_AQUAMARINE, on: false, TagAquamarineCallback, projectItem);
			genericMenu.AddItem(TAG_CYAN, on: false, TagCyanCallback, projectItem);
			genericMenu.AddItem(TAG_SKY_BLUE, on: false, TagSkyBlueCallback, projectItem);
			genericMenu.AddItem(TAG_AZURE, on: false, TagAzureCallback, projectItem);
			genericMenu.AddItem(TAG_CERULEAN, on: false, TagCeruleanCallback, projectItem);
			genericMenu.AddSeparator("Tags/");
			genericMenu.AddItem(TAG_BLUE, on: false, TagBlueCallback, projectItem);
			genericMenu.AddItem(TAG_INDIGO, on: false, TagIndigoCallback, projectItem);
			genericMenu.AddItem(TAG_VIOLET, on: false, TagVioletCallback, projectItem);
			genericMenu.AddItem(TAG_PURPLE, on: false, TagPurpleCallback, projectItem);
			genericMenu.AddItem(TAG_MAGENTA, on: false, TagMagentaCallback, projectItem);
			genericMenu.AddItem(TAG_FUCHSIA, on: false, TagFuchsiaCallback, projectItem);
			genericMenu.AddItem(TAG_ROSE, on: false, TagRoseCallback, projectItem);
			genericMenu.AddItem(TAG_CRIMSON, on: false, TagCrimsonCallback, projectItem);
			genericMenu.AddItem(TYPE_ANIMATIONS, on: false, AnimationsCallback, projectItem);
			genericMenu.AddItem(TYPE_AUDIO, on: false, AudioCallback, projectItem);
			genericMenu.AddItem(TYPE_EDITOR, on: false, EditorCallback, projectItem);
			genericMenu.AddItem(TYPE_EXTENSIONS, on: false, ExtensionsCallback, projectItem);
			genericMenu.AddItem(TYPE_FLARES, on: false, FlaresCallback, projectItem);
			genericMenu.AddItem(TYPE_FONTS, on: false, FontsCallback, projectItem);
			genericMenu.AddItem(TYPE_MATERIALS, on: false, MaterialsCallback, projectItem);
			genericMenu.AddItem(TYPE_MESHES, on: false, MeshesCallback, projectItem);
			genericMenu.AddItem(TYPE_PLUGINS, on: false, PluginsCallback, projectItem);
			genericMenu.AddItem(TYPE_PHYSICS, on: false, PhysicsCallback, projectItem);
			genericMenu.AddItem(TYPE_PREFABS, on: false, PrefabsCallback, projectItem);
			genericMenu.AddItem(TYPE_PROJECT, on: false, ProjectCallback, projectItem);
			genericMenu.AddItem(TYPE_RAINBOW, on: false, RainbowCallback, projectItem);
			genericMenu.AddItem(TYPE_RESOURCES, on: false, ResourcesCallback, projectItem);
			genericMenu.AddItem(TYPE_SCENES, on: false, ScenesCallback, projectItem);
			genericMenu.AddItem(TYPE_SCRIPTS, on: false, ScriptsCallback, projectItem);
			genericMenu.AddItem(TYPE_SHADERS, on: false, ShadersCallback, projectItem);
			genericMenu.AddItem(TYPE_TERRAINS, on: false, TerrainsCallback, projectItem);
			genericMenu.AddItem(TYPE_TEXTURES, on: false, TexturesCallback, projectItem);
			genericMenu.AddItem(PLATFORM_ANDROID, on: false, AndroidCallback, projectItem);
			genericMenu.AddItem(PLATFORM_IOS, on: false, IosCallback, projectItem);
			genericMenu.AddItem(PLATFORM_MAC, on: false, MacCallback, projectItem);
			genericMenu.AddItem(PLATFORM_WEBGL, on: false, WebGLCallback, projectItem);
			genericMenu.AddItem(PLATFORM_WINDOWS, on: false, WindowsCallback, projectItem);
			genericMenu.AddSeparator(string.Empty);
			genericMenu.AddItem(SELECT_CUSTOM, on: false, SelectCustomCallback, projectItem);
			genericMenu.AddItem(SELECT_NONE, on: false, SelectNoneCallback, projectItem);
			genericMenu.DropDown(position);
		}

		private static void AssignIconByType(ProjectIcon type, object item)
		{
			if (IsSerializedProperty(item))
			{
				AssignIconByType(type, item as SerializedProperty);
			}
			else
			{
				AssignIconByType(type, item as ProjectRule);
			}
		}

		private static void AssignIconByType(ProjectIcon type, ProjectRule rule)
		{
			rule.IconType = type;
			rule.SmallIcon = null;
			rule.LargeIcon = null;
		}

		private static void AssignIconByType(ProjectIcon type, SerializedProperty projectItem)
		{
			projectItem.FindPropertyRelative("IconType").intValue = (int)type;
			projectItem.FindPropertyRelative("SmallIcon").objectReferenceValue = null;
			projectItem.FindPropertyRelative("LargeIcon").objectReferenceValue = null;
			ApplyModifiedProperties(projectItem);
		}

		private static void SelectCustom(ProjectRule rule)
		{
			rule.IconType = ProjectIcon.Custom;
			rule.SmallIcon = null;
			rule.LargeIcon = null;
		}

		private static void SelectCustom(SerializedProperty projectItem)
		{
			projectItem.FindPropertyRelative("IconType").intValue = 1;
			projectItem.FindPropertyRelative("SmallIcon").objectReferenceValue = null;
			projectItem.FindPropertyRelative("LargeIcon").objectReferenceValue = null;
			ApplyModifiedProperties(projectItem);
		}

		private static void SelectNone(ProjectRule folder)
		{
			folder.IconType = ProjectIcon.None;
			folder.SmallIcon = null;
			folder.LargeIcon = null;
			folder.IsIconRecursive = false;
		}

		private static void SelectNone(SerializedProperty projectItem)
		{
			projectItem.FindPropertyRelative("IconType").intValue = 0;
			projectItem.FindPropertyRelative("SmallIcon").objectReferenceValue = null;
			projectItem.FindPropertyRelative("LargeIcon").objectReferenceValue = null;
			projectItem.FindPropertyRelative("IsIconRecursive").boolValue = false;
			ApplyModifiedProperties(projectItem);
		}

		private static void ApplyModifiedProperties(SerializedProperty projectItem)
		{
			projectItem.serializedObject.ApplyModifiedProperties();
			ProjectRuleset.OnRulesetChange();
		}

		private static bool IsSerializedProperty(object item)
		{
			return item.GetType() == typeof(SerializedProperty);
		}

		private static void RedCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrRed, folder);
		}

		private static void VermilionCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrVermilion, folder);
		}

		private static void OrangeCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrOrange, folder);
		}

		private static void AmberCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrAmber, folder);
		}

		private static void YellowCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrYellow, folder);
		}

		private static void LimeCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrLime, folder);
		}

		private static void ChartreuseCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrChartreuse, folder);
		}

		private static void HarlequinCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrHarlequin, folder);
		}

		private static void GreenCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrGreen, folder);
		}

		private static void EmeraldCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrEmerald, folder);
		}

		private static void SpringGreenCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrSpringGreen, folder);
		}

		private static void AquamarineCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrAquamarine, folder);
		}

		private static void CyanCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrCyan, folder);
		}

		private static void SkyBlueCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrSkyBlue, folder);
		}

		private static void AzureCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrAzure, folder);
		}

		private static void CeruleanCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrCerulean, folder);
		}

		private static void BlueCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrBlue, folder);
		}

		private static void IndigoCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrIndigo, folder);
		}

		private static void VioletCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrViolet, folder);
		}

		private static void PurpleCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrPurple, folder);
		}

		private static void MagentaCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrMagenta, folder);
		}

		private static void FuchsiaCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrFuchsia, folder);
		}

		private static void RoseCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrRose, folder);
		}

		private static void CrimsonCallback(object folder)
		{
			AssignIconByType(ProjectIcon.ClrCrimson, folder);
		}

		private static void Transparent15Callback(object folder)
		{
			AssignIconByType(ProjectIcon.Transparent15, folder);
		}

		private static void Transparent25Callback(object folder)
		{
			AssignIconByType(ProjectIcon.Transparent25, folder);
		}

		private static void Transparent50Callback(object folder)
		{
			AssignIconByType(ProjectIcon.Transparent50, folder);
		}

		private static void TagRedCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagRed, folder);
		}

		private static void TagVermilionCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagVermilion, folder);
		}

		private static void TagOrangeCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagOrange, folder);
		}

		private static void TagAmberCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagAmber, folder);
		}

		private static void TagYellowCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagYellow, folder);
		}

		private static void TagLimeCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagLime, folder);
		}

		private static void TagChartreuseCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagChartreuse, folder);
		}

		private static void TagHarlequinCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagHarlequin, folder);
		}

		private static void TagGreenCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagGreen, folder);
		}

		private static void TagEmeraldCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagEmerald, folder);
		}

		private static void TagSpringGreenCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagSpringGreen, folder);
		}

		private static void TagAquamarineCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagAquamarine, folder);
		}

		private static void TagCyanCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagCyan, folder);
		}

		private static void TagSkyBlueCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagSkyBlue, folder);
		}

		private static void TagAzureCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagAzure, folder);
		}

		private static void TagCeruleanCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagCerulean, folder);
		}

		private static void TagBlueCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagBlue, folder);
		}

		private static void TagIndigoCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagIndigo, folder);
		}

		private static void TagVioletCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagViolet, folder);
		}

		private static void TagPurpleCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagPurple, folder);
		}

		private static void TagMagentaCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagMagenta, folder);
		}

		private static void TagFuchsiaCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagFuchsia, folder);
		}

		private static void TagRoseCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagRose, folder);
		}

		private static void TagCrimsonCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TagCrimson, folder);
		}

		private static void AnimationsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeAnimations, folder);
		}

		private static void PhysicsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpePhysics, folder);
		}

		private static void PrefabsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpePrefabs, folder);
		}

		private static void ScenesCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeScenes, folder);
		}

		private static void ScriptsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeScripts, folder);
		}

		private static void ExtensionsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeExtensions, folder);
		}

		private static void FlaresCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeFlares, folder);
		}

		private static void PluginsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpePlugins, folder);
		}

		private static void TexturesCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeTextures, folder);
		}

		private static void MaterialsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeMaterials, folder);
		}

		private static void AudioCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeAudio, folder);
		}

		private static void ProjectCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeProject, folder);
		}

		private static void FontsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeFonts, folder);
		}

		private static void EditorCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeEditor, folder);
		}

		private static void ResourcesCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeResources, folder);
		}

		private static void ShadersCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeShaders, folder);
		}

		private static void TerrainsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeTerrains, folder);
		}

		private static void MeshesCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeMeshes, folder);
		}

		private static void RainbowCallback(object folder)
		{
			AssignIconByType(ProjectIcon.TpeRainbow, folder);
		}

		private static void AndroidCallback(object folder)
		{
			AssignIconByType(ProjectIcon.PfmAndroid, folder);
		}

		private static void IosCallback(object folder)
		{
			AssignIconByType(ProjectIcon.PfmiOS, folder);
		}

		private static void MacCallback(object folder)
		{
			AssignIconByType(ProjectIcon.PfmMac, folder);
		}

		private static void WebGLCallback(object folder)
		{
			AssignIconByType(ProjectIcon.PfmWebGL, folder);
		}

		private static void WindowsCallback(object folder)
		{
			AssignIconByType(ProjectIcon.PfmWindows, folder);
		}

		private static void SelectCustomCallback(object item)
		{
			if (IsSerializedProperty(item))
			{
				SelectCustom(item as SerializedProperty);
			}
			else
			{
				SelectCustom(item as ProjectRule);
			}
		}

		private static void SelectNoneCallback(object item)
		{
			if (IsSerializedProperty(item))
			{
				SelectNone(item as SerializedProperty);
			}
			else
			{
				SelectNone(item as ProjectRule);
			}
		}
	}
}
