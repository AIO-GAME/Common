using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Serialization.Schemas;

namespace AIO.YamlDotNet.Serialization.EventEmitters
{
	internal sealed class TypeAssigningEventEmitter : ChainedEventEmitter
	{
		private readonly bool requireTagWhenStaticAndActualTypesAreDifferent;

		private readonly IDictionary<Type, TagName> tagMappings;

		private readonly bool quoteNecessaryStrings;

		private readonly Regex? isSpecialStringValue_Regex;

		private static readonly string SpecialStrings_Pattern = "^(null|Null|NULL|\\~|true|True|TRUE|false|False|FALSE|[-+]?[0-9]+|0o[0-7]+|0x[0-9a-fA-F]+|[-+]?(\\.[0-9]+|[0-9]+(\\.[0-9]*)?)([eE][-+]?[0-9]+)?|[-+]?(\\.inf|\\.Inf|\\.INF)|\\.nan|\\.NaN|\\.NAN)$";

		private static readonly string CombinedYaml1_1SpecialStrings_Pattern = "^(null|Null|NULL|\\~|true|True|TRUE|false|False|FALSE|y|Y|yes|Yes|YES|n|N|no|No|NO|on|On|ON|off|Off|OFF|[-+]?0b[0-1_]+|[-+]?0o?[0-7_]+|[-+]?(0|[1-9][0-9_]*)|[-+]?0x[0-9a-fA-F_]+|[-+]?[1-9][0-9_]*(:[0-5]?[0-9])+|[-+]?([0-9][0-9_]*)?\\.[0-9_]*([eE][-+][0-9]+)?|[-+]?[0-9][0-9_]*(:[0-5]?[0-9])+\\.[0-9_]*|[-+]?\\.(inf|Inf|INF)|\\.(nan|NaN|NAN))$";

		public TypeAssigningEventEmitter(IEventEmitter nextEmitter, bool requireTagWhenStaticAndActualTypesAreDifferent, IDictionary<Type, TagName> tagMappings, bool quoteNecessaryStrings, bool quoteYaml1_1Strings)
			: this(nextEmitter, requireTagWhenStaticAndActualTypesAreDifferent, tagMappings)
		{
			this.quoteNecessaryStrings = quoteNecessaryStrings;
			string pattern = (quoteYaml1_1Strings ? CombinedYaml1_1SpecialStrings_Pattern : SpecialStrings_Pattern);
			isSpecialStringValue_Regex = new Regex(pattern, RegexOptions.Compiled);
		}

		public TypeAssigningEventEmitter(IEventEmitter nextEmitter, bool requireTagWhenStaticAndActualTypesAreDifferent, IDictionary<Type, TagName> tagMappings, bool quoteNecessaryStrings)
			: this(nextEmitter, requireTagWhenStaticAndActualTypesAreDifferent, tagMappings)
		{
			this.quoteNecessaryStrings = quoteNecessaryStrings;
			isSpecialStringValue_Regex = new Regex(SpecialStrings_Pattern, RegexOptions.Compiled);
		}

		public TypeAssigningEventEmitter(IEventEmitter nextEmitter, bool requireTagWhenStaticAndActualTypesAreDifferent, IDictionary<Type, TagName> tagMappings)
			: base(nextEmitter)
		{
			this.requireTagWhenStaticAndActualTypesAreDifferent = requireTagWhenStaticAndActualTypesAreDifferent;
			this.tagMappings = tagMappings ?? throw new ArgumentNullException("tagMappings");
		}

