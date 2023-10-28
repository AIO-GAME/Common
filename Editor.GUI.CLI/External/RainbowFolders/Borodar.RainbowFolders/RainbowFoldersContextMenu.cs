using System.Linq;
using AIO.RainbowCore;
using AIO.RainbowFolders.Settings;
using UnityEditor;
using UnityEngine;

namespace AIO.RainbowFolders
{
	internal static class RainbowFoldersContextMenu
	{
		private const string MENU_BASE = "Assets/Rainbow Folders/";

		private const string ITEM_CUSTOM = "Assets/Rainbow Folders/Apply Custom";

		private const string ITEM_DEFAULT = "Assets/Rainbow Folders/Revert to Default";

		private const string ITEM_RULES_ALL = "Assets/Rainbow Folders/All Rules";

		private const string ITEM_RULES_FOLDER = "Assets/Rainbow Folders/Folder Rules";

		private const string MENU_COLOR = "Assets/Rainbow Folders/Color/";

		private const string MENU_TRANSPARENT = "Assets/Rainbow Folders/Transparent/";

		private const string MENU_TAG = "Assets/Rainbow Folders/Tag/";

		private const string MENU_TYPE = "Assets/Rainbow Folders/Type/";

		private const string MENU_PLATFORM = "Assets/Rainbow Folders/Platform/";

		private const string MENU_BACKGROUND = "Assets/Rainbow Folders/Background/";

		private const string COLOR_RED = "Assets/Rainbow Folders/Color/Red";

		private const string COLOR_VERMILION = "Assets/Rainbow Folders/Color/Vermilion";

		private const string COLOR_ORANGE = "Assets/Rainbow Folders/Color/Orange";

		private const string COLOR_AMBER = "Assets/Rainbow Folders/Color/Amber";

		private const string COLOR_YELLOW = "Assets/Rainbow Folders/Color/Yellow";

		private const string COLOR_LIME = "Assets/Rainbow Folders/Color/Lime";

		private const string COLOR_CHARTREUSE = "Assets/Rainbow Folders/Color/Chartreuse";

		private const string COLOR_HARLEQUIN = "Assets/Rainbow Folders/Color/Harlequin";

		private const string COLOR_GREEN = "Assets/Rainbow Folders/Color/Green";

		private const string COLOR_EMERALD = "Assets/Rainbow Folders/Color/Emerald";

		private const string COLOR_SPRING_GREEN = "Assets/Rainbow Folders/Color/Spring-green";

		private const string COLOR_AQUAMARINE = "Assets/Rainbow Folders/Color/Aquamarine";

		private const string COLOR_CYAN = "Assets/Rainbow Folders/Color/Cyan";

		private const string COLOR_SKY_BLUE = "Assets/Rainbow Folders/Color/Sky-blue";

		private const string COLOR_AZURE = "Assets/Rainbow Folders/Color/Azure";

		private const string COLOR_CERULEAN = "Assets/Rainbow Folders/Color/Cerulean";

		private const string COLOR_BLUE = "Assets/Rainbow Folders/Color/Blue";

		private const string COLOR_INDIGO = "Assets/Rainbow Folders/Color/Indigo";

		private const string COLOR_VIOLET = "Assets/Rainbow Folders/Color/Violet";

		private const string COLOR_PURPLE = "Assets/Rainbow Folders/Color/Purple";

		private const string COLOR_MAGENTA = "Assets/Rainbow Folders/Color/Magenta";

		private const string COLOR_FUCHSIA = "Assets/Rainbow Folders/Color/Fuchsia";

		private const string COLOR_ROSE = "Assets/Rainbow Folders/Color/Rose";

		private const string COLOR_CRIMSON = "Assets/Rainbow Folders/Color/Crimson";

		private const string TRANSPARENT_15 = "Assets/Rainbow Folders/Transparent/15%";

		private const string TRANSPARENT_25 = "Assets/Rainbow Folders/Transparent/25%";

		private const string TRANSPARENT_50 = "Assets/Rainbow Folders/Transparent/50%";

		private const string TAG_RED = "Assets/Rainbow Folders/Tag/Red";

		private const string TAG_VERMILION = "Assets/Rainbow Folders/Tag/Vermilion";

