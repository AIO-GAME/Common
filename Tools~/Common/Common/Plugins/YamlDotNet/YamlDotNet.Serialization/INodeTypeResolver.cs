using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
	internal interface INodeTypeResolver
	{
		bool Resolve(NodeEvent? nodeEvent, ref Type currentType);
	}
}
