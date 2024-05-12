using CleanFreak.Common.Utilities.FileHandlers;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace CleanFreak.Settings.Custom
{
	public static class ExportDefaultUserSettings
	{
		private static readonly MyIni MyIni = new MyIni();
		private static bool _configExported;

		public static void Run()
		{
			if (_configExported) return;
			_configExported = true;

			BuildTheIni();
			Export();
		}

		private static void BuildTheIni()
		{
			MyIni.Set(ConfigConstants.SectionName, UserSettings.UseAggressiveCleanupSettingName, UserSettings.UseAggressiveCleanup);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.UseAggressiveCleanupSettingName, UserSettings.UseAggressiveCleanupDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.UseSuperAggressiveCleanupSettingName, UserSettings.UseSuperAggressiveCleanup);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.UseSuperAggressiveCleanupSettingName, UserSettings.UseSuperAggressiveCleanupDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.EncounterStandardCleanupRangeSettingName, UserSettings.EncounterStandardCleanupRange);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.EncounterStandardCleanupRangeSettingName, UserSettings.EncounterStandardCleanupRangeDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.EncounterAggressiveCleanupRangeSettingName, UserSettings.EncounterAggressiveCleanupRange);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.EncounterAggressiveCleanupRangeSettingName, UserSettings.EncounterAggressiveCleanupRangeDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.EncounterSuperAggressiveCleanupRangeSettingName, UserSettings.EncounterSuperAggressiveCleanupRange);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.EncounterSuperAggressiveCleanupRangeSettingName, UserSettings.EncounterSuperAggressiveCleanupRangeDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.CargoStandardCleanupRangeSettingName, UserSettings.CargoStandardCleanupRange);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.CargoStandardCleanupRangeSettingName, UserSettings.CargoStandardCleanupRangeDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.CargoAggressiveCleanupRangeSettingName, UserSettings.CargoAggressiveCleanupRange);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.CargoAggressiveCleanupRangeSettingName, UserSettings.CargoAggressiveCleanupRangeDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.CargoSuperAggressiveCleanupRangeSettingName, UserSettings.CargoSuperAggressiveCleanupRange);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.CargoSuperAggressiveCleanupRangeSettingName, UserSettings.CargoSuperAggressiveCleanupRangeDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.DebrisCleanupRangeSettingName, UserSettings.DebrisCleanupRange);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.DebrisCleanupRangeSettingName, UserSettings.DebrisCleanupRangeDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.NpcCleanupIntervalSettingName, UserSettings.NpcCleanupInterval);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.NpcCleanupIntervalSettingName, UserSettings.NpcCleanupIntervalDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.PassesBeforeDebrisCleanupSettingName, UserSettings.PassesBeforeDebrisCleanup);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.PassesBeforeDebrisCleanupSettingName, UserSettings.PassesBeforeDebrisCleanupDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.PassesBeforeStandardCleanupSettingName, UserSettings.PassesBeforeStandardCleanup);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.PassesBeforeStandardCleanupSettingName, UserSettings.PassesBeforeStandardCleanupDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.PassesBeforeAggressiveCleanupSettingName, UserSettings.PassesBeforeAggressiveCleanup);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.PassesBeforeAggressiveCleanupSettingName, UserSettings.PassesBeforeAggressiveCleanupDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.PassesBeforeSuperAggressiveCleanupSettingName, UserSettings.PassesBeforeSuperAggressiveCleanup);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.PassesBeforeSuperAggressiveCleanupSettingName, UserSettings.PassesBeforeSuperAggressiveCleanupDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.DebrisBlockCountThresholdSettingName, UserSettings.DebrisBlockCountThreshold);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.DebrisBlockCountThresholdSettingName, UserSettings.DebrisBlockCountThresholdDescription);

			MyIni.Set(ConfigConstants.SectionName, UserSettings.IgnoreCleanupWhenNoPlayersOnlineSettingName, UserSettings.IgnoreCleanupWhenNoPlayersOnline);
			MyIni.SetComment(ConfigConstants.SectionName, UserSettings.IgnoreCleanupWhenNoPlayersOnlineSettingName, UserSettings.IgnoreCleanupWhenNoPlayersOnlineDescription);

            MyIni.Set(ConfigConstants.SectionName, UserSettings.IgnoreGridVelocityCheckSettingName, UserSettings.IgnoreGridVelocityCheck);
            MyIni.SetComment(ConfigConstants.SectionName, UserSettings.IgnoreGridVelocityCheckSettingName, UserSettings.IgnoreGridVelocityCheckDescription);

            MyIni.Set(ConfigConstants.SectionName, UserSettings.VerboseDebugLoggingSettingName, UserSettings.VerboseDebugLogging);
            MyIni.SetComment(ConfigConstants.SectionName, UserSettings.VerboseDebugLoggingSettingName, UserSettings.VerboseDebugLoggingDescription);
        }

		private static void Export()
		{
			Save.WriteToFile(ConfigConstants.ConfigFileName, MyIni, typeof(UserSettings));
		}
	}
}