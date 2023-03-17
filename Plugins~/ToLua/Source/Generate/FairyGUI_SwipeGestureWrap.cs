﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class FairyGUI_SwipeGestureWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(FairyGUI.SwipeGesture), typeof(FairyGUI.EventDispatcher));
		L.RegFunction("Dispose", Dispose);
		L.RegFunction("Enable", Enable);
		L.RegFunction("New", _CreateFairyGUI_SwipeGesture);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("velocity", get_velocity, set_velocity);
		L.RegVar("position", get_position, set_position);
		L.RegVar("delta", get_delta, set_delta);
		L.RegVar("actionDistance", get_actionDistance, set_actionDistance);
		L.RegVar("snapping", get_snapping, set_snapping);
		L.RegVar("ACTION_DISTANCE", get_ACTION_DISTANCE, set_ACTION_DISTANCE);
		L.RegVar("host", get_host, null);
		L.RegVar("onBegin", get_onBegin, null);
		L.RegVar("onEnd", get_onEnd, null);
		L.RegVar("onMove", get_onMove, null);
		L.RegVar("onAction", get_onAction, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFairyGUI_SwipeGesture(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				FairyGUI.GObject arg0 = (FairyGUI.GObject)ToLua.CheckObject<FairyGUI.GObject>(L, 1);
				FairyGUI.SwipeGesture obj = new FairyGUI.SwipeGesture(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: FairyGUI.SwipeGesture.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Dispose(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)ToLua.CheckObject<FairyGUI.SwipeGesture>(L, 1);
			obj.Dispose();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Enable(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)ToLua.CheckObject<FairyGUI.SwipeGesture>(L, 1);
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.Enable(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_velocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			UnityEngine.Vector2 ret = obj.velocity;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index velocity on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_position(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			UnityEngine.Vector2 ret = obj.position;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index position on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_delta(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			UnityEngine.Vector2 ret = obj.delta;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index delta on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_actionDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			int ret = obj.actionDistance;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index actionDistance on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_snapping(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			bool ret = obj.snapping;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index snapping on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ACTION_DISTANCE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushinteger(L, FairyGUI.SwipeGesture.ACTION_DISTANCE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_host(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			FairyGUI.GObject ret = obj.host;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index host on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onBegin(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			FairyGUI.EventListener ret = obj.onBegin;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onBegin on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onEnd(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			FairyGUI.EventListener ret = obj.onEnd;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onEnd on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onMove(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			FairyGUI.EventListener ret = obj.onMove;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onMove on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onAction(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			FairyGUI.EventListener ret = obj.onAction;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onAction on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_velocity(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.velocity = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index velocity on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_position(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.position = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index position on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_delta(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.delta = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index delta on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_actionDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.actionDistance = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index actionDistance on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_snapping(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.SwipeGesture obj = (FairyGUI.SwipeGesture)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.snapping = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index snapping on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ACTION_DISTANCE(IntPtr L)
	{
		try
		{
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			FairyGUI.SwipeGesture.ACTION_DISTANCE = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

