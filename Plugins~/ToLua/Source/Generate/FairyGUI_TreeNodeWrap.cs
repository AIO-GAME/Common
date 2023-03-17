﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class FairyGUI_TreeNodeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(FairyGUI.TreeNode), typeof(System.Object));
		L.RegFunction("AddChild", AddChild);
		L.RegFunction("AddChildAt", AddChildAt);
		L.RegFunction("RemoveChild", RemoveChild);
		L.RegFunction("RemoveChildAt", RemoveChildAt);
		L.RegFunction("RemoveChildren", RemoveChildren);
		L.RegFunction("GetChildAt", GetChildAt);
		L.RegFunction("GetChildIndex", GetChildIndex);
		L.RegFunction("GetPrevSibling", GetPrevSibling);
		L.RegFunction("GetNextSibling", GetNextSibling);
		L.RegFunction("SetChildIndex", SetChildIndex);
		L.RegFunction("SwapChildren", SwapChildren);
		L.RegFunction("SwapChildrenAt", SwapChildrenAt);
		L.RegFunction("New", _CreateFairyGUI_TreeNode);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("data", get_data, set_data);
		L.RegVar("parent", get_parent, null);
		L.RegVar("tree", get_tree, null);
		L.RegVar("cell", get_cell, null);
		L.RegVar("level", get_level, null);
		L.RegVar("expanded", get_expanded, set_expanded);
		L.RegVar("isFolder", get_isFolder, null);
		L.RegVar("text", get_text, null);
		L.RegVar("numChildren", get_numChildren, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFairyGUI_TreeNode(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				bool arg0 = LuaDLL.luaL_checkboolean(L, 1);
				FairyGUI.TreeNode obj = new FairyGUI.TreeNode(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: FairyGUI.TreeNode.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddChild(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			FairyGUI.TreeNode arg0 = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 2);
			FairyGUI.TreeNode o = obj.AddChild(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddChildAt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			FairyGUI.TreeNode arg0 = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			FairyGUI.TreeNode o = obj.AddChildAt(arg0, arg1);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveChild(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			FairyGUI.TreeNode arg0 = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 2);
			FairyGUI.TreeNode o = obj.RemoveChild(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveChildAt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			FairyGUI.TreeNode o = obj.RemoveChildAt(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveChildren(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
				obj.RemoveChildren();
				return 0;
			}
			else if (count == 2)
			{
				FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				obj.RemoveChildren(arg0);
				return 0;
			}
			else if (count == 3)
			{
				FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				obj.RemoveChildren(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: FairyGUI.TreeNode.RemoveChildren");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetChildAt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			FairyGUI.TreeNode o = obj.GetChildAt(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetChildIndex(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			FairyGUI.TreeNode arg0 = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 2);
			int o = obj.GetChildIndex(arg0);
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPrevSibling(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			FairyGUI.TreeNode o = obj.GetPrevSibling();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNextSibling(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			FairyGUI.TreeNode o = obj.GetNextSibling();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetChildIndex(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			FairyGUI.TreeNode arg0 = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.SetChildIndex(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SwapChildren(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			FairyGUI.TreeNode arg0 = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 2);
			FairyGUI.TreeNode arg1 = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 3);
			obj.SwapChildren(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SwapChildrenAt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)ToLua.CheckObject<FairyGUI.TreeNode>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.SwapChildrenAt(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_data(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			object ret = obj.data;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index data on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_parent(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			FairyGUI.TreeNode ret = obj.parent;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index parent on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tree(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			FairyGUI.TreeView ret = obj.tree;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index tree on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cell(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			FairyGUI.GComponent ret = obj.cell;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index cell on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			int ret = obj.level;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index level on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_expanded(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			bool ret = obj.expanded;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index expanded on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isFolder(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			bool ret = obj.isFolder;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index isFolder on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_text(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			string ret = obj.text;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index text on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_numChildren(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			int ret = obj.numChildren;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index numChildren on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_data(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.data = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index data on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_expanded(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.TreeNode obj = (FairyGUI.TreeNode)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.expanded = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index expanded on a nil value");
		}
	}
}

