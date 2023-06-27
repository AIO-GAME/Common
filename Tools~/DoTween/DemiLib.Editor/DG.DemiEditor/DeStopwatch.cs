using System.Diagnostics;

namespace DG.DemiEditor
{
	/// <summary>
	/// A stopwatch whose time can be changed manually via <see cref="M:DG.DemiEditor.DeStopwatch.Goto(System.Single,System.Boolean)" />
	/// </summary>
	public class DeStopwatch
	{
		private readonly Stopwatch _sw = new Stopwatch();

		private float _offset;

		public float elapsed => (float)_sw.ElapsedMilliseconds * 0.001f + _offset;

		/// <summary>
		/// Start or resume playing
		/// </summary>
		public void Start()
		{
			_sw.Start();
		}

		/// <summary>
		/// Stop the watch and reset the time
		/// </summary>
		public void Reset()
		{
			_offset = 0f;
			_sw.Reset();
		}

		/// <summary>
		/// Restart measuring from zero
		/// </summary>
		public void Restart()
		{
			Reset();
			Start();
		}

		/// <summary>
		/// Pause the watch
		/// </summary>
		public void Stop()
		{
			_sw.Stop();
		}

		/// <summary>
		/// Send the watch to the given time
		/// </summary>
		public void Goto(float seconds, bool andPlay = false)
		{
			_offset = seconds - (float)_sw.ElapsedMilliseconds * 0.001f;
			if (!andPlay)
			{
				_sw.Stop();
			}
			else
			{
				_sw.Start();
			}
		}
	}
}
