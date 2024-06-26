﻿using System;
using System.IO;
using CleanFreak.Common.DataTypes;
using Sandbox.ModAPI;
using VRage.Game;

namespace CleanFreak.Common.Utilities.Tools.Logging
{
	public class Log
	{
		private string LogName { get; set; }

		private TextWriter TextWriter { get; set; }

		private static string TimeStamp => DateTime.Now.ToString("MMddyy-HH:mm:ss:ffff");

		private readonly FastQueue<string> _messageQueue = new FastQueue<string>(20);

		private const int DefaultIndent = 4;

		private static string Indent { get; } = new string(' ', DefaultIndent);

		public Log(string logName)
		{
			LogName = logName + ".log";
			Init();
		}

		private void Init()
		{
			if (TextWriter != null) return;
			TextWriter = MyAPIGateway.Utilities.WriteFileInLocalStorage(LogName, typeof(Log));
		}

		public void Close()
		{
			TextWriter?.Flush();
			TextWriter?.Close();
			TextWriter = null;
		}

		public void WriteToLog(string caller, string message, bool showOnHud = false, int duration = Settings.GeneralSettings.DefaultLocalMessageDisplayTime, string color = MyFontEnum.Green)
		{
			BuildLogLine(caller, message);
			if (!showOnHud) return;
		}

		public void GetTailMessages()
		{
			lock (_lockObject)
			{
				MyAPIGateway.Utilities.ShowMissionScreen(LogName, "", "", string.Join($"{Environment.NewLine}{Environment.NewLine}", _messageQueue.GetQueue()));
			}
		}
		
		private readonly object _lockObject = new object();

		private void BuildLogLine(string caller, string message)
		{
			lock (_lockObject)
			{
				WriteLine($"{TimeStamp}{Indent}{caller}{Indent}{message}");
			}
		}

		private void WriteLine(string line)
		{
			_messageQueue?.Enqueue(line);
			TextWriter?.WriteLine(line);
			TextWriter?.Flush();
		}
	}
}