		private const string TAG_ORANGE = "Assets/Rainbow Folders/Tag/Orange";

		private const string TAG_AMBER = "Assets/Rainbow Folders/Tag/Amber";

		private const string TAG_YELLOW = "Assets/Rainbow Folders/Tag/Yellow";

		private const string TAG_LIME = "Assets/Rainbow Folders/Tag/Lime";

		private const string TAG_CHARTREUSE = "Assets/Rainbow Folders/Tag/Chartreuse";

		private const string TAG_HARLEQUIN = "Assets/Rainbow Folders/Tag/Harlequin";

		private const string TAG_GREEN = "Assets/Rainbow Folders/Tag/Green";

		private const string TAG_EMERALD = "Assets/Rainbow Folders/Tag/Emerald";

		private const string TAG_SPRING_GREEN = "Assets/Rainbow Folders/Tag/Spring-green";

		private const string TAG_AQUAMARINE = "Assets/Rainbow Folders/Tag/Aquamarine";

		private const string TAG_CYAN = "Assets/Rainbow Folders/Tag/Cyan";

		private const string TAG_SKY_BLUE = "Assets/Rainbow Folders/Tag/Sky-blue";

		private const string TAG_AZURE = "Assets/Rainbow Folders/Tag/Azure";

		private const string TAG_CERULEAN = "Assets/Rainbow Folders/Tag/Cerulean";

		private const string TAG_BLUE = "Assets/Rainbow Folders/Tag/Blue";

		private const string TAG_INDIGO = "Assets/Rainbow Folders/Tag/Indigo";

		private const string TAG_VIOLET = "Assets/Rainbow Folders/Tag/Violet";

		private const string TAG_PURPLE = "Assets/Rainbow Folders/Tag/Purple";

		private const string TAG_MAGENTA = "Assets/Rainbow Folders/Tag/Magenta";

		private const string TAG_FUCHSIA = "Assets/Rainbow Folders/Tag/Fuchsia";

		private const string TAG_ROSE = "Assets/Rainbow Folders/Tag/Rose";

		private const string TAG_CRIMSON = "Assets/Rainbow Folders/Tag/Crimson";

		private const string TYPE_PREFABS = "Assets/Rainbow Folders/Type/Prefabs";

		private const string TYPE_SCENES = "Assets/Rainbow Folders/Type/Scenes";

		private const string TYPE_SCRIPTS = "Assets/Rainbow Folders/Type/Scripts";

		private const string TYPE_EXTENSIONS = "Assets/Rainbow Folders/Type/Extensions";

		private const string TYPE_FLARES = "Assets/Rainbow Folders/Type/Flares";

		private const string TYPE_PLUGINS = "Assets/Rainbow Folders/Type/Plugins";

		private const string TYPE_TEXTURES = "Assets/Rainbow Folders/Type/Textures";

		private const string TYPE_MATERIALS = "Assets/Rainbow Folders/Type/Materials";

		private const string TYPE_AUDIO = "Assets/Rainbow Folders/Type/Audio";

		private const string TYPE_PROJECT = "Assets/Rainbow Folders/Type/Project";

		private const string TYPE_FONTS = "Assets/Rainbow Folders/Type/Fonts";

		private const string TYPE_EDITOR = "Assets/Rainbow Folders/Type/Editor";

		private const string TYPE_RESOURCES = "Assets/Rainbow Folders/Type/Resources";

		private const string TYPE_SHADERS = "Assets/Rainbow Folders/Type/Shaders";

		private const string TYPE_TERRAINS = "Assets/Rainbow Folders/Type/Terrains";

		private const string TYPE_MESHES = "Assets/Rainbow Folders/Type/Meshes";

		private const string TYPE_RAINBOW = "Assets/Rainbow Folders/Type/Rainbow";

		private const string TYPE_ANIMATIONS = "Assets/Rainbow Folders/Type/Animations";

		private const string TYPE_PHYSICS = "Assets/Rainbow Folders/Type/Physics";

		private const string PLATFORM_ANDROID = "Assets/Rainbow Folders/Platform/Android";

		private const string PLATFORM_IOS = "Assets/Rainbow Folders/Platform/iOS";

		private const string PLATFORM_MAC = "Assets/Rainbow Folders/Platform/Mac";

