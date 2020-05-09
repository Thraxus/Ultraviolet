namespace Ultraviolet.CleanFreak_Thraxus.DataTypes
{
	public struct SpawnGroupPrefab
	{
		public readonly string SpawnGroupName;
		public readonly string PrefabName;
		public readonly PrefabType PrefabType;
		public readonly bool IgnoreCleanup;
		public readonly string Mod;
		public readonly bool IsNull;

		public SpawnGroupPrefab(string spawnGroupName, string prefabName, PrefabType prefabType, bool ignoreCleanup, bool isNull, string mod)
		{
			SpawnGroupName = spawnGroupName;
			PrefabName = prefabName;
			PrefabType = prefabType;
			IgnoreCleanup = ignoreCleanup;
			IsNull = isNull;
			Mod = mod;
		}

		public override string ToString()
		{
			return $"Group: {SpawnGroupName} | Prefab: {PrefabName} | Type: {PrefabType} | Ignore Cleanup: {IgnoreCleanup} | Mod: {Mod}";
		}
	}
}