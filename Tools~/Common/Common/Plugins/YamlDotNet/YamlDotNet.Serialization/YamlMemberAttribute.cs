using System;
using AIO.YamlDotNet.Core;

#pragma warning disable CS1591

namespace AIO.YamlDotNet.Serialization
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class YamlMemberAttribute : Attribute
    {
        private DefaultValuesHandling? defaultValuesHandling;

        public string? Description { get; set; }

        public Type? SerializeAs { get; set; }

        public int Order { get; set; }

        public string? Alias { get; set; }

        public bool ApplyNamingConventions { get; set; }

        public ScalarStyle ScalarStyle { get; set; }

        internal DefaultValuesHandling DefaultValuesHandling
        {
            get { return defaultValuesHandling.GetValueOrDefault(); }
            set { defaultValuesHandling = value; }
        }

        public bool IsDefaultValuesHandlingSpecified => defaultValuesHandling.HasValue;

        public YamlMemberAttribute()
        {
            ScalarStyle = ScalarStyle.Any;
            ApplyNamingConventions = true;
        }

        public YamlMemberAttribute(Type serializeAs)
            : this()
        {
            SerializeAs = serializeAs ?? throw new ArgumentNullException("serializeAs");
        }
    }
}