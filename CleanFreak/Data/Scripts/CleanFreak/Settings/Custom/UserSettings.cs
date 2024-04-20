namespace CleanFreak.Settings.Custom
{
	public static class UserSettings
	{
		/// <summary>
		/// Set this to false to stop using Aggressive Cleanup rules (shorter distance, more hits required)
		/// </summary>
		public static bool UseAggressiveCleanup = true;
		public const string UseAggressiveCleanupSettingName = "UseAggressiveCleanup";
		public const string UseAggressiveCleanupDescription = "Valid Setting: [True/False] Default: True - Set this to false to stop using Aggressive Cleanup rules (shorter distance, more hits required)";

		/// <summary>
		/// Set this to false to never cleanup grids partially owned by a player (is on the SmallOwners List, not BigOwner)
		/// </summary>
		public static bool UseSuperAggressiveCleanup = true;
		public const string UseSuperAggressiveCleanupSettingName = "UseSuperAggressiveCleanup";
		public const string UseSuperAggressiveCleanupDescription = "Valid Setting: [True/False] Default: True - Set this to false to never cleanup grids partially owned by a player (is on the SmallOwners List, not BigOwner)";

		/// <summary>
		/// Range in meters that the cleanup needs for standard rules
		/// </summary>
		public static int EncounterStandardCleanupRange = 30000;
		public const string EncounterStandardCleanupRangeSettingName = "EncounterStandardCleanupRange";
		public const string EncounterStandardCleanupRangeDescription = "Valid Setting: [Whole Number] Default: 30000 - Range in meters that the cleanup needs for standard rules";
		
		/// <summary>
		/// Range in meters that the cleanup needs for aggressive rules
		/// </summary>
		public static int EncounterAggressiveCleanupRange = 20000;
		public const string EncounterAggressiveCleanupRangeSettingName = "EncounterAggressiveCleanupRange";
		public const string EncounterAggressiveCleanupRangeDescription = "Valid Setting: [Whole Number] Default: 20000 - Range in meters that the cleanup needs for aggressive rules";

		/// <summary>
		/// Range in meters that the cleanup needs for super aggressive rules
		/// </summary>
		public static int EncounterSuperAggressiveCleanupRange = 10000;
		public const string EncounterSuperAggressiveCleanupRangeSettingName = "EncounterSuperAggressiveCleanupRange";
		public const string EncounterSuperAggressiveCleanupRangeDescription = "Valid Setting: [Whole Number] Default: 10000 - Range in meters that the cleanup needs for super aggressive rules";

		/// <summary>
		/// Range in meters that the cleanup needs for standard rules
		/// </summary>
		public static int CargoStandardCleanupRange = 10000;
		public const string CargoStandardCleanupRangeSettingName = "CargoStandardCleanupRange";
		public const string CargoStandardCleanupRangeDescription = "Valid Setting: [Whole Number] Default: 10000 - Range in meters that the cleanup needs for standard rules";

		/// <summary>
		/// Range in meters that the cleanup needs for aggressive rules
		/// </summary>
		public static int CargoAggressiveCleanupRange = 5000;
		public const string CargoAggressiveCleanupRangeSettingName = "CargoAggressiveCleanupRange";
		public const string CargoAggressiveCleanupRangeDescription = "Valid Setting: [Whole Number] Default: 5000 - Range in meters that the cleanup needs for aggressive rules";

		/// <summary>
		/// Range in meters that the cleanup needs for super aggressive rules
		/// </summary>
		public static int CargoSuperAggressiveCleanupRange = 10000;
		public const string CargoSuperAggressiveCleanupRangeSettingName = "CargoSuperAggressiveCleanupRange";
		public const string CargoSuperAggressiveCleanupRangeDescription = "Valid Setting: [Whole Number] Default: 10000 - Range in meters that the cleanup needs for super aggressive rules";

		/// <summary>
		/// Range in meters that debris will be cleaned up
		/// </summary>
		public static int DebrisCleanupRange = 1000;
		public const string DebrisCleanupRangeSettingName = "DebrisCleanupRange";
		public const string DebrisCleanupRangeDescription = "Valid Setting: [Whole Number] Default: 1000 - Range in meters that debris will be cleaned up";

		/// <summary>
		/// Minutes before the cleanup routine will kick in 
		/// </summary>
		public static int NpcCleanupInterval = 6;
		public const string NpcCleanupIntervalSettingName = "NpcCleanupInterval";
		public const string NpcCleanupIntervalDescription = "Valid Setting: [Whole Number] Default: 6 - Minutes before the cleanup routine will kick in";

		/// <summary>
		/// How many identical passes must a grid have before cleanup cleans it up
		/// </summary>
		public static int PassesBeforeDebrisCleanup = 3;
		public const string PassesBeforeDebrisCleanupSettingName = "PassesBeforeDebrisCleanup";
		public const string PassesBeforeDebrisCleanupDescription = "Valid Setting: [Whole Number] Default: 3 - How many identical passes must a grid have before cleanup cleans it up";

		/// <summary>
		/// How many identical passes must a grid have before cleanup cleans it up
		/// </summary>
		public static int PassesBeforeStandardCleanup = 3;
		public const string PassesBeforeStandardCleanupSettingName = "PassesBeforeStandardCleanup";
		public const string PassesBeforeStandardCleanupDescription = "Valid Setting: [Whole Number] Default: 3 - How many identical passes must a grid have before cleanup cleans it up";

		/// <summary>
		/// How many identical passes must a grid have before cleanup cleans it up 
		/// </summary>
		public static int PassesBeforeAggressiveCleanup = 6;
		public const string PassesBeforeAggressiveCleanupSettingName = "PassesBeforeAggressiveCleanup";
		public const string PassesBeforeAggressiveCleanupDescription = "Valid Setting: [Whole Number] Default: 6 - How many identical passes must a grid have before cleanup cleans it up";

		/// <summary>
		/// This is only run when a player owns some small portion of a NPC grid (is on the SmallOwners List)
		/// </summary>
		public static int PassesBeforeSuperAggressiveCleanup = 12;
		public const string PassesBeforeSuperAggressiveCleanupSettingName = "PassesBeforeSuperAggressiveCleanup";
		public const string PassesBeforeSuperAggressiveCleanupDescription = "Valid Setting: [Whole Number] Default: 12 - This is only run when a player owns some small portion of a NPC grid (is on the SmallOwners List)";

		/// <summary>
		/// How many blocks does a grid need to consist of before it's not considered debris
		/// </summary>
		public static int DebrisBlockCountThreshold = 6;
		public const string DebrisBlockCountThresholdSettingName = "DebrisBlockCountThreshold";
		public const string DebrisBlockCountThresholdDescription = "Valid Setting: [Whole Number] Default: 6 - How many blocks does a grid need to consist of before it's not considered debris";

		/// <summary>
		/// Stops this mod from running when no players are online
		/// </summary>
		public static bool IgnoreCleanupWhenNoPlayersOnline = false;
		public const string IgnoreCleanupWhenNoPlayersOnlineSettingName = "IgnoreCleanupWhenNoPlayersOnline";
		public const string IgnoreCleanupWhenNoPlayersOnlineDescription = "Valid Setting: [True/False] Default: False - Stops this mod from running when no players are online";

        /// <summary>
        /// Stops this mod from running when no players are online
        /// </summary>
        public static bool IgnoreGridVelocityCheck = false;
        public const string IgnoreGridVelocityCheckSettingName = "IgnoreGridVelocityCheck";
        public const string IgnoreGridVelocityCheckDescription = "Valid Setting: [True/False] Default: False - Stops a grids velocity from being considered when running the deletion checks";
    }
}