		private const string PLATFORM_WEBGL = "Assets/Rainbow Folders/Platform/WebGL";

		private const string PLATFORM_WINDOWS = "Assets/Rainbow Folders/Platform/Windows";

		private const string BACKGROUND_RED = "Assets/Rainbow Folders/Background/Red";

		private const string BACKGROUND_VERMILION = "Assets/Rainbow Folders/Background/Vermilion";

		private const string BACKGROUND_ORANGE = "Assets/Rainbow Folders/Background/Orange";

		private const string BACKGROUND_AMBER = "Assets/Rainbow Folders/Background/Amber";

		private const string BACKGROUND_YELLOW = "Assets/Rainbow Folders/Background/Yellow";

		private const string BACKGROUND_LIME = "Assets/Rainbow Folders/Background/Lime";

		private const string BACKGROUND_CHARTREUSE = "Assets/Rainbow Folders/Background/Chartreuse";

		private const string BACKGROUND_HARLEQUIN = "Assets/Rainbow Folders/Background/Harlequin";

		private const string BACKGROUND_GREEN = "Assets/Rainbow Folders/Background/Green";

		private const string BACKGROUND_EMERALD = "Assets/Rainbow Folders/Background/Emerald";

		private const string BACKGROUND_SPRING_GREEN = "Assets/Rainbow Folders/Background/Spring-green";

		private const string BACKGROUND_AQUAMARINE = "Assets/Rainbow Folders/Background/Aquamarine";

		private const string BACKGROUND_CYAN = "Assets/Rainbow Folders/Background/Cyan";

		private const string BACKGROUND_SKY_BLUE = "Assets/Rainbow Folders/Background/Sky-blue";

		private const string BACKGROUND_AZURE = "Assets/Rainbow Folders/Background/Azure";

		private const string BACKGROUND_CERULEAN = "Assets/Rainbow Folders/Background/Cerulean";

		private const string BACKGROUND_BLUE = "Assets/Rainbow Folders/Background/Blue";

		private const string BACKGROUND_INDIGO = "Assets/Rainbow Folders/Background/Indigo";

		private const string BACKGROUND_VIOLET = "Assets/Rainbow Folders/Background/Violet";

		private const string BACKGROUND_PURPLE = "Assets/Rainbow Folders/Background/Purple";

		private const string BACKGROUND_MAGENTA = "Assets/Rainbow Folders/Background/Magenta";

		private const string BACKGROUND_FUCHSIA = "Assets/Rainbow Folders/Background/Fuchsia";

		private const string BACKGROUND_ROSE = "Assets/Rainbow Folders/Background/Rose";

		private const string BACKGROUND_CRIMSON = "Assets/Rainbow Folders/Background/Crimson";

		private const int DEFAULT_PRIORITY = 2100;

		private const int ICONS_PRIORITY = 2200;

		private const int BACKGROUND_PRIORITY = 2300;

		private const int SETTINGS_PRIORITY = 2400;

		[MenuItem("Assets/Rainbow Folders/Apply Custom", false, 2100)]
		public static void ApplyCustom()
		{
			var draggableWindow = RainbowFoldersPopup.GetDraggableWindow();
			var inPosition = ProjectWindowAdapter.GetFirstProjectWindow().position.position + new Vector2(10f, 30f);
			var source = Selection.assetGUIDs.Select(AssetDatabase.GUIDToAssetPath).Where(AssetDatabase.IsValidFolder).ToList();
			draggableWindow.ShowWithParams(inPosition, source.ToList(), 0);
		}

		[MenuItem("Assets/Rainbow Folders/Revert to Default", false, 2100)]
		public static void RevertToDefault()
		{
			RevertSelectedFoldersToDefault();
		}

		[MenuItem("Assets/Rainbow Folders/All Rules", false, 2400)]
		public static void ShowAllRules()
		{
			ProjectRuleset.ShowInspector();
		}

		[MenuItem("Assets/Rainbow Folders/Folder Rules", false, 2400)]
		public static void ShowFolderRules()
		{
			string text = null;
			string[] assetGUIDs = Selection.assetGUIDs;
			for (int i = 0; i < assetGUIDs.Length; i++)
			{
				text = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
				if (AssetDatabase.IsValidFolder(text))
				{
					break;
				}
			}
			ProjectRuleset.ShowInspector(AssetDatabase.LoadAssetAtPath<DefaultAsset>(text));
		}

