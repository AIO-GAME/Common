//namespace AIO
//{
//    using System;
//    using System.Collections.Specialized;
//    using System.Configuration;

//    internal class TestSettingsProvider : SettingsProvider
//    {
//        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
//        {
//            var collection1 = new SettingsPropertyValueCollection();
//            foreach (SettingsPropertyValue value in collection)
//            {
//                var propertyName = value.Property.Name;
//                if (!HasPropertyElement(propertyName))
//                {
//                    throw new SettingsPropertyNotFoundException($"The property '{propertyName}' was not found.");
//                }
//            }
//            return collection1;
//        }

//        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection values)
//        {
//            foreach (SettingsPropertyValue value in values)
//            {
//                var propertyName = value.Property.Name;
//                if (!HasPropertyElement(propertyName))
//                {
//                    throw new SettingsPropertyNotFoundException($"The property '{propertyName}' was not found.");
//                }
//            }
//        }

//        private bool HasPropertyElement(string propertyName)
//        {
//            bool hasPropertyElement = false;

//            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//            ConfigurationSectionGroup sectionGroup = config.GetSectionGroup("applicationSettings");

//            if (sectionGroup == null)
//            {
//                return hasPropertyElement;
//            }

//            ConfigurationSection section = sectionGroup.Sections["TestSettingsProvider"];

//            if (section == null)
//            {
//                return hasPropertyElement;
//            }

//            NameValueCollection settings = section.ElementInformation.Properties["Settings"].Value as NameValueCollection;

//            if (settings == null)
//            {
//                return hasPropertyElement;
//            }

//            foreach (string setting in settings.Keys)
//            {
//                if (setting == propertyName)
//                {
//                    hasPropertyElement = true;
//                    break;
//                }
//            }

//            return hasPropertyElement;
//        }
//    }
//}
