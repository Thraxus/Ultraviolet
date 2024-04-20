using System;
using CleanFreak.Common.DataTypes;
using CleanFreak.Common.Utilities.FileHandlers;
using CleanFreak.Common.Utilities.Tools.Logging;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace CleanFreak.Settings.Custom
{
	public static class ImportCustomUserSettings
	{
		private static readonly MyIni MyIni = new MyIni();
		private static string _customUserIni;
		private static bool _customConfigSet;

		public static void Run()
		{
			if (_customConfigSet) return;
			_customConfigSet = true;

			_customUserIni = Load.ReadFileFromWorldStorage(ConfigConstants.ConfigFileName, typeof(UserSettings));
			if (string.IsNullOrEmpty(_customUserIni))
			{
				StaticLog.WriteToLog("GetCustomUserIni", "No custom settings found. Exporting vanilla settings.", LogType.General);
				ExportDefaultUserSettings.Run();
				return;
			}
			if (!MyIni.TryParse(_customUserIni))
			{
				StaticLog.WriteToLog("GetCustomUserIni", "Parse failed for custom user settings. Exporting vanilla settings.", LogType.General);
				ExportDefaultUserSettings.Run();
				return;
			}
			if (!MyIni.ContainsSection(ConfigConstants.SectionName))
			{
				StaticLog.WriteToLog("GetCustomUserIni", "User config did not contain the proper section. Exporting vanilla settings.", LogType.General);
				ExportDefaultUserSettings.Run();
				return;
			}
			ParseConfig();
		}

		private static void ParseConfig()
		{
			UserSettings.UseAggressiveCleanup = MyIni.Get(ConfigConstants.SectionName, UserSettings.UseAggressiveCleanupSettingName).ToBoolean(UserSettings.UseAggressiveCleanup);
			UserSettings.UseSuperAggressiveCleanup = MyIni.Get(ConfigConstants.SectionName, UserSettings.UseSuperAggressiveCleanupSettingName).ToBoolean(UserSettings.UseSuperAggressiveCleanup);

			UserSettings.EncounterStandardCleanupRange = MyIni.Get(ConfigConstants.SectionName, UserSettings.EncounterStandardCleanupRangeSettingName).ToInt32(UserSettings.EncounterStandardCleanupRange);
			UserSettings.EncounterAggressiveCleanupRange = MyIni.Get(ConfigConstants.SectionName, UserSettings.EncounterAggressiveCleanupRangeSettingName).ToInt32(UserSettings.EncounterAggressiveCleanupRange);
			UserSettings.EncounterSuperAggressiveCleanupRange = MyIni.Get(ConfigConstants.SectionName, UserSettings.EncounterSuperAggressiveCleanupRangeSettingName).ToInt32(UserSettings.EncounterSuperAggressiveCleanupRange);
			UserSettings.CargoStandardCleanupRange = MyIni.Get(ConfigConstants.SectionName, UserSettings.CargoStandardCleanupRangeSettingName).ToInt32(UserSettings.CargoStandardCleanupRange);
			UserSettings.CargoAggressiveCleanupRange = MyIni.Get(ConfigConstants.SectionName, UserSettings.CargoAggressiveCleanupRangeSettingName).ToInt32(UserSettings.CargoAggressiveCleanupRange);
			UserSettings.CargoSuperAggressiveCleanupRange = MyIni.Get(ConfigConstants.SectionName, UserSettings.CargoSuperAggressiveCleanupRangeSettingName).ToInt32(UserSettings.CargoSuperAggressiveCleanupRange);
			UserSettings.DebrisCleanupRange = MyIni.Get(ConfigConstants.SectionName, UserSettings.DebrisCleanupRangeSettingName).ToInt32(UserSettings.DebrisCleanupRange);
			UserSettings.NpcCleanupInterval = MyIni.Get(ConfigConstants.SectionName, UserSettings.NpcCleanupIntervalSettingName).ToInt32(UserSettings.NpcCleanupInterval);
			UserSettings.PassesBeforeDebrisCleanup = MyIni.Get(ConfigConstants.SectionName, UserSettings.PassesBeforeDebrisCleanupSettingName).ToInt32(UserSettings.PassesBeforeDebrisCleanup);
			UserSettings.PassesBeforeStandardCleanup = MyIni.Get(ConfigConstants.SectionName, UserSettings.PassesBeforeStandardCleanupSettingName).ToInt32(UserSettings.PassesBeforeStandardCleanup);
			UserSettings.PassesBeforeAggressiveCleanup = MyIni.Get(ConfigConstants.SectionName, UserSettings.PassesBeforeAggressiveCleanupSettingName).ToInt32(UserSettings.PassesBeforeAggressiveCleanup);
			UserSettings.PassesBeforeSuperAggressiveCleanup = MyIni.Get(ConfigConstants.SectionName, UserSettings.PassesBeforeSuperAggressiveCleanupSettingName).ToInt32(UserSettings.PassesBeforeSuperAggressiveCleanup);
			UserSettings.DebrisBlockCountThreshold = MyIni.Get(ConfigConstants.SectionName, UserSettings.DebrisBlockCountThresholdSettingName).ToInt32(UserSettings.DebrisBlockCountThreshold);
			
			UserSettings.IgnoreCleanupWhenNoPlayersOnline = MyIni.Get(ConfigConstants.SectionName, UserSettings.IgnoreCleanupWhenNoPlayersOnlineSettingName).ToBoolean(UserSettings.IgnoreCleanupWhenNoPlayersOnline);
			UserSettings.IgnoreGridVelocityCheck = MyIni.Get(ConfigConstants.SectionName, UserSettings.IgnoreGridVelocityCheckSettingName).ToBoolean(UserSettings.IgnoreGridVelocityCheck);

            UserSettings.EncounterStandardCleanupRange = Math.Abs(UserSettings.EncounterStandardCleanupRange);
			UserSettings.EncounterAggressiveCleanupRange = Math.Abs(UserSettings.EncounterAggressiveCleanupRange);
			UserSettings.EncounterSuperAggressiveCleanupRange = Math.Abs(UserSettings.EncounterSuperAggressiveCleanupRange);
			UserSettings.CargoStandardCleanupRange = Math.Abs(UserSettings.CargoStandardCleanupRange);
			UserSettings.CargoAggressiveCleanupRange = Math.Abs(UserSettings.CargoAggressiveCleanupRange);
			UserSettings.CargoSuperAggressiveCleanupRange = Math.Abs(UserSettings.CargoSuperAggressiveCleanupRange);
			UserSettings.DebrisCleanupRange = Math.Abs(UserSettings.DebrisCleanupRange);
			UserSettings.NpcCleanupInterval = Math.Abs(UserSettings.NpcCleanupInterval);
			UserSettings.PassesBeforeDebrisCleanup = Math.Abs(UserSettings.PassesBeforeDebrisCleanup);
			UserSettings.PassesBeforeStandardCleanup = Math.Abs(UserSettings.PassesBeforeStandardCleanup);
			UserSettings.PassesBeforeAggressiveCleanup = Math.Abs(UserSettings.PassesBeforeAggressiveCleanup);
			UserSettings.PassesBeforeSuperAggressiveCleanup = Math.Abs(UserSettings.PassesBeforeSuperAggressiveCleanup);
			UserSettings.DebrisBlockCountThreshold = Math.Abs(UserSettings.DebrisBlockCountThreshold);
		}
	}
}
