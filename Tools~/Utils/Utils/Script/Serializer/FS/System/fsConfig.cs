namespace AIO
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Enables some top-level customization of Full Serializer.
    /// </summary>
    public class fsConfig
    {
        /// <summary>
        /// The attributes that will force a field or property to be serialized.
        /// </summary>
        public Type[] SerializeAttributes =
        {
            typeof(fsPropertyAttribute),
            typeof(SerializeAttribute),
            typeof(SerializeAsAttribute)
        };

        /// <summary>
        /// The attributes that will force a field or property to *not* be
        /// serialized.
        /// </summary>
        public Type[] IgnoreSerializeAttributes =
        {
            typeof(NonSerializedAttribute),
            typeof(fsIgnoreAttribute),
            typeof(DoNotSerializeAttribute)
        };

        /// <summary>
        /// The default member serialization.
        /// </summary>
        public fsMemberSerialization DefaultMemberSerialization = fsMemberSerialization.Default;

        /// <summary>
        /// Convert a C# field/property name into the key used for the JSON
        /// object. For example, you could force all JSON names to lowercase
        /// with:
        /// fsConfig.GetJsonNameFromMemberName = (name, info) =&gt;
        /// name.ToLower();
        /// This will only be used when the name is not explicitly specified with
        /// fsProperty.
        /// </summary>
        public Func<string, MemberInfo, string> GetJsonNameFromMemberName = (name, info) => name;

        /// <summary>
        /// If false, then *all* property serialization support will be disabled
        /// - even properties explicitly annotated with fsProperty or any other
        /// opt-in annotation.
        /// Setting this to false means that SerializeNonAutoProperties and
        /// SerializeNonPublicSetProperties will be completely ignored.
        /// </summary>
        public bool EnablePropertySerialization = true;

        /// <summary>
        /// Should the default serialization behaviour include non-auto
        /// properties?
        /// </summary>
        public bool SerializeNonAutoProperties = false;

        /// <summary>
        /// Should the default serialization behaviour include properties with
        /// non-public setters?
        /// </summary>
        public bool SerializeNonPublicSetProperties = true;

        /// <summary>
        /// If not null, this string format will be used for DateTime instead of
        /// the default one.
        /// </summary>
        public string CustomDateTimeFormatString = null;

        /// <summary>
        /// Int64 and UInt64 will be serialized and deserialized as string for
        /// compatibility
        /// </summary>
        public bool Serialize64BitIntegerAsString = false;

        /// <summary>
        /// Enums are serialized using their names by default. Setting this to
        /// true will serialize them as integers instead.
        /// </summary>
        public bool SerializeEnumsAsInteger = false;
    }
}