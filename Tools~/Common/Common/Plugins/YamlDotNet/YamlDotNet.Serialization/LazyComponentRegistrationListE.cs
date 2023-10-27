using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization
{
	internal static class LazyComponentRegistrationListExtensions
	{
		public static TComponent BuildComponentChain<TComponent>(this LazyComponentRegistrationList<TComponent, TComponent> registrations, TComponent innerComponent)
		{
			return registrations.InReverseOrder.Aggregate(innerComponent, (TComponent inner, Func<TComponent, TComponent> factory) => factory(inner));
		}

		public static TComponent BuildComponentChain<TArgument, TComponent>(this LazyComponentRegistrationList<TArgument, TComponent> registrations, TComponent innerComponent, Func<TComponent, TArgument> argumentBuilder)
		{
			Func<TComponent, TArgument> argumentBuilder2 = argumentBuilder;
			return registrations.InReverseOrder.Aggregate(innerComponent, (TComponent inner, Func<TArgument, TComponent> factory) => factory(argumentBuilder2(inner)));
		}

		public static List<TComponent> BuildComponentList<TComponent>(this LazyComponentRegistrationList<Nothing, TComponent> registrations)
		{
			return registrations.Select((Func<Nothing, TComponent> factory) => factory(default(Nothing))).ToList();
		}

		public static List<TComponent> BuildComponentList<TArgument, TComponent>(this LazyComponentRegistrationList<TArgument, TComponent> registrations, TArgument argument)
		{
			TArgument argument2 = argument;
			return registrations.Select((Func<TArgument, TComponent> factory) => factory(argument2)).ToList();
		}
	}
}
