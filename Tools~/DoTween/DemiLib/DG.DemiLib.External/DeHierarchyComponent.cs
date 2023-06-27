using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG.DemiLib.External
{
	/// <summary>
	/// Used by DeHierarchy
	/// </summary>
	public class DeHierarchyComponent : MonoBehaviour
	{
		public enum HColor
		{
			None,
			Blue,
			Green,
			Orange,
			Purple,
			Red,
			Yellow,
			BrightGrey,
			DarkGrey,
			Black,
			White
		}

		public enum IcoType
		{
			Dot,
			Star,
			Cog,
			Comment,
			UI,
			Play,
			Heart,
			Skull,
			Camera,
			Light
		}

		public enum SeparatorType
		{
			None,
			Top,
			Bottom
		}

		[Serializable]
		public class CustomizedItem
		{
			public GameObject gameObject;

			public HColor hColor = HColor.BrightGrey;

			public IcoType icoType;

			public SeparatorType separatorType;

			public HColor separatorHColor = HColor.Black;

			public CustomizedItem(GameObject gameObject, HColor hColor)
			{
				this.gameObject = gameObject;
				this.hColor = hColor;
			}

			public CustomizedItem(GameObject gameObject, IcoType icoType)
			{
				this.gameObject = gameObject;
				this.icoType = icoType;
			}

			public CustomizedItem(GameObject gameObject, SeparatorType separatorType, HColor separatorHColor)
			{
				this.gameObject = gameObject;
				this.separatorType = separatorType;
				this.separatorHColor = separatorHColor;
			}

			public Color GetColor()
			{
				return DeHierarchyComponent.GetColor(hColor);
			}

			public Color GetSeparatorColor()
			{
				return DeHierarchyComponent.GetColor(separatorHColor);
			}
		}

		public List<CustomizedItem> customizedItems = new List<CustomizedItem>();

		/// <summary>
		/// Returns a list of all items whose gameObject is NULL, or NULL if there's no missing gameObjects.
		/// </summary>
		public List<int> MissingItemsIndexes()
		{
			List<int> list = null;
			for (int num = customizedItems.Count - 1; num > -1; num--)
			{
				if (customizedItems[num].gameObject == null)
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(num);
				}
			}
			return list;
		}

		/// <summary>
		/// If the item exists sets it, otherwise first creates it and then sets it
		/// </summary>
		public void StoreItemColor(GameObject go, HColor hColor)
		{
			for (int i = 0; i < customizedItems.Count; i++)
			{
				if (!(customizedItems[i].gameObject != go))
				{
					customizedItems[i].hColor = hColor;
					return;
				}
			}
			customizedItems.Add(new CustomizedItem(go, hColor));
		}

		/// <summary>
		/// If the item exists sets it, otherwise first creates it and then sets it
		/// </summary>
		public void StoreItemIcon(GameObject go, IcoType icoType)
		{
			for (int i = 0; i < customizedItems.Count; i++)
			{
				if (!(customizedItems[i].gameObject != go))
				{
					customizedItems[i].icoType = icoType;
					return;
				}
			}
			customizedItems.Add(new CustomizedItem(go, icoType));
		}

		/// <summary>
		/// If the item exists sets it, otherwise first creates it and then sets it
		/// </summary>
		public void StoreItemSeparator(GameObject go, SeparatorType? separatorType, HColor? separatorHColor)
		{
			for (int i = 0; i < customizedItems.Count; i++)
			{
				if (!(customizedItems[i].gameObject != go))
				{
					if (separatorType.HasValue)
					{
						customizedItems[i].separatorType = separatorType.Value;
					}
					if (separatorHColor.HasValue)
					{
						customizedItems[i].separatorHColor = separatorHColor.Value;
					}
					return;
				}
			}
			customizedItems.Add(new CustomizedItem(go, separatorType.HasValue ? separatorType.Value : SeparatorType.None, separatorHColor.HasValue ? separatorHColor.Value : HColor.None));
		}

		/// <summary>
		/// Returns TRUE if the item existed and was removed.
		/// </summary>
		public bool RemoveItemData(GameObject go)
		{
			int num = -1;
			for (int i = 0; i < customizedItems.Count; i++)
			{
				if (customizedItems[i].gameObject == go)
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				customizedItems.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns TRUE if the item existed and was changed.
		/// </summary>
		public bool ResetSeparator(GameObject go)
		{
			for (int i = 0; i < customizedItems.Count; i++)
			{
				if (!(customizedItems[i].gameObject != go))
				{
					customizedItems[i].separatorType = SeparatorType.None;
					customizedItems[i].separatorHColor = HColor.None;
					if (customizedItems[i].hColor == HColor.None)
					{
						customizedItems.RemoveAt(i);
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Returns the customizedItem for the given gameObject, or NULL if none was found
		/// </summary>
		public CustomizedItem GetItem(GameObject go)
		{
			for (int i = 0; i < customizedItems.Count; i++)
			{
				if (customizedItems[i].gameObject == go)
				{
					return customizedItems[i];
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the color corresponding to the given <see cref="T:DG.DemiLib.External.DeHierarchyComponent.HColor" />
		/// </summary>
		public static Color GetColor(HColor color)
		{
			switch (color)
			{
			case HColor.Blue:
				return new Color(0.2145329f, 0.4501492f, 31f / 34f, 1f);
			case HColor.Green:
				return new Color(0.05060553f, 117f / 136f, 0.2237113f, 1f);
			case HColor.Orange:
				return new Color(65f / 68f, 0.4471125f, 0.05622837f, 1f);
			case HColor.Purple:
				return new Color(0.907186f, 0.05406574f, 125f / 136f, 1f);
			case HColor.Red:
				return new Color(125f / 136f, 0.1617312f, 0.07434041f, 1f);
			case HColor.Yellow:
				return new Color(1f, 0.853854f, 0.03676468f, 1f);
			case HColor.BrightGrey:
				return new Color(0.6470588f, 0.6470588f, 0.6470588f, 1f);
			case HColor.DarkGrey:
				return new Color(0.3308824f, 0.3308824f, 0.3308824f, 1f);
			case HColor.Black:
				return Color.black;
			case HColor.White:
				return Color.white;
			default:
				return Color.white;
			}
		}
	}
}
