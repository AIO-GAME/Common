using System;
using System.IO;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization
{
	internal sealed class Serializer : ISerializer
	{
		private readonly IValueSerializer valueSerializer;

		private readonly EmitterSettings emitterSettings;

		public Serializer()
			: this(new SerializerBuilder().BuildValueSerializer(), EmitterSettings.Default)
		{
		}

		private Serializer(IValueSerializer valueSerializer, EmitterSettings emitterSettings)
		{
			this.valueSerializer = valueSerializer ?? throw new ArgumentNullException("valueSerializer");
			this.emitterSettings = emitterSettings ?? throw new ArgumentNullException("emitterSettings");
		}

		public static Serializer FromValueSerializer(IValueSerializer valueSerializer, EmitterSettings emitterSettings)
		{
			return new Serializer(valueSerializer, emitterSettings);
		}

		public void Serialize(TextWriter writer, object? graph)
		{
			Serialize(new Emitter(writer, emitterSettings), graph);
		}

		public string Serialize(object? graph)
		{
			using StringWriter stringWriter = new StringWriter();
			Serialize(stringWriter, graph);
			return stringWriter.ToString();
		}

		public void Serialize(TextWriter writer, object? graph, Type type)
		{
			Serialize(new Emitter(writer, emitterSettings), graph, type);
		}

		public void Serialize(IEmitter emitter, object? graph)
		{
			if (emitter == null)
			{
				throw new ArgumentNullException("emitter");
			}
			EmitDocument(emitter, graph, null);
		}

		public void Serialize(IEmitter emitter, object? graph, Type type)
		{
			if (emitter == null)
			{
				throw new ArgumentNullException("emitter");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			EmitDocument(emitter, graph, type);
		}

		private void EmitDocument(IEmitter emitter, object? graph, Type? type)
		{
			emitter.Emit(new StreamStart());
			emitter.Emit(new DocumentStart());
			valueSerializer.SerializeValue(emitter, graph, type);
			emitter.Emit(new DocumentEnd(isImplicit: true));
			emitter.Emit(new StreamEnd());
		}
	}
}
