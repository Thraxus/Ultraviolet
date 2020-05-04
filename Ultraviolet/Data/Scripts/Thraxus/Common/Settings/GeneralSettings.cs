using System;
using Sandbox.ModAPI;

namespace Ultraviolet.Thraxus.Common.Settings
{
	public static class GeneralSettings
	{
		public const string ConfigFileName = "MyCustomConfig.xml";
		public const string SaveFileName = "MyCustomSave.file";
		public const string SandboxVariableName = "MyCustomSandboxVariableName";

		#region User Configuration

		public static bool DebugMode { get; } = false;

		public static bool ProfilingEnabled { get; } = false;

		#endregion

		#region Constant Values

		public const bool ForcedDebugMode = false;

		public const string ChatCommandPrefix = "chatCommand";
		public const string StaticDebugLogName = "StaticLog-Debug";
		public const string ExceptionLogName = "Exception";
		public const string StaticGeneralLogName = "StaticLog-General";
		public const string ProfilingLogName = "Profiler";

		public const ushort NetworkId = 16759;
		
		#endregion


		#region Reference Values

		public static bool IsServer => MyAPIGateway.Multiplayer.IsServer;

		public const int DefaultLocalMessageDisplayTime = 5000;
		public const int DefaultServerMessageDisplayTime = 10000;
		public const int TicksPerMinute = TicksPerSecond * 60;
		public const int TicksPerSecond = 60;

		public static Random Random { get; } = new Random();

		#endregion
	}
}
