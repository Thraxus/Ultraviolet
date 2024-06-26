﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using CleanFreak.Common.BaseClasses;
using CleanFreak.Common.DataTypes;
using CleanFreak.Common.Settings;
using CleanFreak.DataTypes;
using CleanFreak.Models;
using CleanFreak.Settings.Custom;
using CleanFreak.Utilities;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.ModAPI;

namespace CleanFreak
{
	[MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation, priority: int.MinValue + 1)]
	internal class Core : BaseServerSessionComp
	{
		private const string GeneralLogName = "CoreGeneral";
		private const string DebugLogName = "CoreDebug";
		private const string SessionCompName = "Core";
		private const bool NoUpdate = false;

		private readonly ConcurrentDictionary<long, EntityModel> _entityModels = new ConcurrentDictionary<long, EntityModel>();

		private ulong _tickCounter;

		public Core() : base(GeneralLogName, DebugLogName, SessionCompName, NoUpdate) { } // Do nothing else

		/// <inheritdoc />
		protected override void EarlySetup()
		{
			base.EarlySetup();
			ImportCustomUserSettings.Run();
			Initialize();
		}

		private void Initialize()
		{
			MyAPIGateway.Entities.OnEntityAdd += OnEntityAdd;
			MyAPIGateway.Entities.OnEntityRemove += OnEntityRemoved;
		}

		/// <inheritdoc />
		protected override void LateSetup()
		{
			base.LateSetup();
			WriteToLog("LateSetup", $"Cargo: {MyAPIGateway.Session.SessionSettings.CargoShipsEnabled}", LogType.General);
			WriteToLog("LateSetup", $"Encounters: {MyAPIGateway.Session.SessionSettings.EnableEncounters}", LogType.General);
			WriteToLog("LateSetup", $"Drones: {MyAPIGateway.Session.SessionSettings.EnableDrones}", LogType.General);
			WriteToLog("LateSetup", $"Scripts: {MyAPIGateway.Session.SessionSettings.EnableIngameScripts}", LogType.General);
			WriteToLog("LateSetup", $"Sync: {MyAPIGateway.Session.SessionSettings.SyncDistance}", LogType.General);
			WriteToLog("LateSetup", $"View: {MyAPIGateway.Session.SessionSettings.ViewDistance}", LogType.General);
			WriteToLog("LateSetup", $"PiratePCU: {MyAPIGateway.Session.SessionSettings.PiratePCU}", LogType.General);
			WriteToLog("LateSetup", $"PlayerPCU: {MyAPIGateway.Session.SessionSettings.TotalPCU}", LogType.General);
			PrintConfig();
			foreach (MyObjectBuilder_Checkpoint.ModItem mod in MyAPIGateway.Session.Mods)
				WriteToLog("LateSetup", $"Mod: {mod}", LogType.General);
			MyAPIGateway.Parallel.StartBackground(Definitions.Initialize);
		}

		protected override void Unload()
		{
			Close();
			base.Unload();
		}

		private void Close()
		{
			MyAPIGateway.Entities.OnEntityAdd -= OnEntityAdd;
			MyAPIGateway.Entities.OnEntityRemove -= OnEntityRemoved;
			foreach (KeyValuePair<long, EntityModel> entity in _entityModels)
				EntityClose(entity.Value);
		}

		private void EntityClose(EntityModel entity)
		{
			entity.OnWriteToLog -= WriteToLog;
			entity.OnClose -= EntityClose;
			entity.Close();
			_entityModels.Remove(entity.ThisId);
		}

		private void EntityClose(long entityId)
		{
			EntityModel model;
			if (_entityModels.TryGetValue(entityId, out model))
				EntityClose(model);
		}

		private void OnEntityAdd(IMyEntity myEntity)
		{
			if (myEntity.GetType() != typeof(MyCubeGrid)) return;
			EntityModel entity = new EntityModel(myEntity, _tickCounter);
			entity.OnWriteToLog += WriteToLog;
			entity.OnClose += EntityClose;
			entity.Initialize();
			_entityModels.TryAdd(entity.ThisId, entity);
		}

		private void OnEntityRemoved(IMyEntity myEntity)
		{
			if (myEntity.GetType() != typeof(MyCubeGrid)) return;
		}

		public override void UpdateBeforeSimulation()
		{
			base.UpdateBeforeSimulation();
			if (UserSettings.IgnoreCleanupWhenNoPlayersOnline && MyAPIGateway.Players.Count == 0) return;
			_tickCounter++;
			if (_tickCounter % GeneralSettings.TicksPerMinute == 0)
				TriggerUpdate(1);
			if (_tickCounter % (GeneralSettings.TicksPerMinute + 10) == 0)
				TriggerUpdate(2);
			if (_tickCounter % (GeneralSettings.TicksPerMinute + 20) == 0)
				TriggerUpdate(3);
			if (_tickCounter % (GeneralSettings.TicksPerMinute + 30) == 0)
				TriggerUpdate(4);
		}

		private void TriggerUpdate(int which)
		{
			foreach (KeyValuePair<long, EntityModel> model in _entityModels)
			{
				switch (which)
				{
					case 1:
						model.Value.RunEvaluation(_tickCounter, CleanupType.Debris);
						break;
					case 2:
						model.Value.RunEvaluation(_tickCounter, CleanupType.Standard);
						break;
					case 3:
						model.Value.RunEvaluation(_tickCounter, CleanupType.Aggressive);
						break;
					case 4:
						model.Value.RunEvaluation(_tickCounter, CleanupType.SuperAggressive);
						break;
				}
			}
		}

		private void PrintConfig()
		{
			WriteToLog("Config", $"UseAggressiveCleanup: {UserSettings.UseAggressiveCleanup}", LogType.General);
			WriteToLog("Config", $"UseSuperAggressiveCleanup: {UserSettings.UseSuperAggressiveCleanup}", LogType.General);
			WriteToLog("Config", $"EncounterStandardCleanupRange: {UserSettings.EncounterStandardCleanupRange}", LogType.General);
			WriteToLog("Config", $"EncounterAggressiveCleanupRange: {UserSettings.EncounterAggressiveCleanupRange}", LogType.General);
			WriteToLog("Config", $"EncounterSuperAggressiveCleanupRange: {UserSettings.EncounterSuperAggressiveCleanupRange}", LogType.General);
			WriteToLog("Config", $"CargoStandardCleanupRange: {UserSettings.CargoStandardCleanupRange}", LogType.General);
			WriteToLog("Config", $"CargoAggressiveCleanupRange: {UserSettings.CargoAggressiveCleanupRange}", LogType.General);
			WriteToLog("Config", $"CargoSuperAggressiveCleanupRange: {UserSettings.CargoSuperAggressiveCleanupRange}", LogType.General);
			WriteToLog("Config", $"DebrisCleanupRange: {UserSettings.DebrisCleanupRange}", LogType.General);
			WriteToLog("Config", $"NpcCleanupInterval: {UserSettings.NpcCleanupInterval}", LogType.General);
			WriteToLog("Config", $"PassesBeforeDebrisCleanup: {UserSettings.PassesBeforeDebrisCleanup}", LogType.General);
			WriteToLog("Config", $"PassesBeforeStandardCleanup: {UserSettings.PassesBeforeStandardCleanup}", LogType.General);
			WriteToLog("Config", $"PassesBeforeAggressiveCleanup: {UserSettings.PassesBeforeAggressiveCleanup}", LogType.General);
			WriteToLog("Config", $"PassesBeforeSuperAggressiveCleanup: {UserSettings.PassesBeforeSuperAggressiveCleanup}", LogType.General);
			WriteToLog("Config", $"DebrisBlockCountThreshold: {UserSettings.DebrisBlockCountThreshold}", LogType.General);
			WriteToLog("Config", $"IgnoreCleanupWhenNoPlayersOnline: {UserSettings.IgnoreCleanupWhenNoPlayersOnline}", LogType.General);
            WriteToLog("Config", $"IgnoreGridVelocityCheck: {UserSettings.IgnoreGridVelocityCheck}", LogType.General);
            WriteToLog("Config", $"VerboseDebugLogging: {UserSettings.VerboseDebugLogging}", LogType.General);
        }
	}
}