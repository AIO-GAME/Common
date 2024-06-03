using System;
using System.IO;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.RepresentationModel
{
	internal class LibYamlEventStream
	{
		private readonly IParser parser;

		public LibYamlEventStream(IParser iParser)
		{
			parser = iParser ?? throw new ArgumentNullException("iParser");
		}

		public void WriteTo(TextWriter textWriter)
		{
			while (parser.MoveNext())
			{
				ParsingEvent current = parser.Current;
				if (!(current is AnchorAlias anchorAlias))
				{
					if (!(current is DocumentEnd documentEnd))
					{
						if (!(current is DocumentStart documentStart))
						{
							if (!(current is MappingEnd))
							{
								if (!(current is MappingStart nodeEvent))
								{
									if (!(current is Scalar scalar))
									{
										if (!(current is SequenceEnd))
										{
											if (!(current is SequenceStart nodeEvent2))
											{
												if (!(current is StreamEnd))
												{
													if (current is StreamStart)
													{
														textWriter.Write("+STR");
													}
												}
												else
												{
													textWriter.Write("-STR");
												}
											}
											else
											{
												textWriter.Write("+SEQ");
												WriteAnchorAndTag(textWriter, nodeEvent2);
											}
										}
										else
										{
											textWriter.Write("-SEQ");
										}
									}
									else
									{
										textWriter.Write("=VAL");
										WriteAnchorAndTag(textWriter, scalar);
										switch (scalar.Style)
										{
										case ScalarStyle.DoubleQuoted:
											textWriter.Write(" \"");
											break;
										case ScalarStyle.SingleQuoted:
											textWriter.Write(" '");
											break;
										case ScalarStyle.Folded:
											textWriter.Write(" >");
											break;
										case ScalarStyle.Literal:
											textWriter.Write(" |");
											break;
										default:
											textWriter.Write(" :");
											break;
										}
										string value = scalar.Value;
										foreach (char c in value)
										{
											switch (c)
											{
											case '\b':
												textWriter.Write("\\b");
												break;
											case '\t':
												textWriter.Write("\\t");
												break;
											case '\n':
												textWriter.Write("\\n");
												break;
											case '\r':
												textWriter.Write("\\r");
												break;
											case '\\':
												textWriter.Write("\\\\");
												break;
											default:
												textWriter.Write(c);
												break;
											}
										}
									}
								}
								else
								{
									textWriter.Write("+MAP");
									WriteAnchorAndTag(textWriter, nodeEvent);
								}
							}
							else
							{
								textWriter.Write("-MAP");
							}
						}
						else
						{
							textWriter.Write("+DOC");
							if (!documentStart.IsImplicit)
							{
								textWriter.Write(" ---");
							}
						}
					}
					else
					{
						textWriter.Write("-DOC");
						if (!documentEnd.IsImplicit)
						{
							textWriter.Write(" ...");
						}
					}
				}
				else
				{
					textWriter.Write("=ALI *");
					textWriter.Write(anchorAlias.Value);
				}
				textWriter.WriteLine();
			}
		}

		private void WriteAnchorAndTag(TextWriter textWriter, NodeEvent nodeEvent)
		{
			if (!nodeEvent.Anchor.IsEmpty)
			{
				textWriter.Write(" &");
				textWriter.Write(nodeEvent.Anchor);
			}
			if (!nodeEvent.Tag.IsEmpty)
			{
				textWriter.Write(" <");
				textWriter.Write(nodeEvent.Tag.Value);
				textWriter.Write(">");
			}
		}
	}
}
