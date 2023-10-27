using System;
using System.IO;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.Serialization
{
	internal interface ISerializer
	{
		void Serialize(TextWriter writer, object? graph);

		string Serialize(object? graph);

		void Serialize(TextWriter writer, object? graph, Type type);

		void Serialize(IEmitter emitter, object? graph);

		void Serialize(IEmitter emitter, object? graph, Type type);
	}
}
