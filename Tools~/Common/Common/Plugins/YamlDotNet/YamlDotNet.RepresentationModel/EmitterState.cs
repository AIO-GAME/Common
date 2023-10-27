using System.Collections.Generic;
using YamlDotNet.Core;

namespace YamlDotNet.RepresentationModel
{
	internal class EmitterState
	{
		public HashSet<AnchorName> EmittedAnchors { get; } = new HashSet<AnchorName>();

	}
}
