﻿extern alias il2cpp;
using System;
using BepInEx.Logging;
using il2cpp::UnityEngine;

namespace BepInEx.IL2CPP.Logging
{
	public class IL2CPPUnityLogSource : ILogSource
	{
		public string SourceName { get; } = "Unity";

		public event EventHandler<LogEventArgs> LogEvent;

		public void UnityLogCallback(string logLine, string exception, LogType type)
		{
			var level = type switch
			{
				LogType.Error => LogLevel.Error,
				LogType.Assert => LogLevel.Debug,
				LogType.Warning => LogLevel.Warning,
				LogType.Log => LogLevel.Message,
				LogType.Exception => LogLevel.Error,
				_ => LogLevel.Message
			};
			LogEvent?.Invoke(this, new LogEventArgs(logLine, level, this));
		}

		public IL2CPPUnityLogSource()
		{
			Application.s_LogCallbackHandler = new Action<string, string, LogType>(UnityLogCallback);
		}

		public void Dispose() { }
	}
}