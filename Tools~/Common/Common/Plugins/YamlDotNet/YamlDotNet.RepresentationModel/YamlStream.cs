using System.Collections;
using System.Collections.Generic;
using System.IO;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.RepresentationModel
{
	internal class YamlStream : IEnumerable<YamlDocument>, IEnumerable
	{
		private readonly IList<YamlDocument> documents = new List<YamlDocument>();

		public IList<YamlDocument> Documents => documents;

		public YamlStream()
		{
		}

		public YamlStream(params YamlDocument[] documents)
			: this((IEnumerable<YamlDocument>)documents)
		{
		}

		public YamlStream(IEnumerable<YamlDocument> documents)
		{
			foreach (YamlDocument document in documents)
			{
				this.documents.Add(document);
			}
		}

		public void Add(YamlDocument document)
		{
			documents.Add(document);
		}

		public void Load(TextReader input)
		{
			Load(new Parser(input));
		}

		public void Load(IParser parser)
		{
			documents.Clear();
			parser.Consume<StreamStart>();
			StreamEnd @event;
			while (!parser.TryConsume<StreamEnd>(out @event))
			{
				YamlDocument item = new YamlDocument(parser);
				documents.Add(item);
			}
		}

		public void Save(TextWriter output)
		{
			Save(output, assignAnchors: true);
		}

		public void Save(TextWriter output, bool assignAnchors)
		{
			Save(new Emitter(output), assignAnchors);
		}

		public void Save(IEmitter emitter, bool assignAnchors)
		{
			emitter.Emit(new StreamStart());
			foreach (YamlDocument document in documents)
			{
				document.Save(emitter, assignAnchors);
			}
			emitter.Emit(new StreamEnd());
		}

		public void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}

		public IEnumerator<YamlDocument> GetEnumerator()
		{
			return documents.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
