using System.Collections.Concurrent;
using System.Collections.Generic;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using Ultraviolet.Thraxus.Common.BaseClasses;
using Ultraviolet.Thraxus.Common.DataTypes;
using Ultraviolet.Thraxus.Common.Settings;
using Ultraviolet.Thraxus.Models;
using VRage.Game;
using VRage.Game.Components;
using VRage.ModAPI;

namespace Ultraviolet.Thraxus
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
			WriteToLog("LateSetup", $"TotalPCU: {MyAPIGateway.Session.SessionSettings.TotalPCU}", LogType.General);
			foreach (MyObjectBuilder_Checkpoint.ModItem mod in MyAPIGateway.Session.Mods)
				WriteToLog("LateSetup", $"Mod: {mod}", LogType.General);
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
			_tickCounter++;
			if (_tickCounter % GeneralSettings.TicksPerMinute == 0)
				TriggerUpdate();
		}

		private void TriggerUpdate()
		{
			foreach (KeyValuePair<long, EntityModel> model in _entityModels)
			{
				model.Value.RunEvaluation(_tickCounter);
			}
		}
	}
}
