﻿using System;
using BeastConsole.Backend;
using BeastConsole.GUI;
using UnityEngine;

namespace BeastConsole
{
#pragma warning disable 0649
    public class Console : MonoBehaviour
    {
        public static event Action<bool> OnStateChanged = delegate { };

        private static Console _instance;

        public static Console instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<Console>();
                return _instance;
            }
        }

        [SerializeField] internal ConsoleGui.Options consoleOptions;

        private GameObject m_consoleRoot;
        private ConsoleGui m_gui;
        private ConsoleBackend m_backend;

        private void Awake()
        {
            _instance = this;
            consoleOptions = new ConsoleGui.Options();
            consoleOptions.ConsoleKey = KeyCode.BackQuote;
            consoleOptions.TweenTime = 0.4f;
            consoleOptions.MaxConsoleLines = 120;
            consoleOptions.LinePadding = 10;
            consoleOptions.LogHandler = true;
            consoleOptions.colors = new ConsoleGui.Options.Colors();
            consoleOptions.colors.error = Color.red;
            consoleOptions.colors.warning = Color.yellow;
            consoleOptions.colors.log = Color.white;
            consoleOptions.colors.command = Color.blue;
            consoleOptions.colors.suggestionGreyed = Color.gray;

            consoleOptions.skin = Resources.Load<GUISkin>("BeastConsole/ConsoleSkin");
            m_backend = new ConsoleBackend(consoleOptions.LogHandler, consoleOptions);
            m_gui = new ConsoleGui(m_backend, consoleOptions);
            ConsoleGui.OnStateChanged += x => OnStateChanged(x);
        }

        public static void AddCommand(string name, string description, object owner, Action<string[]> callback)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.RegisterCommand(name, description, owner, callback);
        }

        public static void RemoveCommand(string name, object owner)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.RemoveCommandIfExists(name, owner);
        }

        public static void AddVariable<T>(string name, string description, Action<T> setter, object owner)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.RegisterVariable<T>(setter, owner, name, description);
        }

        public static void RemoveVariable<T>(string name, object owner)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.UnregisterVariable<T>(name, owner);
        }

        /// <summary>
        /// Directly execute command
        /// </summary>
        /// <param name="line"></param>
        public static void ExecuteLine(string line)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.ExecuteLine(line);
        }


        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(string message)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.WriteLine(message);
        }

        private void Update()
        {
            m_gui.Update();
        }

        private void OnGUI()
        {
            m_gui.OnGUI();
        }
    }
}