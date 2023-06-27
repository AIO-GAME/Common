using System;
using UnityEngine;

namespace DG.DemiLib
{
	/// <summary>
	/// Content colors
	/// </summary>
	[Serializable]
	public class DeColorContent
	{
		public DeSkinColor def = new DeSkinColor(Color.black, new Color(0.7f, 0.7f, 0.7f, 1f));

		public DeSkinColor critical = new DeSkinColor(new Color(1f, 0.9148073f, 19f / 34f, 1f), new Color(1f, 0.3881846f, 0.3014706f, 1f));

		public DeSkinColor toggleOn = new DeSkinColor(new Color(1f, 0.9686275f, 0.6980392f, 1f), new Color(0.8025267f, 1f, 0.4705882f, 1f));

		public DeSkinColor toggleOff = new DeSkinColor(new Color(0.4117647f, 0.3360727f, 0.3360727f, 1f), new Color(0.6470588f, 0.5185986f, 0.5185986f, 1f));

		public DeSkinColor toggleCritical = new DeSkinColor(new Color(1f, 0.84f, 0.62f), new Color(1f, 0.84f, 0.62f));
	}
}
