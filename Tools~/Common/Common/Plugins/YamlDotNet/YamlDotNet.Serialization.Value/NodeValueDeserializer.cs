#nullable enable
#pragma warning disable

using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;
using AIO.YamlDotNet.Serialization.Utilities;

namespace AIO.YamlDotNet.Serialization.ValueDeserializers
{
    internal sealed class NodeValueDeserializer : IValueDeserializer
    {
        private readonly IList<INodeDeserializer> deserializers;

        private readonly IList<INodeTypeResolver> typeResolvers;

        private readonly ITypeConverter typeConverter;

        public NodeValueDeserializer(IList<INodeDeserializer> deserializers, IList<INodeTypeResolver> typeResolvers,
            ITypeConverter typeConverter)
        {
            this.deserializers = deserializers ?? throw new ArgumentNullException("deserializers");
            this.typeResolvers = typeResolvers ?? throw new ArgumentNullException("typeResolvers");
            this.typeConverter = typeConverter ?? throw new ArgumentNullException("typeConverter");
        }

        public object? DeserializeValue(IParser parser, Type expectedType, SerializerState state,
            IValueDeserializer nestedObjectDeserializer)
        {
            IValueDeserializer nestedObjectDeserializer2 = nestedObjectDeserializer;
            SerializerState state2 = state;
            parser.Accept<NodeEvent>(out var @event);
            Type typeFromEvent = GetTypeFromEvent(@event, expectedType);
            Mark start;
            Mark end;
            try
            {
                foreach (INodeDeserializer deserializer in deserializers)
                {
                    if (deserializer.Deserialize(parser, typeFromEvent,
                            (IParser r, Type t) =>
                                nestedObjectDeserializer2.DeserializeValue(r, t, state2, nestedObjectDeserializer2),
                            out var value))
                    {
                        return typeConverter.ChangeType(value, expectedType);
                    }
                }
            }
            catch (YamlException)
            {
                throw;
            }
            catch (Exception innerException)
            {
                start = @event?.Start ?? Mark.Empty;
                end = @event?.End ?? Mark.Empty;
                throw new YamlException(in start, in end, "Exception during deserialization", innerException);
            }

            start = @event?.Start ?? Mark.Empty;
            end = @event?.End ?? Mark.Empty;
            throw new YamlException(in start, in end,
                "No node deserializer was able to deserialize the node into type " +
                expectedType.AssemblyQualifiedName);
        }

        private Type GetTypeFromEvent(NodeEvent? nodeEvent, Type currentType)
        {
            foreach (INodeTypeResolver typeResolver in typeResolvers)
            {
                if (typeResolver.Resolve(nodeEvent, ref currentType))
                {
                    return currentType;
                }
            }

            return currentType;
        }
    }
}
