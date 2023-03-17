﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class FairyGUI_NTextureWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(FairyGUI.NTexture), typeof(System.Object));
		L.RegFunction("DisposeEmpty", DisposeEmpty);
		L.RegFunction("GetDrawRect", GetDrawRect);
		L.RegFunction("GetUV", GetUV);
		L.RegFunction("GetMaterialManager", GetMaterialManager);
		L.RegFunction("Unload", Unload);
		L.RegFunction("Reload", Reload);
		L.RegFunction("AddRef", AddRef);
		L.RegFunction("ReleaseRef", ReleaseRef);
		L.RegFunction("Dispose", Dispose);
		L.RegFunction("New", _CreateFairyGUI_NTexture);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("uvRect", get_uvRect, set_uvRect);
		L.RegVar("rotated", get_rotated, set_rotated);
		L.RegVar("refCount", get_refCount, set_refCount);
		L.RegVar("lastActive", get_lastActive, set_lastActive);
		L.RegVar("destroyMethod", get_destroyMethod, set_destroyMethod);
		L.RegVar("Empty", get_Empty, null);
		L.RegVar("width", get_width, null);
		L.RegVar("height", get_height, null);
		L.RegVar("offset", get_offset, set_offset);
		L.RegVar("originalSize", get_originalSize, set_originalSize);
		L.RegVar("root", get_root, null);
		L.RegVar("disposed", get_disposed, null);
		L.RegVar("nativeTexture", get_nativeTexture, null);
		L.RegVar("alphaTexture", get_alphaTexture, null);
		L.RegVar("CustomDestroyMethod", get_CustomDestroyMethod, set_CustomDestroyMethod);
		L.RegVar("onSizeChanged", get_onSizeChanged, set_onSizeChanged);
		L.RegVar("onRelease", get_onRelease, set_onRelease);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFairyGUI_NTexture(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes<UnityEngine.Texture>(L, 1))
			{
				UnityEngine.Texture arg0 = (UnityEngine.Texture)ToLua.ToObject(L, 1);
				FairyGUI.NTexture obj = new FairyGUI.NTexture(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes<UnityEngine.Sprite>(L, 1))
			{
				UnityEngine.Sprite arg0 = (UnityEngine.Sprite)ToLua.ToObject(L, 1);
				FairyGUI.NTexture obj = new FairyGUI.NTexture(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 2)
			{
				UnityEngine.Texture arg0 = (UnityEngine.Texture)ToLua.CheckObject<UnityEngine.Texture>(L, 1);
				UnityEngine.Rect arg1 = StackTraits<UnityEngine.Rect>.Check(L, 2);
				FairyGUI.NTexture obj = new FairyGUI.NTexture(arg0, arg1);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 3)
			{
				FairyGUI.NTexture arg0 = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
				UnityEngine.Rect arg1 = StackTraits<UnityEngine.Rect>.Check(L, 2);
				bool arg2 = LuaDLL.luaL_checkboolean(L, 3);
				FairyGUI.NTexture obj = new FairyGUI.NTexture(arg0, arg1, arg2);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 4)
			{
				UnityEngine.Texture arg0 = (UnityEngine.Texture)ToLua.CheckObject<UnityEngine.Texture>(L, 1);
				UnityEngine.Texture arg1 = (UnityEngine.Texture)ToLua.CheckObject<UnityEngine.Texture>(L, 2);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 3);
				float arg3 = (float)LuaDLL.luaL_checknumber(L, 4);
				FairyGUI.NTexture obj = new FairyGUI.NTexture(arg0, arg1, arg2, arg3);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 5)
			{
				FairyGUI.NTexture arg0 = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
				UnityEngine.Rect arg1 = StackTraits<UnityEngine.Rect>.Check(L, 2);
				bool arg2 = LuaDLL.luaL_checkboolean(L, 3);
				UnityEngine.Vector2 arg3 = ToLua.ToVector2(L, 4);
				UnityEngine.Vector2 arg4 = ToLua.ToVector2(L, 5);
				FairyGUI.NTexture obj = new FairyGUI.NTexture(arg0, arg1, arg2, arg3, arg4);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: FairyGUI.NTexture.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DisposeEmpty(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			FairyGUI.NTexture.DisposeEmpty();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDrawRect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
			UnityEngine.Rect arg0 = StackTraits<UnityEngine.Rect>.Check(L, 2);
			UnityEngine.Rect o = obj.GetDrawRect(arg0);
			ToLua.PushValue(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUV(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
			UnityEngine.Vector2[] arg0 = ToLua.CheckStructArray<UnityEngine.Vector2>(L, 2);
			obj.GetUV(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaterialManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			FairyGUI.MaterialManager o = obj.GetMaterialManager(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Unload(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
				obj.Unload();
				return 0;
			}
			else if (count == 2)
			{
				FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
				bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
				obj.Unload(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: FairyGUI.NTexture.Unload");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reload(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
			UnityEngine.Texture arg0 = (UnityEngine.Texture)ToLua.CheckObject<UnityEngine.Texture>(L, 2);
			UnityEngine.Texture arg1 = (UnityEngine.Texture)ToLua.CheckObject<UnityEngine.Texture>(L, 3);
			obj.Reload(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddRef(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
			obj.AddRef();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReleaseRef(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
			obj.ReleaseRef();
			return 0;
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
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject<FairyGUI.NTexture>(L, 1);
			obj.Dispose();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_uvRect(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			UnityEngine.Rect ret = obj.uvRect;
			ToLua.PushValue(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index uvRect on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rotated(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			bool ret = obj.rotated;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index rotated on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_refCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			int ret = obj.refCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index refCount on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lastActive(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			float ret = obj.lastActive;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index lastActive on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_destroyMethod(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			FairyGUI.DestroyMethod ret = obj.destroyMethod;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index destroyMethod on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Empty(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, FairyGUI.NTexture.Empty);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_width(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			int ret = obj.width;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index width on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_height(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			int ret = obj.height;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index height on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_offset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			UnityEngine.Vector2 ret = obj.offset;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index offset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_originalSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			UnityEngine.Vector2 ret = obj.originalSize;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index originalSize on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_root(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			FairyGUI.NTexture ret = obj.root;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index root on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_disposed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			bool ret = obj.disposed;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index disposed on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_nativeTexture(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			UnityEngine.Texture ret = obj.nativeTexture;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index nativeTexture on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alphaTexture(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			UnityEngine.Texture ret = obj.alphaTexture;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index alphaTexture on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CustomDestroyMethod(IntPtr L)
	{
		ToLua.Push(L, new EventObject(typeof(System.Action<UnityEngine.Texture>)));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onSizeChanged(IntPtr L)
	{
		ToLua.Push(L, new EventObject(typeof(System.Action<FairyGUI.NTexture>)));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onRelease(IntPtr L)
	{
		ToLua.Push(L, new EventObject(typeof(System.Action<FairyGUI.NTexture>)));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_uvRect(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			UnityEngine.Rect arg0 = StackTraits<UnityEngine.Rect>.Check(L, 2);
			obj.uvRect = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index uvRect on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rotated(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.rotated = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index rotated on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_refCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.refCount = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index refCount on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lastActive(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.lastActive = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index lastActive on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_destroyMethod(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			FairyGUI.DestroyMethod arg0 = (FairyGUI.DestroyMethod)ToLua.CheckObject(L, 2, typeof(FairyGUI.DestroyMethod));
			obj.destroyMethod = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index destroyMethod on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_offset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.offset = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index offset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_originalSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			FairyGUI.NTexture obj = (FairyGUI.NTexture)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.originalSize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index originalSize on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CustomDestroyMethod(IntPtr L)
	{
		try
		{
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'FairyGUI.NTexture.CustomDestroyMethod' can only appear on the left hand side of += or -= when used outside of the type 'FairyGUI.NTexture'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action<UnityEngine.Texture> ev = (System.Action<UnityEngine.Texture>)arg0.func;
				FairyGUI.NTexture.CustomDestroyMethod += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action<UnityEngine.Texture> ev = (System.Action<UnityEngine.Texture>)arg0.func;
				FairyGUI.NTexture.CustomDestroyMethod -= ev;
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onSizeChanged(IntPtr L)
	{
		try
		{
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject(L, 1, typeof(FairyGUI.NTexture));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'FairyGUI.NTexture.onSizeChanged' can only appear on the left hand side of += or -= when used outside of the type 'FairyGUI.NTexture'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action<FairyGUI.NTexture> ev = (System.Action<FairyGUI.NTexture>)arg0.func;
				obj.onSizeChanged += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action<FairyGUI.NTexture> ev = (System.Action<FairyGUI.NTexture>)arg0.func;
				obj.onSizeChanged -= ev;
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onRelease(IntPtr L)
	{
		try
		{
			FairyGUI.NTexture obj = (FairyGUI.NTexture)ToLua.CheckObject(L, 1, typeof(FairyGUI.NTexture));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'FairyGUI.NTexture.onRelease' can only appear on the left hand side of += or -= when used outside of the type 'FairyGUI.NTexture'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action<FairyGUI.NTexture> ev = (System.Action<FairyGUI.NTexture>)arg0.func;
				obj.onRelease += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action<FairyGUI.NTexture> ev = (System.Action<FairyGUI.NTexture>)arg0.func;
				obj.onRelease -= ev;
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