		[MenuItem("Assets/Rainbow Folders/Apply Custom", true)]
		[MenuItem("Assets/Rainbow Folders/Revert to Default", true)]
		[MenuItem("Assets/Rainbow Folders/Folder Rules", true)]
		public static bool IsValidFolder()
		{
			bool flag = false;
			string[] assetGUIDs = Selection.assetGUIDs;
			for (int i = 0; i < assetGUIDs.Length; i++)
			{
				string path = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
				flag |= AssetDatabase.IsValidFolder(path);
			}
			return flag;
		}

		[MenuItem("Assets/Rainbow Folders/Color/Red", false, 2200)]
		public static void Red()
		{
			AssignIconForSelection(ProjectIcon.ClrRed);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Vermilion", false, 2200)]
		public static void Vermilion()
		{
			AssignIconForSelection(ProjectIcon.ClrVermilion);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Orange", false, 2200)]
		public static void Orange()
		{
			AssignIconForSelection(ProjectIcon.ClrOrange);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Amber", false, 2200)]
		public static void Amber()
		{
			AssignIconForSelection(ProjectIcon.ClrAmber);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Yellow", false, 2200)]
		public static void Yellow()
		{
			AssignIconForSelection(ProjectIcon.ClrYellow);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Lime", false, 2200)]
		public static void Lime()
		{
			AssignIconForSelection(ProjectIcon.ClrLime);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Chartreuse", false, 2200)]
		public static void Chartreuse()
		{
			AssignIconForSelection(ProjectIcon.ClrChartreuse);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Harlequin", false, 2200)]
		public static void Harlequin()
		{
			AssignIconForSelection(ProjectIcon.ClrHarlequin);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Green", false, 2300)]
		public static void Green()
		{
			AssignIconForSelection(ProjectIcon.ClrGreen);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Emerald", false, 2300)]
		public static void Emerald()
		{
			AssignIconForSelection(ProjectIcon.ClrEmerald);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Spring-green", false, 2300)]
		public static void SpringGreen()
		{
			AssignIconForSelection(ProjectIcon.ClrSpringGreen);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Aquamarine", false, 2300)]
		public static void Aquamarine()
		{
			AssignIconForSelection(ProjectIcon.ClrAquamarine);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Cyan", false, 2300)]
		public static void BondiBlue()
		{
			AssignIconForSelection(ProjectIcon.ClrCyan);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Sky-blue", false, 2300)]
		public static void SkyBlue()
		{
			AssignIconForSelection(ProjectIcon.ClrSkyBlue);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Azure", false, 2300)]
		public static void Azure()
		{
			AssignIconForSelection(ProjectIcon.ClrAzure);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Cerulean", false, 2300)]
		public static void Cerulean()
		{
			AssignIconForSelection(ProjectIcon.ClrCerulean);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Blue", false, 2400)]
		public static void Blue()
		{
			AssignIconForSelection(ProjectIcon.ClrBlue);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Indigo", false, 2400)]
		public static void Indigo()
		{
			AssignIconForSelection(ProjectIcon.ClrIndigo);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Violet", false, 2400)]
		public static void Violet()
		{
			AssignIconForSelection(ProjectIcon.ClrViolet);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Purple", false, 2400)]
		public static void Purple()
		{
			AssignIconForSelection(ProjectIcon.ClrPurple);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Magenta", false, 2400)]
		public static void Magenta()
		{
			AssignIconForSelection(ProjectIcon.ClrMagenta);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Fuchsia", false, 2400)]
		public static void Fuchsia()
		{
			AssignIconForSelection(ProjectIcon.ClrFuchsia);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Rose", false, 2400)]
		public static void Rose()
		{
			AssignIconForSelection(ProjectIcon.ClrRose);
		}

		[MenuItem("Assets/Rainbow Folders/Color/Crimson", false, 2400)]
		public static void Crimson()
		{
			AssignIconForSelection(ProjectIcon.ClrCrimson);
		}

		[MenuItem("Assets/Rainbow Folders/Transparent/15%", false, 2200)]
		public static void Transparent15()
		{
			AssignIconForSelection(ProjectIcon.Transparent15);
		}

		[MenuItem("Assets/Rainbow Folders/Transparent/25%", false, 2200)]
		public static void Transparent25()
		{
			AssignIconForSelection(ProjectIcon.Transparent25);
		}

		[MenuItem("Assets/Rainbow Folders/Transparent/50%", false, 2200)]
		public static void Transparent50()
		{
			AssignIconForSelection(ProjectIcon.Transparent50);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Red", false, 2200)]
		public static void TagRed()
		{
			AssignIconForSelection(ProjectIcon.TagRed);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Vermilion", false, 2200)]
		public static void TagVermilion()
		{
			AssignIconForSelection(ProjectIcon.TagVermilion);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Orange", false, 2200)]
		public static void TagOrange()
		{
			AssignIconForSelection(ProjectIcon.TagOrange);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Amber", false, 2200)]
		public static void TagAmber()
		{
			AssignIconForSelection(ProjectIcon.TagAmber);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Yellow", false, 2200)]
		public static void TagYellow()
		{
			AssignIconForSelection(ProjectIcon.TagYellow);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Lime", false, 2200)]
		public static void TagLime()
		{
			AssignIconForSelection(ProjectIcon.TagLime);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Chartreuse", false, 2200)]
		public static void TagChartreuse()
		{
			AssignIconForSelection(ProjectIcon.TagChartreuse);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Harlequin", false, 2200)]
		public static void TagHarlequin()
		{
			AssignIconForSelection(ProjectIcon.TagHarlequin);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Green", false, 2300)]
		public static void TagGreen()
		{
			AssignIconForSelection(ProjectIcon.TagGreen);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Emerald", false, 2300)]
		public static void TagEmerald()
		{
			AssignIconForSelection(ProjectIcon.TagEmerald);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Spring-green", false, 2300)]
		public static void TagSpringGreen()
		{
			AssignIconForSelection(ProjectIcon.TagSpringGreen);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Aquamarine", false, 2300)]
		public static void TagAquamarine()
		{
			AssignIconForSelection(ProjectIcon.TagAquamarine);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Cyan", false, 2300)]
		public static void TagBondiBlue()
		{
			AssignIconForSelection(ProjectIcon.TagCyan);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Sky-blue", false, 2300)]
		public static void TagSkyBlue()
		{
			AssignIconForSelection(ProjectIcon.TagSkyBlue);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Azure", false, 2300)]
		public static void TagAzure()
		{
			AssignIconForSelection(ProjectIcon.TagAzure);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Cerulean", false, 2300)]
		public static void TagCerulean()
		{
			AssignIconForSelection(ProjectIcon.TagCerulean);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Blue", false, 2400)]
		public static void TagBlue()
		{
			AssignIconForSelection(ProjectIcon.TagBlue);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Indigo", false, 2400)]
		public static void TagIndigo()
		{
			AssignIconForSelection(ProjectIcon.TagIndigo);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Violet", false, 2400)]
		public static void TagViolet()
		{
			AssignIconForSelection(ProjectIcon.TagViolet);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Purple", false, 2400)]
		public static void TagPurple()
		{
			AssignIconForSelection(ProjectIcon.TagPurple);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Magenta", false, 2400)]
		public static void TagMagenta()
		{
			AssignIconForSelection(ProjectIcon.TagMagenta);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Fuchsia", false, 2400)]
		public static void TagFuchsia()
		{
			AssignIconForSelection(ProjectIcon.TagFuchsia);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Rose", false, 2400)]
		public static void TagRose()
		{
			AssignIconForSelection(ProjectIcon.TagRose);
		}

		[MenuItem("Assets/Rainbow Folders/Tag/Crimson", false, 2400)]
		public static void TagCrimson()
		{
			AssignIconForSelection(ProjectIcon.TagCrimson);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Animations", false, 2200)]
		public static void TypeAnimations()
		{
			AssignIconForSelection(ProjectIcon.TpeAnimations);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Audio", false, 2200)]
		public static void TypeAudio()
		{
			AssignIconForSelection(ProjectIcon.TpeAudio);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Editor", false, 2200)]
		public static void TypeEditor()
		{
			AssignIconForSelection(ProjectIcon.TpeEditor);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Extensions", false, 2200)]
		public static void TypeExtensions()
		{
			AssignIconForSelection(ProjectIcon.TpeExtensions);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Flares", false, 2200)]
		public static void TypeFlares()
		{
			AssignIconForSelection(ProjectIcon.TpeFlares);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Fonts", false, 2200)]
		public static void TypeFonts()
		{
			AssignIconForSelection(ProjectIcon.TpeFonts);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Materials", false, 2200)]
		public static void TypeMaterials()
		{
			AssignIconForSelection(ProjectIcon.TpeMaterials);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Meshes", false, 2200)]
		public static void TypeMeshes()
		{
			AssignIconForSelection(ProjectIcon.TpeMeshes);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Physics", false, 2200)]
		public static void TypePhysics()
		{
			AssignIconForSelection(ProjectIcon.TpePhysics);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Plugins", false, 2200)]
		public static void TypePlugins()
		{
			AssignIconForSelection(ProjectIcon.TpePlugins);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Prefabs", false, 2200)]
		public static void TypePrefabs()
		{
			AssignIconForSelection(ProjectIcon.TpePrefabs);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Project", false, 2200)]
		public static void TypeProject()
		{
			AssignIconForSelection(ProjectIcon.TpeProject);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Rainbow", false, 2200)]
		public static void TypeRainbow()
		{
			AssignIconForSelection(ProjectIcon.TpeRainbow);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Resources", false, 2200)]
		public static void TypeResources()
		{
			AssignIconForSelection(ProjectIcon.TpeResources);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Scenes", false, 2200)]
		public static void TypeScenes()
		{
			AssignIconForSelection(ProjectIcon.TpeScenes);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Scripts", false, 2200)]
		public static void TypeScripts()
		{
			AssignIconForSelection(ProjectIcon.TpeScripts);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Shaders", false, 2200)]
		public static void TypeShaders()
		{
			AssignIconForSelection(ProjectIcon.TpeShaders);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Terrains", false, 2200)]
		public static void TypeTerrains()
		{
			AssignIconForSelection(ProjectIcon.TpeTerrains);
		}

		[MenuItem("Assets/Rainbow Folders/Type/Textures", false, 2200)]
		public static void TypeTextures()
		{
			AssignIconForSelection(ProjectIcon.TpeTextures);
		}

		[MenuItem("Assets/Rainbow Folders/Platform/Android", false, 2200)]
		public static void PlatformAndroid()
		{
			AssignIconForSelection(ProjectIcon.PfmAndroid);
		}

		[MenuItem("Assets/Rainbow Folders/Platform/iOS", false, 2200)]
		public static void PlatformiOS()
		{
			AssignIconForSelection(ProjectIcon.PfmiOS);
		}

		[MenuItem("Assets/Rainbow Folders/Platform/Mac", false, 2200)]
		public static void PlatformMac()
		{
			AssignIconForSelection(ProjectIcon.PfmMac);
		}

		[MenuItem("Assets/Rainbow Folders/Platform/WebGL", false, 2200)]
		public static void PlatformWebGL()
		{
			AssignIconForSelection(ProjectIcon.PfmWebGL);
		}

		[MenuItem("Assets/Rainbow Folders/Platform/Windows", false, 2200)]
		public static void PlatformWindows()
		{
			AssignIconForSelection(ProjectIcon.PfmWindows);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Red", false, 2300)]
		public static void BackgroundRed()
		{
			AssignBackgroundForSelection(CoreBackground.ClrRed);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Vermilion", false, 2300)]
		public static void BackgroundVermilion()
		{
			AssignBackgroundForSelection(CoreBackground.ClrVermilion);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Orange", false, 2300)]
		public static void BackgroundOrange()
		{
			AssignBackgroundForSelection(CoreBackground.ClrOrange);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Amber", false, 2300)]
		public static void BackgroundAmber()
		{
			AssignBackgroundForSelection(CoreBackground.ClrAmber);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Yellow", false, 2300)]
		public static void BackgroundYellow()
		{
			AssignBackgroundForSelection(CoreBackground.ClrYellow);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Lime", false, 2300)]
		public static void BackgroundLime()
		{
			AssignBackgroundForSelection(CoreBackground.ClrLime);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Chartreuse", false, 2300)]
		public static void BackgroundChartreuse()
		{
			AssignBackgroundForSelection(CoreBackground.ClrChartreuse);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Harlequin", false, 2300)]
		public static void BackgroundHarlequin()
		{
			AssignBackgroundForSelection(CoreBackground.ClrHarlequin);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Green", false, 2400)]
		public static void BackgroundGreen()
		{
			AssignBackgroundForSelection(CoreBackground.ClrGreen);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Emerald", false, 2400)]
		public static void BackgroundEmerald()
		{
			AssignBackgroundForSelection(CoreBackground.ClrEmerald);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Spring-green", false, 2400)]
		public static void BackgroundSpringGreen()
		{
			AssignBackgroundForSelection(CoreBackground.ClrSpringGreen);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Aquamarine", false, 2400)]
		public static void BackgroundAquamarine()
		{
			AssignBackgroundForSelection(CoreBackground.ClrAquamarine);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Cyan", false, 2400)]
		public static void BackgroundBondiBlue()
		{
			AssignBackgroundForSelection(CoreBackground.ClrCyan);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Sky-blue", false, 2400)]
		public static void BackgroundSkyBlue()
		{
			AssignBackgroundForSelection(CoreBackground.ClrSkyBlue);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Azure", false, 2400)]
		public static void BackgroundAzure()
		{
			AssignBackgroundForSelection(CoreBackground.ClrAzure);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Cerulean", false, 2400)]
		public static void BackgroundCerulean()
		{
			AssignBackgroundForSelection(CoreBackground.ClrCerulean);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Blue", false, 2500)]
		public static void BackgroundBlue()
		{
			AssignBackgroundForSelection(CoreBackground.ClrBlue);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Indigo", false, 2500)]
		public static void BackgroundIndigo()
		{
			AssignBackgroundForSelection(CoreBackground.ClrIndigo);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Violet", false, 2500)]
		public static void BackgroundViolet()
		{
			AssignBackgroundForSelection(CoreBackground.ClrViolet);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Purple", false, 2500)]
		public static void BackgroundPurple()
		{
			AssignBackgroundForSelection(CoreBackground.ClrPurple);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Magenta", false, 2500)]
		public static void BackgroundMagenta()
		{
			AssignBackgroundForSelection(CoreBackground.ClrMagenta);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Fuchsia", false, 2500)]
		public static void BackgroundFuchsia()
		{
			AssignBackgroundForSelection(CoreBackground.ClrFuchsia);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Rose", false, 2500)]
		public static void BackgroundRose()
		{
			AssignBackgroundForSelection(CoreBackground.ClrRose);
		}

		[MenuItem("Assets/Rainbow Folders/Background/Crimson", false, 2500)]
		public static void BackgroundCrimson()
		{
			AssignBackgroundForSelection(CoreBackground.ClrCrimson);
		}

		private static void AssignIconForSelection(ProjectIcon icon)
		{
			Selection.assetGUIDs.ToList().ForEach(delegate(string assetGuid)
			{
				string text = AssetDatabase.GUIDToAssetPath(assetGuid);
				if (AssetDatabase.IsValidFolder(text))
				{
					string assetPath = AssetDatabase.GetAssetPath(AssetDatabase.LoadAssetAtPath<DefaultAsset>(text));
					ProjectRuleset.Instance.ChangeRuleIconsByPath(assetPath, icon);
				}
			});
		}

		private static void AssignBackgroundForSelection(CoreBackground background)
		{
			Selection.assetGUIDs.ToList().ForEach(delegate(string assetGuid)
			{
				string text = AssetDatabase.GUIDToAssetPath(assetGuid);
				if (AssetDatabase.IsValidFolder(text))
				{
					string assetPath = AssetDatabase.GetAssetPath(AssetDatabase.LoadAssetAtPath<DefaultAsset>(text));
					ProjectRuleset.Instance.ChangeRuleBackgroundByPath(assetPath, background);
				}
			});
		}

		private static void RevertSelectedFoldersToDefault()
		{
			Selection.assetGUIDs.ToList().ForEach(delegate(string assetGuid)
			{
				string path = AssetDatabase.GUIDToAssetPath(assetGuid);
				if (AssetDatabase.IsValidFolder(path))
				{
					ProjectRuleset.Instance.RemoveAllByPath(path);
				}
			});
		}
	}
}
