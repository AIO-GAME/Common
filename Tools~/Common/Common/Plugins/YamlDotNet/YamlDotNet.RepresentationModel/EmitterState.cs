using System.Collections.Generic;
using AIO.YamlDotNet.Core;

namespace AIO.YamlDotNet.RepresentationModel
{
	internal class EmitterState
	{
		public HashSet<AnchorName> EmittedAnchors { get; } = new HashSet<AnchorName>();

	}
}
