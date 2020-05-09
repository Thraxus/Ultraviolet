using Ultraviolet.CleanFreak.Common.Settings;
using Ultraviolet.CleanFreak.Common.Utilities.FileHandlers;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace Ultraviolet.CleanFreak.Settings.Custom
{
	public static class ExportDefaultUserSettings
	{
		private static readonly MyIni MyIni = new MyIni();
		private const string SectionName = "User Configuration";

		public static void Run()
		{
			BuildTheIni();
			Export();
		}

		private static void BuildTheIni()
		{
			MyIni.Set(SectionName, UserSettings.UseAggressiveCleanupSettingName, UserSettings.UseAggressiveCleanup);
			MyIni.SetComment(SectionName, UserSettings.UseAggressiveCleanupSettingName, UserSettings.UseAggressiveCleanupDescription);

			MyIni.Set(SectionName, UserSettings.UseSuperAggressiveCleanupSettingName, UserSettings.UseSuperAggressiveCleanup);
			MyIni.SetComment(SectionName, UserSettings.UseSuperAggressiveCleanupSettingName, UserSettings.UseSuperAggressiveCleanupDescription);

			MyIni.Set(SectionName, UserSettings.EncounterStandardCleanupRangeSettingName, UserSettings.EncounterStandardCleanupRange);
			MyIni.SetComment(SectionName, UserSettings.EncounterStandardCleanupRangeSettingName, UserSettings.EncounterStandardCleanupRangeDescription);

			MyIni.Set(SectionName, UserSettings.EncounterAggressiveCleanupRangeSettingName, UserSettings.EncounterAggressiveCleanupRange);
			MyIni.SetComment(SectionName, UserSettings.EncounterAggressiveCleanupRangeSettingName, UserSettings.EncounterAggressiveCleanupRangeDescription);

			MyIni.Set(SectionName, UserSettings.EncounterSuperAggressiveCleanupRangeSettingName, UserSettings.EncounterSuperAggressiveCleanupRange);
			MyIni.SetComment(SectionName, UserSettings.EncounterSuperAggressiveCleanupRangeSettingName, UserSettings.EncounterSuperAggressiveCleanupRangeDescription);

			MyIni.Set(SectionName, UserSettings.CargoStandardCleanupRangeSettingName, UserSettings.CargoStandardCleanupRange);
			MyIni.SetComment(SectionName, UserSettings.CargoStandardCleanupRangeSettingName, UserSettings.CargoStandardCleanupRangeDescription);

			MyIni.Set(SectionName, UserSettings.CargoAggressiveCleanupRangeSettingName, UserSettings.CargoAggressiveCleanupRange);
			MyIni.SetComment(SectionName, UserSettings.CargoAggressiveCleanupRangeSettingName, UserSettings.CargoAggressiveCleanupRangeDescription);

			MyIni.Set(SectionName, UserSettings.CargoSuperAggressiveCleanupRangeSettingName, UserSettings.CargoSuperAggressiveCleanupRange);
			MyIni.SetComment(SectionName, UserSettings.CargoSuperAggressiveCleanupRangeSettingName, UserSettings.CargoSuperAggressiveCleanupRangeDescription);

			MyIni.Set(SectionName, UserSettings.DebrisCleanupRangeSettingName, UserSettings.DebrisCleanupRange);
			MyIni.SetComment(SectionName, UserSettings.DebrisCleanupRangeSettingName, UserSettings.DebrisCleanupRangeDescription);

			MyIni.Set(SectionName, UserSettings.NpcCleanupIntervalSettingName, UserSettings.NpcCleanupInterval);
			MyIni.SetComment(SectionName, UserSettings.NpcCleanupIntervalSettingName, UserSettings.NpcCleanupIntervalDescription);

			MyIni.Set(SectionName, UserSettings.PassesBeforeDebrisCleanupSettingName, UserSettings.PassesBeforeDebrisCleanup);
			MyIni.SetComment(SectionName, UserSettings.PassesBeforeDebrisCleanupSettingName, UserSettings.PassesBeforeDebrisCleanupDescription);

			MyIni.Set(SectionName, UserSettings.PassesBeforeStandardCleanupSettingName, UserSettings.PassesBeforeStandardCleanup);
			MyIni.SetComment(SectionName, UserSettings.PassesBeforeStandardCleanupSettingName, UserSettings.PassesBeforeStandardCleanupDescription);

			MyIni.Set(SectionName, UserSettings.PassesBeforeAggressiveCleanupSettingName, UserSettings.PassesBeforeAggressiveCleanup);
			MyIni.SetComment(SectionName, UserSettings.PassesBeforeAggressiveCleanupSettingName, UserSettings.PassesBeforeAggressiveCleanupDescription);

			MyIni.Set(SectionName, UserSettings.PassesBeforeSuperAggressiveCleanupSettingName, UserSettings.PassesBeforeSuperAggressiveCleanup);
			MyIni.SetComment(SectionName, UserSettings.PassesBeforeSuperAggressiveCleanupSettingName, UserSettings.PassesBeforeSuperAggressiveCleanupDescription);

			MyIni.Set(SectionName, UserSettings.DebrisBlockCountThresholdSettingName, UserSettings.DebrisBlockCountThreshold);
			MyIni.SetComment(SectionName, UserSettings.DebrisBlockCountThresholdSettingName, UserSettings.DebrisBlockCountThresholdDescription);

			MyIni.Set(SectionName, UserSettings.IgnoreCleanupWhenNoPlayersOnlineSettingName, UserSettings.IgnoreCleanupWhenNoPlayersOnline);
			MyIni.SetComment(SectionName, UserSettings.IgnoreCleanupWhenNoPlayersOnlineSettingName, UserSettings.IgnoreCleanupWhenNoPlayersOnlineDescription);
		}

		private static void Export()
		{
			Save.WriteToFile(GeneralSettings.ConfigFileName, MyIni, typeof(UserSettings));
		}
	}
}