		public override void Emit(ScalarEventInfo eventInfo, IEmitter emitter)
		{
			ScalarStyle style = ScalarStyle.Plain;
			object value = eventInfo.Source.Value;
			if (value == null)
			{
				eventInfo.Tag = JsonSchema.Tags.Null;
				eventInfo.RenderedValue = "";
			}
			else
			{
				TypeCode typeCode = eventInfo.Source.Type.GetTypeCode();
				switch (typeCode)
				{
				case TypeCode.Boolean:
					eventInfo.Tag = JsonSchema.Tags.Bool;
					eventInfo.RenderedValue = YamlFormatter.FormatBoolean(value);
					break;
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					if (eventInfo.Source.Type.IsEnum)
					{
						eventInfo.Tag = FailsafeSchema.Tags.Str;
						eventInfo.RenderedValue = value.ToString();
						style = ((quoteNecessaryStrings && IsSpecialStringValue(eventInfo.RenderedValue)) ? ScalarStyle.DoubleQuoted : ScalarStyle.Any);
					}
					else
					{
						eventInfo.Tag = JsonSchema.Tags.Int;
						eventInfo.RenderedValue = YamlFormatter.FormatNumber(value);
					}
					break;
				case TypeCode.Single:
					eventInfo.Tag = JsonSchema.Tags.Float;
					eventInfo.RenderedValue = YamlFormatter.FormatNumber((float)value);
					break;
				case TypeCode.Double:
					eventInfo.Tag = JsonSchema.Tags.Float;
					eventInfo.RenderedValue = YamlFormatter.FormatNumber((double)value);
					break;
				case TypeCode.Decimal:
					eventInfo.Tag = JsonSchema.Tags.Float;
					eventInfo.RenderedValue = YamlFormatter.FormatNumber(value);
					break;
				case TypeCode.Char:
				case TypeCode.String:
					eventInfo.Tag = FailsafeSchema.Tags.Str;
					eventInfo.RenderedValue = value.ToString();
					style = ((quoteNecessaryStrings && IsSpecialStringValue(eventInfo.RenderedValue)) ? ScalarStyle.DoubleQuoted : ScalarStyle.Any);
					break;
				case TypeCode.DateTime:
					eventInfo.Tag = DefaultSchema.Tags.Timestamp;
					eventInfo.RenderedValue = YamlFormatter.FormatDateTime(value);
					break;
				case TypeCode.Empty:
					eventInfo.Tag = JsonSchema.Tags.Null;
					eventInfo.RenderedValue = "";
					break;
				default:
					if (eventInfo.Source.Type == typeof(TimeSpan))
					{
						eventInfo.RenderedValue = YamlFormatter.FormatTimeSpan(value);
						break;
					}
					throw new NotSupportedException($"TypeCode.{typeCode} is not supported.");
				}
			}
			eventInfo.IsPlainImplicit = true;
			if (eventInfo.Style == ScalarStyle.Any)
			{
				eventInfo.Style = style;
			}
			base.Emit(eventInfo, emitter);
		}

		public override void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
		{
			AssignTypeIfNeeded(eventInfo);
			base.Emit(eventInfo, emitter);
		}

		public override void Emit(SequenceStartEventInfo eventInfo, IEmitter emitter)
		{
			AssignTypeIfNeeded(eventInfo);
			base.Emit(eventInfo, emitter);
		}

		private void AssignTypeIfNeeded(ObjectEventInfo eventInfo)
		{
			if (tagMappings.TryGetValue(eventInfo.Source.Type, out var value))
			{
				eventInfo.Tag = value;
			}
			else if (requireTagWhenStaticAndActualTypesAreDifferent && eventInfo.Source.Value != null && eventInfo.Source.Type != eventInfo.Source.StaticType)
			{
				throw new YamlException("Cannot serialize type '" + eventInfo.Source.Type.FullName + "' where a '" + eventInfo.Source.StaticType.FullName + "' was expected because no tag mapping has been registered for '" + eventInfo.Source.Type.FullName + "', which means that it won't be possible to deserialize the document.\nRegister a tag mapping using the SerializerBuilder.WithTagMapping method.\n\nE.g: builder.WithTagMapping(\"!" + eventInfo.Source.Type.Name + "\", typeof(" + eventInfo.Source.Type.FullName + "));");
			}
		}

		private bool IsSpecialStringValue(string value)
		{
			if (value.Trim() == string.Empty)
			{
				return true;
			}
			return isSpecialStringValue_Regex?.IsMatch(value) ?? false;
		}
	}
}
