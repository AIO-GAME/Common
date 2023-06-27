using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core
{
	/// <summary>
	/// Stores cloned nodes for pasting
	/// </summary>
	internal class NodesClipboard
	{
		public readonly List<IEditorGUINode> currClones = new List<IEditorGUINode>();

		private NodeProcess _process;

		private readonly string _copyBufferId;

		private readonly List<IEditorGUINode> _originalNodes = new List<IEditorGUINode>();

		private readonly Dictionary<IEditorGUINode, NodeConnectionOptions> _originalNodeToConnectionOptions = new Dictionary<IEditorGUINode, NodeConnectionOptions>();

		private readonly Dictionary<IEditorGUINode, NodeGUIData> _originalNodeToGuiData = new Dictionary<IEditorGUINode, NodeGUIData>();

		private readonly Dictionary<string, string> _originalIdToCloneId = new Dictionary<string, string>();

		private readonly Dictionary<IEditorGUINode, IEditorGUINode> _cloneToOriginalNode = new Dictionary<IEditorGUINode, IEditorGUINode>();

		private Func<IEditorGUINode, IEditorGUINode, bool> _onCloneNodeCallback;

		private bool _requiresRecloningOnNextPaste;

		public bool hasContent
		{
			get
			{
				if (EditorGUIUtility.systemCopyBuffer == _copyBufferId)
				{
					return _originalNodes.Count > 0;
				}
				return false;
			}
		}

		public NodesClipboard(NodeProcess process)
		{
			_process = process;
			_copyBufferId = string.Concat("[[[[!?NODES?!]]]]", Guid.NewGuid().ToString());
		}

		public void Clear()
		{
			currClones.Clear();
			_originalNodes.Clear();
			_originalIdToCloneId.Clear();
			_originalNodeToConnectionOptions.Clear();
			_originalNodeToGuiData.Clear();
			_cloneToOriginalNode.Clear();
			_requiresRecloningOnNextPaste = false;
		}

		public void Add(IEditorGUINode node, IEditorGUINode clone, NodeConnectionOptions connectionOptions, Func<IEditorGUINode, IEditorGUINode, bool> onCloneNodeCallback)
		{
			_originalNodes.Add(node);
			_originalIdToCloneId.Add(node.id, clone.id);
			_originalNodeToConnectionOptions.Add(node, connectionOptions);
			_originalNodeToGuiData.Add(node, _process.nodeToGUIData[node]);
			_cloneToOriginalNode.Add(clone, node);
			_onCloneNodeCallback = onCloneNodeCallback;
			currClones.Add(clone);
			EditorGUIUtility.systemCopyBuffer = _copyBufferId;
		}

		/// <summary>
		/// Returns a list of pasteable nodes, with their GUID recreated and their connections adapted
		/// </summary>
		/// <returns></returns>
		public List<T> GetNodesToPaste<T>() where T : IEditorGUINode, new()
		{
			if (_originalNodes.Count == 0)
			{
				return null;
			}
			List<T> list = new List<T>();
			if (_requiresRecloningOnNextPaste)
			{
				currClones.Clear();
				_originalIdToCloneId.Clear();
				_cloneToOriginalNode.Clear();
				foreach (IEditorGUINode originalNode in _originalNodes)
				{
					T val = CloneNode<T>(originalNode);
					if (_onCloneNodeCallback == null || _onCloneNodeCallback(originalNode, val))
					{
						_cloneToOriginalNode.Add(val, originalNode);
						_originalIdToCloneId[originalNode.id] = val.id;
						currClones.Add(val);
						list.Add(val);
					}
				}
			}
			else
			{
				foreach (IEditorGUINode currClone in currClones)
				{
					list.Add((T)currClone);
				}
			}
			foreach (IEditorGUINode originalNode2 in _originalNodes)
			{
				NodeConnectionOptions nodeConnectionOptions = _originalNodeToConnectionOptions[originalNode2];
				IEditorGUINode cloneByOriginalId = GetCloneByOriginalId(originalNode2.id);
				for (int num = cloneByOriginalId.connectedNodesIds.Count - 1; num > -1; num--)
				{
					string text = cloneByOriginalId.connectedNodesIds[num];
					if (IsConnectionToCopiedNode(text))
					{
						cloneByOriginalId.connectedNodesIds[num] = _originalIdToCloneId[text];
					}
					else if (nodeConnectionOptions.connectionMode == ConnectionMode.Flexible)
					{
						cloneByOriginalId.connectedNodesIds.RemoveAt(num);
					}
					else
					{
						cloneByOriginalId.connectedNodesIds[num] = null;
					}
				}
			}
			_requiresRecloningOnNextPaste = true;
			return list;
		}

		/// <summary>
		/// Returns a deep clone of the given node but doesn't clone UnityEngine references.
		/// A new ID will be automatically generated.
		/// </summary>
		public T CloneNode<T>(IEditorGUINode node) where T : IEditorGUINode, new()
		{
			T result = (T)CloneObject(node);
			result.id = Guid.NewGuid().ToString();
			return result;
		}

		private object CloneObject(object original)
		{
			if (original == null)
			{
				return null;
			}
			Type type = original.GetType();
			if (type.IsValueType || type == typeof(string) || (type.Namespace != null && type.Namespace.StartsWith("UnityEngine")))
			{
				return original;
			}
			object obj = Activator.CreateInstance(type);
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			FieldInfo[] fields2 = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			FieldInfo[] array = fields;
			foreach (FieldInfo srcField in array)
			{
				FieldInfo fieldInfo = fields2.FirstOrDefault((FieldInfo field) => field.Name == srcField.Name);
				if (fieldInfo == null || fieldInfo.IsLiteral || srcField.FieldType != fieldInfo.FieldType)
				{
					continue;
				}
				object value = srcField.GetValue(original);
				if (value == null)
				{
					continue;
				}
				Type type2 = value.GetType();
				if (type2.IsArray)
				{
					string text = type2.FullName.Replace("[]", string.Empty);
					Type type3 = Type.GetType(text);
					if (type3 == null)
					{
						type3 = Type.GetType(string.Concat(text, ", Assembly-CSharp-firstpass"));
						if (type3 == null)
						{
							type3 = Type.GetType(string.Concat(text, ", Assembly-CSharp"));
						}
					}
					Array array2 = value as Array;
					Array array3 = Array.CreateInstance(type3, array2.Length);
					for (int j = 0; j < array2.Length; j++)
					{
						array3.SetValue(CloneObject(array2.GetValue(j)), j);
					}
					fieldInfo.SetValue(obj, array3);
				}
				else if (type2.IsGenericType)
				{
					Type type4 = Type.GetType(type2.FullName.Replace("[]", string.Empty));
					if (type4 == null)
					{
						Debug.LogWarning($"Couldn't clone correctly the {srcField.Name} field, a shallow copy will be used");
						fieldInfo.SetValue(obj, CloneObject(srcField.GetValue(original)));
						continue;
					}
					IList list = value as IList;
					IList list2 = Activator.CreateInstance(type4) as IList;
					for (int k = 0; k < list.Count; k++)
					{
						list2.Add(CloneObject(list[k]));
					}
					fieldInfo.SetValue(obj, list2);
				}
				else
				{
					fieldInfo.SetValue(obj, CloneObject(srcField.GetValue(original)));
				}
			}
			return obj;
		}

		public IEditorGUINode GetCloneByOriginalId(string id)
		{
			string text = _originalIdToCloneId[id];
			foreach (IEditorGUINode currClone in currClones)
			{
				if (currClone.id == text)
				{
					return currClone;
				}
			}
			return null;
		}

		public NodeGUIData GetGuiDataByCloneId(string cloneId)
		{
			foreach (IEditorGUINode currClone in currClones)
			{
				if (currClone.id == cloneId)
				{
					return _originalNodeToGuiData[_cloneToOriginalNode[currClone]];
				}
			}
			return default(NodeGUIData);
		}

		public NodeConnectionOptions GetConnectionOptionsByCloneId(string cloneId)
		{
			foreach (IEditorGUINode currClone in currClones)
			{
				if (currClone.id == cloneId)
				{
					return _originalNodeToConnectionOptions[_cloneToOriginalNode[currClone]];
				}
			}
			return default(NodeConnectionOptions);
		}

		private bool IsConnectionToCopiedNode(string connectionId)
		{
			foreach (IEditorGUINode originalNode in _originalNodes)
			{
				if (originalNode.id == connectionId)
				{
					return true;
				}
			}
			return false;
		}
	}
}
