namespace Ultraviolet.Thraxus.DataTypes
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
			Mod = string.IsNullOrEmpty(mod) ? "Vanilla" : mod;
		}

		public override string ToString()
		{
			return $"Origin: {Mod}\t{SpawnGroupName}\t{PrefabName}\t{PrefabType}\tIgnore Cleanup: {IgnoreCleanup}";
		}
	}
}
