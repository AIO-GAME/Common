using System;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization
{
	internal interface INodeTypeResolver
	{
		bool Resolve(NodeEvent? nodeEvent, ref Type currentType);
	}
}
