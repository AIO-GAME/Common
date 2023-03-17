﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class FairyGUI_BlendModeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(FairyGUI.BlendMode));
		L.RegVar("Normal", get_Normal, null);
		L.RegVar("None", get_None, null);
		L.RegVar("Add", get_Add, null);
		L.RegVar("Multiply", get_Multiply, null);
		L.RegVar("Screen", get_Screen, null);
		L.RegVar("Erase", get_Erase, null);
		L.RegVar("Mask", get_Mask, null);
		L.RegVar("Below", get_Below, null);
		L.RegVar("Off", get_Off, null);
		L.RegVar("One_OneMinusSrcAlpha", get_One_OneMinusSrcAlpha, null);
		L.RegVar("Custom1", get_Custom1, null);
		L.RegVar("Custom2", get_Custom2, null);
		L.RegVar("Custom3", get_Custom3, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
		TypeTraits<FairyGUI.BlendMode>.Check = CheckType;
		StackTraits<FairyGUI.BlendMode>.Push = Push;
	}

	static void Push(IntPtr L, FairyGUI.BlendMode arg)
	{
		ToLua.Push(L, arg);
	}

	static bool CheckType(IntPtr L, int pos)
	{
		return TypeChecker.CheckEnumType(typeof(FairyGUI.BlendMode), L, pos);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Normal(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Normal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_None(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.None);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Add(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Add);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Multiply(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Multiply);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Screen(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Screen);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Erase(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Erase);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mask(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Mask);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Below(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Below);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Off(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Off);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_One_OneMinusSrcAlpha(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.One_OneMinusSrcAlpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Custom1(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Custom1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Custom2(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Custom2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Custom3(IntPtr L)
	{
		ToLua.Push(L, FairyGUI.BlendMode.Custom3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		FairyGUI.BlendMode o = (FairyGUI.BlendMode)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

