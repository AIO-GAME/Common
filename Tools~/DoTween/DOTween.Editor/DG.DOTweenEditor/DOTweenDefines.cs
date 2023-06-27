namespace DG.DOTweenEditor
{
	/// <summary>
	/// Not used as menu item anymore, but as a utiity function
	/// </summary>
	internal static class DOTweenDefines
	{
		public const string GlobalDefine_Legacy_AudioModule = "DOTAUDIO";

		public const string GlobalDefine_Legacy_PhysicsModule = "DOTPHYSICS";

		public const string GlobalDefine_Legacy_Physics2DModule = "DOTPHYSICS2D";

		public const string GlobalDefine_Legacy_SpriteModule = "DOTSPRITE";

		public const string GlobalDefine_Legacy_UIModule = "DOTUI";

		public const string GlobalDefine_Legacy_TK2D = "DOTWEEN_TK2D";

		public const string GlobalDefine_Legacy_TextMeshPro = "DOTWEEN_TMP";

		public const string GlobalDefine_Legacy_NoRigidbody = "DOTWEEN_NORBODY";

		public static void RemoveAllDefines()
		{
		}

		public static void RemoveAllLegacyDefines()
		{
			EditorUtils.RemoveGlobalDefine("DOTAUDIO");
			EditorUtils.RemoveGlobalDefine("DOTPHYSICS");
			EditorUtils.RemoveGlobalDefine("DOTPHYSICS2D");
			EditorUtils.RemoveGlobalDefine("DOTSPRITE");
			EditorUtils.RemoveGlobalDefine("DOTUI");
			EditorUtils.RemoveGlobalDefine("DOTWEEN_NORBODY");
			EditorUtils.RemoveGlobalDefine("DOTWEEN_TK2D");
			EditorUtils.RemoveGlobalDefine("DOTWEEN_TMP");
		}
	}
}
