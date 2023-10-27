#nullable enable
using System;
using System.Globalization;

namespace YamlDotNet
{
	internal sealed class CultureInfoAdapter : CultureInfo
	{
		private readonly IFormatProvider provider;

		public CultureInfoAdapter(CultureInfo baseCulture, IFormatProvider provider)
			: base(baseCulture.LCID)
		{
			this.provider = provider;
		}

		public override object? GetFormat(Type? formatType)
		{
			return provider.GetFormat(formatType);
		}
	}
}
