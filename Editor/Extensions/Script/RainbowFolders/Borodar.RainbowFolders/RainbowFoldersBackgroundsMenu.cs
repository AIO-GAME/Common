using Borodar.RainbowCore;
using Borodar.RainbowFolders.Settings;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders
{
	public static class RainbowFoldersBackgroundsMenu
	{
		private const string MENU_COLORIZE = "Colors/";

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

		private static readonly GUIContent SELECT_CUSTOM = new GUIContent("Custom");

		private static readonly GUIContent SELECT_NONE = new GUIContent("None");

		public static void ShowDropDown(Rect position, object item)
		{
			GenericMenu genericMenu = new GenericMenu();
			genericMenu.AddItem(COLOR_RED, on: false, RedCallback, item);
			genericMenu.AddItem(COLOR_VERMILION, on: false, VermilionCallback, item);
			genericMenu.AddItem(COLOR_ORANGE, on: false, OrangeCallback, item);
			genericMenu.AddItem(COLOR_AMBER, on: false, AmberCallback, item);
			genericMenu.AddItem(COLOR_YELLOW, on: false, YellowCallback, item);
			genericMenu.AddItem(COLOR_LIME, on: false, LimeCallback, item);
			genericMenu.AddItem(COLOR_CHARTREUSE, on: false, ChartreuseCallback, item);
			genericMenu.AddItem(COLOR_HARLEQUIN, on: false, HarlequinCallback, item);
			genericMenu.AddSeparator("Colors/");
			genericMenu.AddItem(COLOR_GREEN, on: false, GreenCallback, item);
			genericMenu.AddItem(COLOR_EMERALD, on: false, EmeraldCallback, item);
			genericMenu.AddItem(COLOR_SPRING_GREEN, on: false, SpringGreenCallback, item);
			genericMenu.AddItem(COLOR_AQUAMARINE, on: false, AquamarineCallback, item);
			genericMenu.AddItem(COLOR_CYAN, on: false, CyanCallback, item);
			genericMenu.AddItem(COLOR_SKY_BLUE, on: false, SkyBlueCallback, item);
			genericMenu.AddItem(COLOR_AZURE, on: false, AzureCallback, item);
			genericMenu.AddItem(COLOR_CERULEAN, on: false, CeruleanCallback, item);
			genericMenu.AddSeparator("Colors/");
			genericMenu.AddItem(COLOR_BLUE, on: false, BlueCallback, item);
			genericMenu.AddItem(COLOR_INDIGO, on: false, IndigoCallback, item);
			genericMenu.AddItem(COLOR_VIOLET, on: false, VioletCallback, item);
			genericMenu.AddItem(COLOR_PURPLE, on: false, PurpleCallback, item);
			genericMenu.AddItem(COLOR_MAGENTA, on: false, MagentaCallback, item);
			genericMenu.AddItem(COLOR_FUCHSIA, on: false, FuchsiaCallback, item);
			genericMenu.AddItem(COLOR_ROSE, on: false, RoseCallback, item);
			genericMenu.AddItem(COLOR_CRIMSON, on: false, CrimsonCallback, item);
			genericMenu.AddSeparator(string.Empty);
			genericMenu.AddItem(SELECT_CUSTOM, on: false, SelectCustomCallback, item);
			genericMenu.AddItem(SELECT_NONE, on: false, SelectNoneCallback, item);
			genericMenu.DropDown(position);
		}

		private static void RedCallback(object item)
		{
			AssignBackground(CoreBackground.ClrRed, item);
		}

		private static void VermilionCallback(object item)
		{
			AssignBackground(CoreBackground.ClrVermilion, item);
		}

		private static void OrangeCallback(object item)
		{
			AssignBackground(CoreBackground.ClrOrange, item);
		}

		private static void AmberCallback(object item)
		{
			AssignBackground(CoreBackground.ClrAmber, item);
		}

		private static void YellowCallback(object item)
		{
			AssignBackground(CoreBackground.ClrYellow, item);
		}

		private static void LimeCallback(object item)
		{
			AssignBackground(CoreBackground.ClrLime, item);
		}

		private static void ChartreuseCallback(object item)
		{
			AssignBackground(CoreBackground.ClrChartreuse, item);
		}

		private static void HarlequinCallback(object item)
		{
			AssignBackground(CoreBackground.ClrHarlequin, item);
		}

		private static void GreenCallback(object item)
		{
			AssignBackground(CoreBackground.ClrGreen, item);
		}

		private static void EmeraldCallback(object item)
		{
			AssignBackground(CoreBackground.ClrEmerald, item);
		}

		private static void SpringGreenCallback(object item)
		{
			AssignBackground(CoreBackground.ClrSpringGreen, item);
		}

		private static void AquamarineCallback(object item)
		{
			AssignBackground(CoreBackground.ClrAquamarine, item);
		}

		private static void CyanCallback(object item)
		{
			AssignBackground(CoreBackground.ClrCyan, item);
		}

		private static void SkyBlueCallback(object item)
		{
			AssignBackground(CoreBackground.ClrSkyBlue, item);
		}

		private static void AzureCallback(object item)
		{
			AssignBackground(CoreBackground.ClrAzure, item);
		}

		private static void CeruleanCallback(object item)
		{
			AssignBackground(CoreBackground.ClrCerulean, item);
		}

		private static void BlueCallback(object item)
		{
			AssignBackground(CoreBackground.ClrBlue, item);
		}

		private static void IndigoCallback(object item)
		{
			AssignBackground(CoreBackground.ClrIndigo, item);
		}

		private static void VioletCallback(object item)
		{
			AssignBackground(CoreBackground.ClrViolet, item);
		}

		private static void PurpleCallback(object item)
		{
			AssignBackground(CoreBackground.ClrPurple, item);
		}

		private static void MagentaCallback(object item)
		{
			AssignBackground(CoreBackground.ClrMagenta, item);
		}

		private static void FuchsiaCallback(object item)
		{
			AssignBackground(CoreBackground.ClrFuchsia, item);
		}

		private static void RoseCallback(object item)
		{
			AssignBackground(CoreBackground.ClrRose, item);
		}

		private static void CrimsonCallback(object item)
		{
			AssignBackground(CoreBackground.ClrCrimson, item);
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

		private static void AssignBackground(CoreBackground type, object item)
		{
			if (IsSerializedProperty(item))
			{
				AssignBackground(type, item as SerializedProperty);
			}
			else
			{
				AssignBackground(type, item as ProjectRule);
			}
		}

		private static void AssignBackground(CoreBackground type, ProjectRule rule)
		{
			rule.BackgroundType = type;
			rule.BackgroundTexture = null;
		}

		private static void SelectNone(ProjectRule rule)
		{
			rule.BackgroundType = CoreBackground.None;
			rule.BackgroundTexture = null;
			rule.IsBackgroundRecursive = false;
		}

		private static void SelectCustom(ProjectRule rule)
		{
			rule.BackgroundType = CoreBackground.Custom;
			rule.BackgroundTexture = null;
		}

		private static void AssignBackground(CoreBackground type, SerializedProperty property)
		{
			property.FindPropertyRelative("BackgroundType").intValue = (int)type;
			property.FindPropertyRelative("BackgroundTexture").objectReferenceValue = null;
			ApplyModifiedProperties(property);
		}

		private static void SelectNone(SerializedProperty property)
		{
			property.FindPropertyRelative("BackgroundType").intValue = 0;
			property.FindPropertyRelative("BackgroundTexture").objectReferenceValue = null;
			property.FindPropertyRelative("IsBackgroundRecursive").boolValue = false;
			ApplyModifiedProperties(property);
		}

		private static void SelectCustom(SerializedProperty property)
		{
			property.FindPropertyRelative("BackgroundType").intValue = 1;
			property.FindPropertyRelative("BackgroundTexture").objectReferenceValue = null;
			ApplyModifiedProperties(property);
		}

		private static void ApplyModifiedProperties(SerializedProperty property)
		{
			property.serializedObject.ApplyModifiedProperties();
			ProjectRuleset.OnRulesetChange();
		}

		private static bool IsSerializedProperty(object item)
		{
			return item.GetType() == typeof(SerializedProperty);
		}
	}
}
