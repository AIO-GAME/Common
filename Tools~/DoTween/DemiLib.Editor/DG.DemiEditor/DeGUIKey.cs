using System.Collections.Generic;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Returns key modifiers currently pressed.
	/// Requires to be updated at the beginning of every GUI call.
	/// </summary>
	public static class DeGUIKey
	{
		public static class Extra
		{
			public static bool space { get; internal set; }
		}

		public static class Exclusive
		{
			public static bool shift
			{
				get
				{
					if (DeGUIKey.shift && !DeGUIKey.ctrl)
					{
						return !DeGUIKey.alt;
					}
					return false;
				}
			}

			public static bool ctrl
			{
				get
				{
					if (DeGUIKey.ctrl && !DeGUIKey.shift)
					{
						return !DeGUIKey.alt;
					}
					return false;
				}
			}

			public static bool alt
			{
				get
				{
					if (DeGUIKey.alt && !DeGUIKey.ctrl)
					{
						return !DeGUIKey.shift;
					}
					return false;
				}
			}

			public static bool softShift
			{
				get
				{
					if (DeGUIKey.softShift && !DeGUIKey.ctrl)
					{
						return !DeGUIKey.alt;
					}
					return false;
				}
			}

			public static bool softCtrl
			{
				get
				{
					if (DeGUIKey.softCtrl && !DeGUIKey.shift)
					{
						return !DeGUIKey.alt;
					}
					return false;
				}
			}

			public static bool softAlt
			{
				get
				{
					if (DeGUIKey.softAlt && !DeGUIKey.shift)
					{
						return !DeGUIKey.ctrl;
					}
					return false;
				}
			}

			public static bool ctrlShiftAlt => DeGUIKey.ctrlShiftAlt;

			public static bool ctrlShift
			{
				get
				{
					if (DeGUIKey.ctrl && DeGUIKey.shift)
					{
						return !DeGUIKey.alt;
					}
					return false;
				}
			}

			public static bool ctrlAlt
			{
				get
				{
					if (DeGUIKey.ctrl && DeGUIKey.alt)
					{
						return !DeGUIKey.shift;
					}
					return false;
				}
			}

			public static bool shiftAlt
			{
				get
				{
					if (DeGUIKey.shift && DeGUIKey.alt)
					{
						return !DeGUIKey.ctrl;
					}
					return false;
				}
			}

			public static bool softCtrlShiftAlt => DeGUIKey.softCtrlShiftAlt;

			public static bool softCtrlShift
			{
				get
				{
					if (DeGUIKey.softCtrl && DeGUIKey.softShift)
					{
						return !DeGUIKey.alt;
					}
					return false;
				}
			}

			public static bool softCtrlAlt
			{
				get
				{
					if (DeGUIKey.softCtrl && DeGUIKey.softAlt)
					{
						return !DeGUIKey.shift;
					}
					return false;
				}
			}

			public static bool softShiftAlt
			{
				get
				{
					if (DeGUIKey.softShift && DeGUIKey.softAlt)
					{
						return !DeGUIKey.ctrl;
					}
					return false;
				}
			}
		}

		public static class Toggled
		{
			public static bool tab { get; internal set; }
		}

		public struct Keys
		{
			public bool shift;

			public bool ctrl;

			public bool alt;

			public bool space;

			public void Refresh(bool shift, bool ctrl, bool alt, bool space)
			{
				this.shift = shift;
				this.ctrl = ctrl;
				this.alt = alt;
				this.space = space;
			}
		}

		public struct KeysRefreshResult
		{
			public Keys pressed;

			public Keys released;
		}

		private const float _SoftDelay = 0.2f;

		private static float _timeAtShiftKeyRelease;

		private static float _timeAtCtrlKeyRelease;

		private static float _timeAtAltKeyRelease;

		private static readonly Dictionary<string, Keys> _idToDownKeysAtLastPass = new Dictionary<string, Keys>();

		public static bool shift => Event.current.shift;

		public static bool ctrl
		{
			get
			{
				if (!Event.current.control)
				{
					return Event.current.command;
				}
				return true;
			}
		}

		public static bool alt => Event.current.alt;

		public static bool none
		{
			get
			{
				if (!ctrl && !shift)
				{
					return !alt;
				}
				return false;
			}
		}

		public static bool softShift
		{
			get
			{
				if (!Event.current.shift)
				{
					return Time.realtimeSinceStartup - _timeAtShiftKeyRelease < 0.2f;
				}
				return true;
			}
		}

		public static bool softCtrl
		{
			get
			{
				if (!Event.current.control && !Event.current.command)
				{
					return Time.realtimeSinceStartup - _timeAtCtrlKeyRelease < 0.2f;
				}
				return true;
			}
		}

		public static bool softAlt
		{
			get
			{
				if (!Event.current.alt)
				{
					return Time.realtimeSinceStartup - _timeAtAltKeyRelease < 0.2f;
				}
				return true;
			}
		}

		public static bool ctrlShiftAlt
		{
			get
			{
				if (ctrl && shift)
				{
					return alt;
				}
				return false;
			}
		}

		public static bool ctrlShift
		{
			get
			{
				if (ctrl)
				{
					return shift;
				}
				return false;
			}
		}

		public static bool ctrlAlt
		{
			get
			{
				if (ctrl)
				{
					return alt;
				}
				return false;
			}
		}

		public static bool shiftAlt
		{
			get
			{
				if (shift)
				{
					return alt;
				}
				return false;
			}
		}

		public static bool softCtrlShiftAlt
		{
			get
			{
				if (softCtrl && softShift)
				{
					return softAlt;
				}
				return false;
			}
		}

		public static bool softCtrlShift
		{
			get
			{
				if (softCtrl)
				{
					return softShift;
				}
				return false;
			}
		}

		public static bool softCtrlAlt
		{
			get
			{
				if (softCtrl)
				{
					return softAlt;
				}
				return false;
			}
		}

		public static bool softShiftAlt
		{
			get
			{
				if (softShift)
				{
					return softAlt;
				}
				return false;
			}
		}

		/// <summary>
		/// Call this method to update data required by softCtrl calculations.
		/// Automatically called from within a <see cref="T:DG.DemiEditor.DeGUINodeSystem.NodeProcessScope`1" />.<para />
		/// Returns a <see cref="T:DG.DemiEditor.DeGUIKey.KeysRefreshResult" /> object with the keys that were just pressed and just released
		/// </summary>
		/// <param name="id">Required to have the correct <see cref="T:DG.DemiEditor.DeGUIKey.KeysRefreshResult" /> for the given target call</param>
		public static KeysRefreshResult Refresh(string id)
		{
			if (!_idToDownKeysAtLastPass.ContainsKey(id))
			{
				_idToDownKeysAtLastPass.Add(id, default(Keys));
			}
			Keys keys = _idToDownKeysAtLastPass[id];
			KeysRefreshResult result = default(KeysRefreshResult);
			if (Event.current.type == EventType.KeyDown)
			{
				if (Event.current.keyCode == KeyCode.Space)
				{
					Extra.space = (result.pressed.space = true);
				}
				result.pressed.shift = shift && !keys.shift;
				result.pressed.ctrl = ctrl && !keys.ctrl;
				result.pressed.alt = alt && !keys.alt;
				if (Event.current.keyCode == KeyCode.Tab)
				{
					Toggled.tab = !Toggled.tab;
				}
			}
			else if (Event.current.rawType == EventType.KeyUp)
			{
				switch (Event.current.keyCode)
				{
				case KeyCode.RightShift:
				case KeyCode.LeftShift:
					result.released.shift = true;
					_timeAtShiftKeyRelease = Time.realtimeSinceStartup;
					break;
				case KeyCode.RightControl:
				case KeyCode.LeftControl:
				case KeyCode.RightMeta:
				case KeyCode.LeftMeta:
					result.released.ctrl = true;
					_timeAtCtrlKeyRelease = Time.realtimeSinceStartup;
					break;
				case KeyCode.RightAlt:
				case KeyCode.LeftAlt:
					result.released.alt = true;
					_timeAtAltKeyRelease = Time.realtimeSinceStartup;
					break;
				case KeyCode.Space:
					Extra.space = (result.released.space = false);
					break;
				}
			}
			keys.Refresh(shift, ctrl, alt, Extra.space);
			return result;
		}

		/// <summary>
		/// Returns the given <see cref="T:UnityEngine.KeyCode" /> as an int, or -1 if it's not a number
		/// </summary>
		public static int ToInt(KeyCode keycode)
		{
			switch (keycode)
			{
			case KeyCode.Alpha0:
			case KeyCode.Keypad0:
				return 0;
			case KeyCode.Alpha1:
			case KeyCode.Keypad1:
				return 1;
			case KeyCode.Alpha2:
			case KeyCode.Keypad2:
				return 2;
			case KeyCode.Alpha3:
			case KeyCode.Keypad3:
				return 3;
			case KeyCode.Alpha4:
			case KeyCode.Keypad4:
				return 4;
			case KeyCode.Alpha5:
			case KeyCode.Keypad5:
				return 5;
			case KeyCode.Alpha6:
			case KeyCode.Keypad6:
				return 6;
			case KeyCode.Alpha7:
			case KeyCode.Keypad7:
				return 7;
			case KeyCode.Alpha8:
			case KeyCode.Keypad8:
				return 8;
			case KeyCode.Alpha9:
			case KeyCode.Keypad9:
				return 9;
			default:
				return -1;
			}
		}
	}
}
