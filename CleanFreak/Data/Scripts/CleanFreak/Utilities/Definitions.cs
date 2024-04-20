using System;
using System.Collections.Generic;
using CleanFreak.Common.DataTypes;
using CleanFreak.Common.Utilities.Tools.Logging;
using CleanFreak.DataTypes;
using Sandbox.Definitions;

namespace CleanFreak.Utilities
{
	public static class Definitions
	{
		public static readonly Dictionary<string, SpawnGroupPrefab> CargoShipPrefabs = new Dictionary<string, SpawnGroupPrefab>();

		public static readonly Dictionary<string, SpawnGroupPrefab> EncounterPrefabs = new Dictionary<string, SpawnGroupPrefab>();

		private static readonly SpawnGroupPrefab NullPrefab = new SpawnGroupPrefab("null", "null", PrefabType.None, false, true, "null");

		private const string MesCleanupIgnore = "";

		public static bool Ready;

		public static void Initialize()
		{
			ClassifySpawnGroups();
		}

		public static SpawnGroupPrefab GetPrefabInfo(string name)
		{
			if (!Ready | string.IsNullOrEmpty(name)) return NullPrefab;
			SpawnGroupPrefab prefab;
			if (EncounterPrefabs.TryGetValue(name, out prefab))
				return prefab;
			return CargoShipPrefabs.TryGetValue(name, out prefab) ? prefab : NullPrefab;
		}

		private static void ClassifySpawnGroups()
		{
			foreach (MySpawnGroupDefinition spawnGroupDefinition in MyDefinitionManager.Static.GetSpawnGroupDefinitions())
			{
				try
				{
					if (spawnGroupDefinition == null) continue;
					if (!spawnGroupDefinition.Public) continue;
					if (spawnGroupDefinition.Prefabs == null || spawnGroupDefinition.Prefabs.Count == 0) continue;
					PrefabType type = spawnGroupDefinition.IsEncounter ? PrefabType.Encounter :
						spawnGroupDefinition.IsCargoShip ? PrefabType.CargoShip :
						(!spawnGroupDefinition.IsEncounter && !spawnGroupDefinition.IsPirate) ? PrefabType.CargoShip :
						PrefabType.None;
					if (type == PrefabType.None) continue;
					string spawnGroupName = spawnGroupDefinition.Id.SubtypeName;
					if (string.IsNullOrEmpty(spawnGroupName)) spawnGroupName = "wasNullOrEmpty";
					string mod = string.IsNullOrEmpty(spawnGroupDefinition.Context.ModName) ? "Vanilla" : spawnGroupDefinition.Context.ModName;

					foreach (MySpawnGroupDefinition.SpawnGroupPrefab prefab in spawnGroupDefinition.Prefabs.AsReadOnly())
					{
						MyPrefabDefinition prefabDef = MyDefinitionManager.Static.GetPrefabDefinition(prefab.SubtypeId);
						if (prefabDef == null) continue;
						if (prefabDef.CubeGrids.Length == 0) continue;
						if (!prefabDef.Public) continue;
						string name = prefabDef.CubeGrids[0].DisplayName;
						SpawnGroupPrefab sgp = new SpawnGroupPrefab(spawnGroupName, name, type, false, false, mod);
						switch (type)
						{
							case PrefabType.Encounter:
								if (EncounterPrefabs.ContainsKey(name)) continue;
								EncounterPrefabs.Add(name, sgp);
								continue;
							case PrefabType.CargoShip:
								if (CargoShipPrefabs.ContainsKey(name)) continue;
								CargoShipPrefabs.Add(name, sgp);
								continue;
							case PrefabType.None:
								continue;
							case PrefabType.SubGrid:
								continue;
							default:
								continue;
						}
					}
				}
				catch (Exception e)
				{
					StaticLog.WriteToLog("ClassifySpawnGroups", e.ToString(), LogType.Exception);
				}
			}

			foreach (KeyValuePair<string, SpawnGroupPrefab> prefab in EncounterPrefabs)
			{
				StaticLog.WriteToLog("ClassifySpawnGroups-Encounter", $"{prefab.Value}", LogType.General);
			}

			foreach (KeyValuePair<string, SpawnGroupPrefab> prefab in CargoShipPrefabs)
			{
				StaticLog.WriteToLog("ClassifySpawnGroups-CargoShip", $"{prefab.Value}", LogType.General);
			}

			Ready = true;
		}
	}
}