using System.Collections.Generic;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core.DebugSystem
{
	internal class NodeProcessDebug
	{
		internal class DataStore
		{
			/// <summary>Layout, Repaint, LayoutAndRepaint</summary>
			private readonly Data[] _eventData;

			public float avrgFps_Layout => _eventData[0].avrgFps;

			public float avrgFps_Repaint => _eventData[1].avrgFps;

			public float avrgFps_LayoutAndRepaint => _eventData[2].avrgFps;

			public float avrgDrawTime_Layout => _eventData[0].avrgMs;

			public float avrgDrawTime_Repaint => _eventData[1].avrgMs;

			public float avrgDrawTime_LayoutAndRepaint => _eventData[2].avrgMs;

			public DataStore()
			{
				_eventData = new Data[3]
				{
					new Data(),
					new Data(),
					new Data()
				};
			}

			public void OnGUIStart()
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				switch (Event.current.type)
				{
				case EventType.Layout:
					_eventData[0].OnGUIStart(realtimeSinceStartup);
					_eventData[2].OnGUIStart(realtimeSinceStartup);
					break;
				case EventType.Repaint:
					_eventData[1].OnGUIStart(realtimeSinceStartup);
					break;
				}
			}

			public void OnGUIEnd()
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				switch (Event.current.type)
				{
				case EventType.Layout:
					_eventData[0].OnGUIEnd(realtimeSinceStartup);
					break;
				case EventType.Repaint:
					_eventData[1].OnGUIEnd(realtimeSinceStartup);
					_eventData[2].OnGUIEnd(realtimeSinceStartup);
					break;
				}
			}
		}

		private class Data
		{
			private const int _MaxToStore = 30;

			private readonly List<float> _elapsedTimes = new List<float>(30);

			private readonly List<float> _avrgFpsOverTime = new List<float>(30);

			private readonly List<float> _avrgMsOverTime = new List<float>(30);

			private float _guiStartTime;

			public float avrgFps { get; private set; }

			public float avrgMs { get; private set; }

			public void OnGUIStart(float time)
			{
				_guiStartTime = time;
			}

			public void OnGUIEnd(float time)
			{
				float item = time - _guiStartTime;
				if (_elapsedTimes.Count > 30)
				{
					_elapsedTimes.RemoveAt(0);
				}
				_elapsedTimes.Add(item);
				StoreAverageFps();
			}

			private void StoreAverageFps()
			{
				int count = _elapsedTimes.Count;
				if (count == 0)
				{
					float num3 = (avrgFps = (avrgMs = 0f));
					return;
				}
				float num4 = 0f;
				for (int i = 0; i < count; i++)
				{
					num4 += _elapsedTimes[i];
				}
				avrgFps = 1f / (num4 / (float)count);
				if (_avrgFpsOverTime.Count > 30)
				{
					_avrgFpsOverTime.RemoveAt(0);
				}
				_avrgFpsOverTime.Add(avrgFps);
				avrgMs = num4 / (float)count * 1000f;
				if (_avrgMsOverTime.Count > 30)
				{
					_avrgMsOverTime.RemoveAt(0);
				}
				_avrgMsOverTime.Add(avrgMs);
			}
		}

		public readonly DataStore panningData = new DataStore();

		private DataStore _currDataStore;

		public void Draw(Rect processArea)
		{
			NodeProcessDebugGUI.Draw(this, processArea);
		}

		public void OnNodeProcessStart(InteractionManager.State interactionState)
		{
			if (interactionState == InteractionManager.State.Panning)
			{
				_currDataStore = panningData;
			}
			if (_currDataStore != null)
			{
				_currDataStore.OnGUIStart();
			}
		}

		public void OnNodeProcessEnd()
		{
			if (_currDataStore != null)
			{
				_currDataStore.OnGUIEnd();
				_currDataStore = null;
			}
		}
	}
}
