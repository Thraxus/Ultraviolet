namespace Ultraviolet.Thraxus.Settings
{
	public static class UserSettings
	{
		/// <summary>
		/// Range in meters that the cleanup needs for standard rules
		/// </summary>
		public static int StandardCleanupRange = 10000;

		/// <summary>
		/// Range in meters that the cleanup needs for aggressive rules
		/// </summary>
		public static int AggressiveCleanupRange = 5000;

		/// <summary>
		/// Range in meters that the cleanup needs for super aggressive rules
		/// </summary>
		public static int SuperAggressiveCleanupRange = 10000;

		/// <summary>
		/// Range in meters that debris will be cleaned up
		/// </summary>
		public static int DebrisCleanupRange = 1000;

		/// <summary>
		/// Minutes before the cleanup routine will kick in 
		/// </summary>
		public static ulong NpcCleanupInterval = 6;
		
		/// <summary>
		/// How many identical passes must a grid have before cleanup cleans it up
		/// </summary>
		public static int PassesBeforeStandardCleanup = 3;

		/// <summary>
		/// How many identical passes must a grid have before cleanup cleans it up 
		/// </summary>
		public static int PassesBeforeAggressiveCleanup = 6;

		/// <summary>
		/// Set this to false to never cleanup grids partially owned by a player (is on the SmallOwners List, not BigOwner)
		/// </summary>
		public static bool UseSuperAggressiveCleanup = true;

		/// <summary>
		/// This is only run when a player owns some small portion of a NPC grid (is on the SmallOwners List)
		/// </summary>
		public static int PassesBeforeSuperAggressiveCleanup = 12;
		
		/// <summary>
		/// How many blocks does a grid need to consist of before it's not considered debris
		/// </summary>
		public static int DebrisBlockCountThreshold = 6;
	}
}
