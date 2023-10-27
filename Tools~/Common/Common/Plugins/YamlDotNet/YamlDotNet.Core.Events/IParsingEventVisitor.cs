namespace YamlDotNet.Core.Events
{
	internal interface IParsingEventVisitor
	{
		void Visit(AnchorAlias e);

		void Visit(StreamStart e);

		void Visit(StreamEnd e);

		void Visit(DocumentStart e);

		void Visit(DocumentEnd e);

		void Visit(Scalar e);

		void Visit(SequenceStart e);

		void Visit(SequenceEnd e);

		void Visit(MappingStart e);

		void Visit(MappingEnd e);

		void Visit(Comment e);
	}
}
