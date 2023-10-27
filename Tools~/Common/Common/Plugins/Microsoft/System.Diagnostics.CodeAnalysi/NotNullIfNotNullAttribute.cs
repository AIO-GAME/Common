namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		public string ParameterName { get; }

		public NotNullIfNotNullAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}
	}
}