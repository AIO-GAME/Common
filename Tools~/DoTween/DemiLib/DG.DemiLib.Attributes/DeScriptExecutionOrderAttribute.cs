using System;

namespace DG.DemiLib.Attributes
{
	/// <summary>
	/// <code>Class attribute</code><para />
	/// Sets the script execution order index
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class DeScriptExecutionOrderAttribute : Attribute
	{
		internal int order;

		/// <summary>
		/// Sets the script execution order for this class
		/// </summary>
		/// <param name="order">Script execution order index</param>
		public DeScriptExecutionOrderAttribute(int order)
		{
			this.order = order;
		}
	}
}
