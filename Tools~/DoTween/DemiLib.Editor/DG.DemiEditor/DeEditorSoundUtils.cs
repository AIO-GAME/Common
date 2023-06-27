using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	public static class DeEditorSoundUtils
	{
		private static MethodInfo _miPlay;

		private static MethodInfo _miStop;

		private static MethodInfo _miStopAll;

		/// <summary>
		/// Plays the given clip in the Editor
		/// </summary>
		public static void Play(AudioClip audioClip)
		{
			if (audioClip == null)
			{
				return;
			}
			if (_miPlay == null)
			{
				Type type = Assembly.GetAssembly(typeof(EditorWindow)).CreateInstance("UnityEditor.AudioUtil").GetType();
				if (DeUnityEditorVersion.MajorVersion < 2019)
				{
					_miPlay = type.GetMethod("PlayClip", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, Type.DefaultBinder, new Type[1] { typeof(AudioClip) }, null);
				}
				else
				{
					_miPlay = type.GetMethod("PlayClip", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, Type.DefaultBinder, new Type[3]
					{
						typeof(AudioClip),
						typeof(int),
						typeof(bool)
					}, null);
				}
			}
			if (DeUnityEditorVersion.MajorVersion < 2019)
			{
				_miPlay.Invoke(null, new object[1] { audioClip });
				return;
			}
			_miPlay.Invoke(null, new object[3] { audioClip, 0, false });
		}

		/// <summary>
		/// Stops playing the given clip.
		/// </summary>
		public static void Stop(AudioClip audioClip)
		{
			if (!(audioClip == null))
			{
				Assembly assembly = Assembly.GetAssembly(typeof(EditorWindow));
				if (_miStop == null)
				{
					_miStop = assembly.CreateInstance("UnityEditor.AudioUtil").GetType().GetMethod("StopClip", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, Type.DefaultBinder, new Type[1] { typeof(AudioClip) }, null);
				}
				_miStop.Invoke(null, new object[1] { audioClip });
			}
		}

		/// <summary>
		/// Stops all clips playing.
		/// </summary>
		public static void StopAll()
		{
			Assembly assembly = Assembly.GetAssembly(typeof(EditorWindow));
			if (_miStopAll == null)
			{
				_miStopAll = assembly.CreateInstance("UnityEditor.AudioUtil").GetType().GetMethod("StopAllClips", BindingFlags.Static | BindingFlags.Public);
			}
			_miStopAll.Invoke(null, null);
		}
	}
}
