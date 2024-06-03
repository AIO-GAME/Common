namespace AIO.YamlDotNet.Serialization
{
	internal interface ITrackingRegistrationLocationSelectionSyntax<TBaseRegistrationType>
	{
		void InsteadOf<TRegistrationType>() where TRegistrationType : TBaseRegistrationType;
	}
}
