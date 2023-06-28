using System.Collections.Generic;
using Borodar.RainbowCore;
using UnityEngine;

namespace Borodar.RainbowFolders
{
	public static class ProjectEditorTexturesStorage
	{
		private static readonly Dictionary<ProjectEditorTexture, Texture2D> EDITOR_TEXTURES = new Dictionary<ProjectEditorTexture, Texture2D>();

		private static readonly Dictionary<ProjectEditorTexture, string> EDITOR_STRINGS = new Dictionary<ProjectEditorTexture, string>
		{
			{
				ProjectEditorTexture.IcnEditFreeSmall,
				"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACNklEQVQ4EZ1TS28SURT+BgbCawoLBlKBxA2vRa07lBYK9hewI/ws09/AHkI31qQVEmClxiZiqwkLgRDkPcpDgfGeQ4O6qUlvcuY+zne+c+a750rlcrkNQBH2kKHJIurRQyLvYhQi0KbTqTIej7FYLHZcdrsdLpcLNN8zNFnXdXS7XYRCIXi93h221Wqh1+v9jwCypmkSRZlMJvT7/R0B7UulEu/T6fTunBZWq5WrUxQFcqfTMQQCAQgiBhUKBUiSBKPRiEwmw2f1eh3r9ZrX2WwWw+GQjQno1Gw2M6BYLCIej8PpdEIQIxwOc9BgMICqetAX88uzM5ykT2HYrNgnRJRw8/kLfokMo8kEBpE5EonA7/dDlkljIBaLweFw4MP1NfaUPRjo/OeOQMfjYIiBnxofQeKFgkHel87PeX4hNJgI8tubW8znc2w2GxjYA3CKjgi6fH2BxPERnhwcsAbVWh1v371nmNVqQ/z5MySTCThdTlxevMJpOsU+WRcTCUSss9mMM5GIDoddCLnNQ+vlcsk+qgCgqO3gClarFY5PUqhW3vDdJxMJPD08hM1mYxT9EolarlTwVVR7lEzdhUOIud5gtV6xUSWqqvIdN5tNqG43G62pK8lHmPVme6XEIlOZ86kGs8UC0ZQYiDuu1mr4JrowGo1ypkajAdXjwXA0Ysz8x3e49/fZJ4lu0/9+B1dXVywi6eDz+RjUbrdFoM6WSqX+6UTpvuecz+ctxJDL5f68MqbcfbTfVFv3mcgD6YAAAAAASUVORK5CYII="
			},
			{
				ProjectEditorTexture.IcnEditFreeLarge,
				"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"
			},
			{
				ProjectEditorTexture.IcnEditProSmall,
				"iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAIAAACQkWg2AAAB3UlEQVQoFZWSy07CQBSGLZ1WyoDCAgm40A2sRFZAiIaa6BPoc/BU+g4aIfGy8hINNWxkIQmNImAEStPaC63/UOPC6MJJ5uTMP+fMd2bOcMViceE/I/SfYBZLMD3Pc13X9/0gORQK8TwPGyx/WJaA6FKplEgkgr3BYNBsNv9MwPHYI4RomhYkiKIImm3b30zo31jiOE6hUDBNE+poNIJFcq1Wg9NoNACHk0qlJpNJq9ViR6NcQRDAQbQsy8lkcjQel8tlxBmGEYvFeq+v9XpDilDEsOMwn1967vziEUorlYqu64BAr1ar0Wj04vIS1uO+3oBwHLeSyWBbe3/HdYGGf3h0BHuwv49lt9u1bJuIIhQMPp1OE0Hs93p7e7s7sozyjk9Ozs7Pu6rqe34ul11fWwPwQVEs6wMOQ6Mc3/dMw8R5AFJKgzelUWpZFkTTMPyFry4xgkSpIC52np70qb66mtnM59GT/MbG9tbWcDg8Pa3f3N6GpYg+1RhhNh/guK6ztLwUj8c7nU4um4UCBw8KEVvcvBZ2B0hEIPgN3mwmhcN4xMd2W5IkVHJ1fa2qar/fH769OY7rWBZ6wCDT8Tho6v3dHe4A9WPeR0VRgA+6Dh2RIHC/fm/EMTrPw/4Yn2B+96QzeXGxAAAAAElFTkSuQmCC"
			},
			{
				ProjectEditorTexture.IcnEditProLarge,
				"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"
			}
		};

		public static Texture2D GetTexture(ProjectEditorTexture type)
		{
			return CoreTexturesStorageHelper<ProjectEditorTexture>.GetTexture(type, FilterMode.Point, EDITOR_STRINGS, EDITOR_TEXTURES);
		}
	}
}
