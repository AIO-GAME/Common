using System;
using System.Globalization;
using System.Linq;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization.Converters
{
	internal class DateTimeConverter : IYamlTypeConverter
	{
		private readonly DateTimeKind kind;

		private readonly IFormatProvider provider;

		private readonly bool doubleQuotes;

		private readonly string[] formats;

		public DateTimeConverter(DateTimeKind kind = DateTimeKind.Utc, IFormatProvider? provider = null, bool doubleQuotes = false, params string[] formats)
		{
			this.kind = ((kind == DateTimeKind.Unspecified) ? DateTimeKind.Utc : kind);
			this.provider = provider ?? CultureInfo.InvariantCulture;
			this.doubleQuotes = doubleQuotes;
			this.formats = formats.DefaultIfEmpty<string>("G").ToArray();
		}

		public bool Accepts(Type type)
		{
			return type == typeof(DateTime);
		}

		public object ReadYaml(IParser parser, Type type)
		{
			return EnsureDateTimeKind(DateTime.ParseExact(parser.Consume<Scalar>().Value, style: (kind == DateTimeKind.Local) ? DateTimeStyles.AssumeLocal : DateTimeStyles.AssumeUniversal, formats: formats, provider: provider), kind);
		}

		public void WriteYaml(IEmitter emitter, object? value, Type type)
		{
			DateTime dateTime = (DateTime)value;
			string value2 = ((kind == DateTimeKind.Local) ? dateTime.ToLocalTime() : dateTime.ToUniversalTime()).ToString(formats.First(), provider);
			emitter.Emit(new Scalar(AnchorName.Empty, TagName.Empty, value2, doubleQuotes ? ScalarStyle.DoubleQuoted : ScalarStyle.Any, isPlainImplicit: true, isQuotedImplicit: false));
		}

		private static DateTime EnsureDateTimeKind(DateTime dt, DateTimeKind kind)
		{
			if (dt.Kind == DateTimeKind.Local && kind == DateTimeKind.Utc)
			{
				return dt.ToUniversalTime();
			}
			if (dt.Kind == DateTimeKind.Utc && kind == DateTimeKind.Local)
			{
				return dt.ToLocalTime();
			}
			return dt;
		}
	}
}
