using System;
using DG.DemiLib.Attributes;
using UnityEditor;

namespace DG.DemiEditor.AttributesManagers
{
	[InitializeOnLoad]
	public class DeScriptExecutionOrderManager
	{
		static DeScriptExecutionOrderManager()
		{
			MonoScript[] allRuntimeMonoScripts = MonoImporter.GetAllRuntimeMonoScripts();
			foreach (MonoScript monoScript in allRuntimeMonoScripts)
			{
				if (monoScript.GetClass() == null)
				{
					continue;
				}
				Attribute[] customAttributes = Attribute.GetCustomAttributes(monoScript.GetClass(), typeof(DeScriptExecutionOrderAttribute));
				foreach (Attribute attribute in customAttributes)
				{
					int executionOrder = MonoImporter.GetExecutionOrder(monoScript);
					int order = ((DeScriptExecutionOrderAttribute)attribute).order;
					if (executionOrder != order)
					{
						MonoImporter.SetExecutionOrder(monoScript, order);
					}
				}
			}
		}
	}
}
