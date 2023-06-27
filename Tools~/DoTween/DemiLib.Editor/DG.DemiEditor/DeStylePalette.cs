using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Stores a GUIStyle palette, which can be passed to default DeGUI layouts when calling <code>DeGUI.BeginGUI</code>,
	/// and changed at any time by calling <code>DeGUI.ChangePalette</code>.
	/// You can inherit from this class to create custom GUIStyle palettes with more options.
	/// Each of the sub-options require a public Init method to initialize the styles, which will be called via Reflection.
	/// </summary>
	public class DeStylePalette
	{
		public readonly BoxStyles box = new BoxStyles();

		public readonly ButtonStyles button = new ButtonStyles();

		public readonly LabelStyles label = new LabelStyles();

		public readonly ToolbarStyles toolbar = new ToolbarStyles();

		public readonly MiscStyles misc = new MiscStyles();

		protected bool initialized;

		protected bool initializedAsInterfont;

		private static string _fooAdbImgsDir;

		private static Texture2D _transparent;

		private static Texture2D _whiteSquare;

		private static Texture2D _whiteSquareAlpha10;

		private static Texture2D _whiteSquareAlpha15;

		private static Texture2D _whiteSquareAlpha25;

		private static Texture2D _whiteSquareAlpha50;

		private static Texture2D _whiteSquareAlpha80;

		private static Texture2D _whiteSquare_fadeOut_bt;

		private static Texture2D _blackSquare;

		private static Texture2D _blackSquareAlpha10;

		private static Texture2D _blackSquareAlpha15;

		private static Texture2D _blackSquareAlpha25;

		private static Texture2D _blackSquareAlpha50;

		private static Texture2D _blackSquareAlpha80;

		private static Texture2D _redSquare;

		private static Texture2D _orangeSquare;

		private static Texture2D _yellowSquare;

		private static Texture2D _greenSquare;

		private static Texture2D _blueSquare;

		private static Texture2D _purpleSquare;

		private static Texture2D _squareBorder;

		private static Texture2D _squareBorderEmpty01;

		private static Texture2D _squareBorderEmpty02;

		private static Texture2D _squareBorderEmpty03;

		private static Texture2D _squareBorderAlpha15;

		private static Texture2D _squareBorderCurved;

		private static Texture2D _squareBorderCurved02;

		private static Texture2D _squareBorderCurvedEmpty;

		private static Texture2D _squareBorderCurvedEmptyThick;

		private static Texture2D _squareBorderCurvedEmpty02;

		private static Texture2D _squareBorderCurvedAlpha;

		private static Texture2D _squareBorderCurved_darkBorders;

		private static Texture2D _squareBorderCurved_darkBordersAlpha;

		private static Texture2D _squareBorderCurved02_darkBorders;

		private static Texture2D _squareCornersEmpty02;

		private static Texture2D _whiteDot;

		private static Texture2D _whiteDot_darkBorder;

		private static Texture2D _whiteDot_whiteBorderAlpha;

		private static Texture2D _circle;

		private static Texture2D _ico_demigiant;

		private static Texture2D _ico_lock;

		private static Texture2D _ico_lock_open;

		private static Texture2D _ico_visibility_on;

		private static Texture2D _ico_visibility_off;

		private static Texture2D _ico_flipV;

		private static Texture2D _ico_optionsDropdown;

		private static Texture2D _ico_foldout_open;

		private static Texture2D _ico_foldout_closed;

		private static Texture2D _ico_nodeArrow;

		private static Texture2D _ico_delete;

		private static Texture2D _ico_end;

		private static Texture2D _ico_alert;

		private static Texture2D _ico_ok;

		private static Texture2D _ico_alignTL;

		private static Texture2D _ico_alignTC;

		private static Texture2D _ico_alignTR;

		private static Texture2D _ico_alignCL;

		private static Texture2D _ico_alignCC;

		private static Texture2D _ico_alignCR;

		private static Texture2D _ico_alignBL;

		private static Texture2D _ico_alignBC;

		private static Texture2D _ico_alignBR;

		private static Texture2D _ico_alignL;

		private static Texture2D _ico_alignHC;

		private static Texture2D _ico_alignR;

		private static Texture2D _ico_alignT;

		private static Texture2D _ico_alignVC;

		private static Texture2D _ico_alignB;

		private static Texture2D _ico_distributeHAlignT;

		private static Texture2D _ico_distributeVAlignL;

		private static Texture2D _ico_star;

		private static Texture2D _ico_star_border;

		private static Texture2D _ico_cog;

		private static Texture2D _ico_cog_border;

		private static Texture2D _ico_play;

		private static Texture2D _ico_play_border;

		private static Texture2D _ico_comment;

		private static Texture2D _ico_comment_border;

		private static Texture2D _ico_ui;

		private static Texture2D _ico_ui_border;

		private static Texture2D _ico_heart;

		private static Texture2D _ico_heart_border;

		private static Texture2D _ico_skull;

		private static Texture2D _ico_skull_border;

		private static Texture2D _ico_camera;

		private static Texture2D _ico_camera_border;

		private static Texture2D _ico_light;

		private static Texture2D _ico_light_border;

		private static Texture2D _grid_dark;

		private static Texture2D _grid_bright;

		private static Texture2D _tileBars_empty;

		private static Texture2D _tileBars_slanted;

		private static Texture2D _tileBars_slanted_alpha;

		private static Texture2D _proj_folder;

		private static Texture2D _proj_atlas;

		private static Texture2D _proj_audio;

		private static Texture2D _proj_bundle;

		private static Texture2D _proj_cog;

		private static Texture2D _proj_cross;

		private static Texture2D _proj_demigiant;

		private static Texture2D _proj_fonts;

		private static Texture2D _proj_heart;

		private static Texture2D _proj_materials;

		private static Texture2D _proj_models;

		private static Texture2D _proj_particles;

		private static Texture2D _proj_play;

		private static Texture2D _proj_prefab;

		private static Texture2D _proj_shaders;

		private static Texture2D _proj_scripts;

		private static Texture2D _proj_skull;

		private static Texture2D _proj_star;

		private static Texture2D _proj_terrains;

		private static Texture2D _proj_textures;

		private static string _adbImgsDir
		{
			get
			{
				if (_fooAdbImgsDir == null)
				{
					_fooAdbImgsDir = string.Concat(Assembly.GetExecutingAssembly().ADBDir(), "/Imgs/");
				}
				return _fooAdbImgsDir;
			}
		}

		public static Texture2D transparent => LoadTexture(ref _transparent, "transparentSquare");

		public static Texture2D whiteSquare => LoadTexture(ref _whiteSquare, "whiteSquare");

		public static Texture2D whiteSquareAlpha10 => LoadTexture(ref _whiteSquareAlpha10, "whiteSquareAlpha10");

		public static Texture2D whiteSquareAlpha15 => LoadTexture(ref _whiteSquareAlpha15, "whiteSquareAlpha15");

		public static Texture2D whiteSquareAlpha25 => LoadTexture(ref _whiteSquareAlpha25, "whiteSquareAlpha25");

		public static Texture2D whiteSquareAlpha50 => LoadTexture(ref _whiteSquareAlpha50, "whiteSquareAlpha50");

		public static Texture2D whiteSquareAlpha80 => LoadTexture(ref _whiteSquareAlpha80, "whiteSquareAlpha80");

		public static Texture2D whiteSquare_fadeOut_bt => LoadTexture(ref _whiteSquare_fadeOut_bt, "whiteSquare_fadeOut_bt", FilterMode.Bilinear);

		public static Texture2D blackSquare => LoadTexture(ref _blackSquare, "blackSquare");

		public static Texture2D blackSquareAlpha10 => LoadTexture(ref _blackSquareAlpha10, "blackSquareAlpha10");

		public static Texture2D blackSquareAlpha15 => LoadTexture(ref _blackSquareAlpha15, "blackSquareAlpha15");

		public static Texture2D blackSquareAlpha25 => LoadTexture(ref _blackSquareAlpha25, "blackSquareAlpha25");

		public static Texture2D blackSquareAlpha50 => LoadTexture(ref _blackSquareAlpha50, "blackSquareAlpha50");

		public static Texture2D blackSquareAlpha80 => LoadTexture(ref _blackSquareAlpha80, "blackSquareAlpha80");

		public static Texture2D redSquare => LoadTexture(ref _redSquare, "redSquare");

		public static Texture2D orangeSquare => LoadTexture(ref _orangeSquare, "orangeSquare");

		public static Texture2D yellowSquare => LoadTexture(ref _yellowSquare, "yellowSquare");

		public static Texture2D greenSquare => LoadTexture(ref _greenSquare, "greenSquare");

		public static Texture2D blueSquare => LoadTexture(ref _blueSquare, "blueSquare");

		public static Texture2D purpleSquare => LoadTexture(ref _purpleSquare, "purpleSquare");

		public static Texture2D squareBorder => LoadTexture(ref _squareBorder, "squareBorder");

		public static Texture2D squareBorderEmpty01 => LoadTexture(ref _squareBorderEmpty01, "squareBorderEmpty01");

		public static Texture2D squareBorderEmpty02 => LoadTexture(ref _squareBorderEmpty02, "squareBorderEmpty02");

		public static Texture2D squareBorderEmpty03 => LoadTexture(ref _squareBorderEmpty03, "squareBorderEmpty03");

		public static Texture2D squareBorderAlpha15 => LoadTexture(ref _squareBorderAlpha15, "squareBorderAlpha15");

		public static Texture2D squareBorderCurved => LoadTexture(ref _squareBorderCurved, "squareBorderCurved");

		public static Texture2D squareBorderCurved02 => LoadTexture(ref _squareBorderCurved02, "squareBorderCurved02");

		public static Texture2D squareBorderCurvedEmpty => LoadTexture(ref _squareBorderCurvedEmpty, "squareBorderCurvedEmpty");

		public static Texture2D squareBorderCurvedEmptyThick => LoadTexture(ref _squareBorderCurvedEmptyThick, "squareBorderCurvedEmptyThick");

		public static Texture2D squareBorderCurvedEmpty02 => LoadTexture(ref _squareBorderCurvedEmpty02, "squareBorderCurvedEmpty02");

		public static Texture2D squareBorderCurvedAlpha => LoadTexture(ref _squareBorderCurvedAlpha, "squareBorderCurvedAlpha");

		public static Texture2D squareBorderCurved_darkBorders => LoadTexture(ref _squareBorderCurved_darkBorders, "squareBorderCurved_darkBorders");

		public static Texture2D squareBorderCurved_darkBordersAlpha => LoadTexture(ref _squareBorderCurved_darkBordersAlpha, "squareBorderCurved_darkBordersAlpha");

		public static Texture2D squareBorderCurved02_darkBorders => LoadTexture(ref _squareBorderCurved02_darkBorders, "squareBorderCurved02_darkBorders");

		public static Texture2D squareCornersEmpty02 => LoadTexture(ref _squareCornersEmpty02, "squareCornersEmpty02");

		public static Texture2D whiteDot => LoadTexture(ref _whiteDot, "whiteDot");

		public static Texture2D whiteDot_darkBorder => LoadTexture(ref _whiteDot_darkBorder, "whiteDot_darkBorder");

		public static Texture2D whiteDot_whiteBorderAlpha => LoadTexture(ref _whiteDot_whiteBorderAlpha, "whiteDot_whiteBorderAlpha");

		public static Texture2D circle => LoadTexture(ref _circle, "circle", FilterMode.Bilinear);

		public static Texture2D ico_demigiant => LoadTexture(ref _ico_demigiant, "ico_demigiant", FilterMode.Bilinear, 16);

		public static Texture2D ico_lock => LoadTexture(ref _ico_lock, "ico_lock");

		public static Texture2D ico_lock_open => LoadTexture(ref _ico_lock_open, "ico_lock_open");

		public static Texture2D ico_visibility => LoadTexture(ref _ico_visibility_on, "ico_visibility");

		public static Texture2D ico_visibility_off => LoadTexture(ref _ico_visibility_off, "ico_visibility_off");

		public static Texture2D ico_flipV => LoadTexture(ref _ico_flipV, "ico_flipV");

		public static Texture2D ico_optionsDropdown => LoadTexture(ref _ico_optionsDropdown, "ico_optionsDropdown");

		public static Texture2D ico_foldout_open => LoadTexture(ref _ico_foldout_open, "ico_foldout_open");

		public static Texture2D ico_foldout_closed => LoadTexture(ref _ico_foldout_closed, "ico_foldout_closed");

		public static Texture2D ico_nodeArrow => LoadTexture(ref _ico_nodeArrow, "ico_nodeArrow", FilterMode.Bilinear, 16);

		public static Texture2D ico_delete => LoadTexture(ref _ico_delete, "ico_delete", FilterMode.Bilinear, 16);

		public static Texture2D ico_end => LoadTexture(ref _ico_end, "ico_end", FilterMode.Bilinear);

		public static Texture2D ico_alert => LoadTexture(ref _ico_alert, "ico_alert", FilterMode.Bilinear);

		public static Texture2D ico_ok => LoadTexture(ref _ico_ok, "ico_ok", FilterMode.Bilinear);

		public static Texture2D ico_alignTL => LoadTexture(ref _ico_alignTL, "ico_alignTL");

		public static Texture2D ico_alignTC => LoadTexture(ref _ico_alignTC, "ico_alignTC");

		public static Texture2D ico_alignTR => LoadTexture(ref _ico_alignTR, "ico_alignTR");

		public static Texture2D ico_alignCL => LoadTexture(ref _ico_alignCL, "ico_alignCL");

		public static Texture2D ico_alignCC => LoadTexture(ref _ico_alignCC, "ico_alignCC");

		public static Texture2D ico_alignCR => LoadTexture(ref _ico_alignCR, "ico_alignCR");

		public static Texture2D ico_alignBL => LoadTexture(ref _ico_alignBL, "ico_alignBL");

		public static Texture2D ico_alignBC => LoadTexture(ref _ico_alignBC, "ico_alignBC");

		public static Texture2D ico_alignBR => LoadTexture(ref _ico_alignBR, "ico_alignBR");

		public static Texture2D ico_alignL => LoadTexture(ref _ico_alignL, "ico_alignL");

		public static Texture2D ico_alignHC => LoadTexture(ref _ico_alignHC, "ico_alignHC");

		public static Texture2D ico_alignR => LoadTexture(ref _ico_alignR, "ico_alignR");

		public static Texture2D ico_alignT => LoadTexture(ref _ico_alignT, "ico_alignT");

		public static Texture2D ico_alignVC => LoadTexture(ref _ico_alignVC, "ico_alignVC");

		public static Texture2D ico_alignB => LoadTexture(ref _ico_alignB, "ico_alignB");

		public static Texture2D ico_distributeHAlignT => LoadTexture(ref _ico_distributeHAlignT, "ico_distributeHAlignT");

		public static Texture2D ico_distributeVAlignL => LoadTexture(ref _ico_distributeVAlignL, "ico_distributeVAlignL");

		public static Texture2D ico_star => LoadTexture(ref _ico_star, "ico_star");

		public static Texture2D ico_star_border => LoadTexture(ref _ico_star_border, "ico_star_border");

		public static Texture2D ico_play => LoadTexture(ref _ico_play, "ico_play");

		public static Texture2D ico_play_border => LoadTexture(ref _ico_play_border, "ico_play_border");

		public static Texture2D ico_cog => LoadTexture(ref _ico_cog, "ico_cog");

		public static Texture2D ico_cog_border => LoadTexture(ref _ico_cog_border, "ico_cog_border");

		public static Texture2D ico_comment => LoadTexture(ref _ico_comment, "ico_comment");

		public static Texture2D ico_comment_border => LoadTexture(ref _ico_comment_border, "ico_comment_border");

		public static Texture2D ico_ui => LoadTexture(ref _ico_ui, "ico_ui");

		public static Texture2D ico_ui_border => LoadTexture(ref _ico_ui_border, "ico_ui_border");

		public static Texture2D ico_heart => LoadTexture(ref _ico_heart, "ico_heart");

		public static Texture2D ico_heart_border => LoadTexture(ref _ico_heart_border, "ico_heart_border");

		public static Texture2D ico_skull => LoadTexture(ref _ico_skull, "ico_skull");

		public static Texture2D ico_skull_border => LoadTexture(ref _ico_skull_border, "ico_skull_border");

		public static Texture2D ico_camera => LoadTexture(ref _ico_camera, "ico_camera");

		public static Texture2D ico_camera_border => LoadTexture(ref _ico_camera_border, "ico_camera_border");

		public static Texture2D ico_light => LoadTexture(ref _ico_light, "ico_light");

		public static Texture2D ico_light_border => LoadTexture(ref _ico_light_border, "ico_light_border");

		public static Texture2D grid_dark => LoadTexture(ref _grid_dark, "grid_dark", FilterMode.Point, 64, TextureWrapMode.Repeat);

		public static Texture2D grid_bright => LoadTexture(ref _grid_bright, "grid_bright", FilterMode.Point, 64, TextureWrapMode.Repeat);

		public static Texture2D tileBars_empty => LoadTexture(ref _tileBars_empty, "tileBars_empty", FilterMode.Point, 32, TextureWrapMode.Repeat);

		public static Texture2D tileBars_slanted => LoadTexture(ref _tileBars_slanted, "tileBars_slanted", FilterMode.Point, 32, TextureWrapMode.Repeat);

		public static Texture2D tileBars_slanted_alpha => LoadTexture(ref _tileBars_slanted_alpha, "tileBars_slanted_alpha", FilterMode.Point, 32, TextureWrapMode.Repeat);

		public static Texture2D proj_folder => LoadTexture(ref _proj_folder, "project/ico_folder");

		public static Texture2D proj_atlas => LoadTexture(ref _proj_atlas, "project/ico_atlas");

		public static Texture2D proj_audio => LoadTexture(ref _proj_audio, "project/ico_audio");

		public static Texture2D proj_bundle => LoadTexture(ref _proj_bundle, "project/ico_bundle");

		public static Texture2D proj_cog => LoadTexture(ref _proj_cog, "project/ico_cog");

		public static Texture2D proj_cross => LoadTexture(ref _proj_cross, "project/ico_cross");

		public static Texture2D proj_demigiant => LoadTexture(ref _proj_demigiant, "project/ico_demigiant");

		public static Texture2D proj_fonts => LoadTexture(ref _proj_fonts, "project/ico_fonts");

		public static Texture2D proj_heart => LoadTexture(ref _proj_heart, "project/ico_heart");

		public static Texture2D proj_materials => LoadTexture(ref _proj_materials, "project/ico_materials");

		public static Texture2D proj_models => LoadTexture(ref _proj_models, "project/ico_models");

		public static Texture2D proj_particles => LoadTexture(ref _proj_particles, "project/ico_particles");

		public static Texture2D proj_play => LoadTexture(ref _proj_play, "project/ico_play");

		public static Texture2D proj_prefab => LoadTexture(ref _proj_prefab, "project/ico_prefab");

		public static Texture2D proj_shaders => LoadTexture(ref _proj_shaders, "project/ico_shaders");

		public static Texture2D proj_scripts => LoadTexture(ref _proj_scripts, "project/ico_scripts");

		public static Texture2D proj_skull => LoadTexture(ref _proj_skull, "project/ico_skull");

		public static Texture2D proj_star => LoadTexture(ref _proj_star, "project/ico_star");

		public static Texture2D proj_terrains => LoadTexture(ref _proj_terrains, "project/ico_terrains");

		public static Texture2D proj_textures => LoadTexture(ref _proj_textures, "project/ico_textures");

		/// <summary>
		/// Called automatically by <code>DeGUI.BeginGUI</code>.
		/// Override when adding new style subclasses.
		/// Returns TRUE if the styles were initialized or re-initialized
		/// </summary>
		internal bool Init()
		{
			if (initialized && initializedAsInterfont == DeGUI.usesInterFont)
			{
				return false;
			}
			initialized = true;
			initializedAsInterfont = DeGUI.usesInterFont;
			Vector2 contentOffset = GUI.skin.button.contentOffset;
			if (DeUnityEditorVersion.MajorVersion >= 2018)
			{
				EditorStyles.toolbarButton.contentOffset = new Vector2(1f, 0f);
			}
			box.Init();
			button.Init();
			label.Init();
			toolbar.Init();
			misc.Init();
			FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.FieldType.IsSubclassOf(typeof(DeStyleSubPalette)))
				{
					((DeStyleSubPalette)fieldInfo.GetValue(this)).Init();
				}
			}
			if (DeUnityEditorVersion.MajorVersion >= 2018)
			{
				EditorStyles.toolbarButton.contentOffset = contentOffset;
			}
			return true;
		}

		private static Texture2D LoadTexture(ref Texture2D property, string name, FilterMode filterMode = FilterMode.Point, int maxTextureSize = 32, TextureWrapMode wrapMode = TextureWrapMode.Clamp)
		{
			if (property == null)
			{
				property = AssetDatabase.LoadAssetAtPath($"{_adbImgsDir}{name}.png", typeof(Texture2D)) as Texture2D;
				if (property == null)
				{
					property = AssetDatabase.LoadAssetAtPath($"Packages/com.demigiant.demilib/Plugins/Editor/Imgs/{name}.png", typeof(Texture2D)) as Texture2D;
				}
				property.SetGUIFormat(filterMode, maxTextureSize, wrapMode);
			}
			return property;
		}
	}
}
