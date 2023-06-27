using System;
using UnityEngine;

namespace DG.DemiLib
{
	/// <summary>
	/// Extend this to replicate Unity's Scope system with any Unity version.
	/// Thanks to Dmitriy Yukhanov for pointing this out and creating an initial version.
	/// Expand this class to create scopes.<para />
	/// Example:
	/// <code>public class VBoxScope : DeScope
	/// {
	///     public VBoxScope(GUIStyle style)
	///     {
	///         BeginVBox(style);
	///     }
	///
	///     protected override void CloseScope()
	///     { 
	///         EndVBox();
	///     }
	/// }</code>
	/// Usage:
	/// <code>using (new VBoxScope(myStyle) {
	///     // Do something
	/// }</code>
	/// </summary>
	public abstract class DeScope : IDisposable
	{
		private bool _disposed;

		protected abstract void CloseScope();

		~DeScope()
		{
			if (!_disposed)
			{
				Debug.LogError("Scope was not disposed! You should use the 'using' keyword or manually call Dispose.");
				Dispose();
			}
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;
				CloseScope();
			}
		}
	}
}
