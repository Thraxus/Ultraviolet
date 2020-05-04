﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Sandbox.Game.Entities;
using Ultraviolet.Thraxus.Common.BaseClasses;
using Ultraviolet.Thraxus.Common.DataTypes;
using Ultraviolet.Thraxus.Common.Settings;
using Ultraviolet.Thraxus.Common.Utilities.Statics;
using Ultraviolet.Thraxus.Settings;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRageMath;

namespace Ultraviolet.Thraxus.Models
{
	public class EntityModel : LogBaseEvent
	{
		/// <summary>
		/// Since this is cleanup, the below are important things to track
		///		Grid BigOwner => ignore cleaning up player owned grids; always clean only NPC owned grids
		///		Grid SmallOwners => much more harsh rules for cleaning up grids even partially owned by a player
		///		Grids must meet the rules on 3 separate checks to be removed
		///		Checks are run every 30s
		///		Default cleanup range = 10km
		/// 
		/// Aggressive cleanup allowed between 5 and 10km; must meet double the check requirement
		///
		/// Debris cleanup allowed within 1km so long as it meets 3 checks in a row
		///
		/// Rules (assume no player SmallOwners):
		///
		/// Grid velocity = 0
		///		Ignore if station for now
		///		Default cleanup radius
		///		Must meet 3 checks in a row
		/// 
		/// Grid velocity = constant
		///		Total time in game world >= pre-determined tick timer duration (base 5m)
		///		No players within default radius
		///		Must meet 3 checks in a row
		///
		/// Grid block count = debris threshold
		///		No players within default debris radius
		///		Must be fully NPC owned or no owner
		/// </summary>
		
		public event Action<long> OnClose;

		private readonly GridInfo _lastPassInformation;

		private GridOwnerType _ownerType;
		
		private bool _hasPlayerSmallOwner;

		private bool _isClosed;
		
		private Vector3 LinearVelocity => _thisCubeGrid.Physics?.LinearVelocity ?? Vector3.Zero;

		private Vector3D Position => _thisMyCubeGrid.GetPosition();

		private GridType _gridType;

		private readonly MyCubeGrid _thisCubeGrid;

		private readonly IMyCubeGrid _thisMyCubeGrid;

		public readonly long ThisId;

		private readonly ulong _firstSeenTick;

		private List<MyEntity> _playersInRange = new List<MyEntity>();
		
		private int BlockCount => _thisCubeGrid.CubeBlocks.Count;

		private string _closeReason = "None";
		
		public EntityModel(IMyEntity thisEntity, ulong tick)
		{
			_firstSeenTick = tick;
			_thisCubeGrid = (MyCubeGrid) thisEntity;
			_thisMyCubeGrid = (IMyCubeGrid) thisEntity;
			ThisId = thisEntity.EntityId;
			base.Id = ThisId.ToString();
			_thisCubeGrid.OnClose += Close;
			_thisCubeGrid.OnBlockOwnershipChanged += OwnershipChanged;
			_thisCubeGrid.OnBlockAdded += BlockCountChanged;
			_thisCubeGrid.OnBlockRemoved += BlockCountChanged;
			_thisCubeGrid.OnGridSplit += GridSplit;
			_lastPassInformation = new GridInfo() { LinearVelocity = LinearVelocity, Position = Position, BlockCount = BlockCount};
			_lastPassInformation.ResetAll();
		}

		public void Initialize()
		{
			GridEvaluation();
			WriteToLog($"Initialize", $"Oh, hi! {BlockCount} | {_ownerType} | {_gridType}", LogType.General);
		}

		public void Close()
		{
			// Closing stuff happens here
			if (_isClosed) return;
			_isClosed = true;
			_thisCubeGrid.OnClose -= Close;
			_thisCubeGrid.OnBlockOwnershipChanged -= OwnershipChanged;
			_thisCubeGrid.OnBlockAdded -= BlockCountChanged;
			_thisCubeGrid.OnBlockRemoved -= BlockCountChanged;
			_thisCubeGrid.OnGridSplit -= GridSplit;
			WriteToLog($"Close", $"Oh, bye! {_closeReason} | {BlockCount} | {_ownerType} | {_gridType} | {_lastPassInformation}", LogType.General);
			OnClose?.Invoke(ThisId);
			_thisCubeGrid.Close();
		}

		private void Close(IMyEntity unused)
		{
			Close();
		}

		private bool InvalidEvaluation(ulong tickCounter)
		{
			if (_isClosed) return true;
			if (_ownerType == GridOwnerType.Player) return true;
			if (_gridType == GridType.Station || _gridType == GridType.Projection) return true;
			return tickCounter < _firstSeenTick + (UserSettings.NpcCleanupInterval * GeneralSettings.TicksPerMinute);
		}

		public void RunEvaluation(ulong tickCounter, CleanupType type)
		{
			if (InvalidEvaluation(tickCounter) || !ValidPass()) return;
			switch (type)
			{
				case CleanupType.Debris:
					if (_gridType != GridType.Debris) return;
					RunDebrisCleanup();
					break;
				case CleanupType.Standard:
					if (_hasPlayerSmallOwner) return;
					RunStandardCleanup();
					break;
				case CleanupType.Aggressive:
					if (_hasPlayerSmallOwner) return;
					RunAggressiveCleanup();
					break;
				case CleanupType.SuperAggressive:
					if (!_hasPlayerSmallOwner || !UserSettings.UseSuperAggressiveCleanup) return;
					RunSuperAggressiveCleanup();
					break;
				default:
					break;
			}
			WriteToLog("RunEvaluation", $"Pass Results: {_lastPassInformation}", LogType.General);
		}

		private void RunDebrisCleanup()
		{
			if (AnyPlayersInRange(UserSettings.DebrisCleanupRange))
			{
				_lastPassInformation.ConsecutiveDebrisHits = 0;
				WriteToLog("RunDebrisCleanup", $"Player in range; resetting.", LogType.General);
				return;
			}
			_lastPassInformation.ConsecutiveDebrisHits++;
			if (_lastPassInformation.ConsecutiveDebrisHits < UserSettings.PassesBeforeDebrisCleanup) return;
			WriteToLog("RunDebrisCleanup", $"No player in range; incrementing count.", LogType.General);
			_closeReason = "Debris";
			Close();
		}

		private void RunStandardCleanup()
		{
			if (AnyPlayersInRange(UserSettings.StandardCleanupRange))
			{
				_lastPassInformation.ConsecutiveStandardHits = 0;
				WriteToLog("RunStandardCleanup", $"Player in range; resetting.", LogType.General);
				return;
			}
			_lastPassInformation.ConsecutiveStandardHits++;
			if (_lastPassInformation.ConsecutiveStandardHits < UserSettings.PassesBeforeStandardCleanup) return;
			WriteToLog("RunStandardCleanup", $"No player in range; incrementing count.", LogType.General);
			_closeReason = "Standard";
			Close();
		}

		private void RunAggressiveCleanup()
		{
			if (AnyPlayersInRange(UserSettings.AggressiveCleanupRange))
			{
				_lastPassInformation.ConsecutiveAggressiveHits = 0;
				WriteToLog("RunAggressiveCleanup", $"Player in range; resetting.", LogType.General);
				return;
			}
			_lastPassInformation.ConsecutiveAggressiveHits++;
			WriteToLog("RunAggressiveCleanup", $"No player in range; incrementing count.", LogType.General);
			if (_lastPassInformation.ConsecutiveAggressiveHits < UserSettings.PassesBeforeAggressiveCleanup) return;
			_closeReason = "Aggressive";
			Close();
		}

		private void RunSuperAggressiveCleanup()
		{
			if (AnyPlayersInRange(UserSettings.SuperAggressiveCleanupRange))
			{
				_lastPassInformation.ConsecutiveSuperAggressiveHits = 0;
				WriteToLog("RunSuperAggressiveCleanup", $"Player in range; resetting.", LogType.General);
				return;
			}
			_lastPassInformation.ConsecutiveSuperAggressiveHits++;
			WriteToLog("RunSuperAggressiveCleanup", $"No player in range; incrementing count.", LogType.General);
			if (_lastPassInformation.ConsecutiveSuperAggressiveHits < UserSettings.PassesBeforeSuperAggressiveCleanup) return;
			_closeReason = "SuperAggressive";
			Close();
		}

		private bool AnyPlayersInRange(int range)
		{
			_playersInRange.Clear();
			_playersInRange = Statics.DetectTopMostEntitiesInSphere(Position, range) as List<MyEntity>;
			if (_playersInRange == null) return false;
			bool playerFound = false;
			foreach (MyEntity ent in _playersInRange)
			{
				if (!Statics.ValidPlayer(ent.EntityId)) continue;
				playerFound = true;
				break;
			}
			return playerFound;
		}

		private bool ValidPass()
		{
			if (LinearVelocity == Vector3.Zero && _lastPassInformation.Position == Position)
				return true;

			if (LinearVelocity == _lastPassInformation.LinearVelocity)
				return true;

			_lastPassInformation.Position = Position;
			_lastPassInformation.LinearVelocity = LinearVelocity;
			_lastPassInformation.BlockCount = BlockCount;
			_lastPassInformation.ResetAll();
			return false;
		}

		private void BlockCountChanged(IMySlimBlock unused)
		{
			GridEvaluation();
		}
		
		private void GridSplit(MyCubeGrid unused, MyCubeGrid alsoUnused)
		{
			GridEvaluation();
		}

		private void OwnershipChanged(MyCubeGrid unused)
		{
			GetOwnerType();
		}

		private void GridEvaluation()
		{
			GetOwnerType();
			GetGridType();
		}

		private void GetOwnerType()
		{
			if (_thisCubeGrid.BigOwners.Count == 0)
			{
				_ownerType = GridOwnerType.None;
				return;
			}
			_ownerType = Statics.ValidPlayer(_thisCubeGrid.BigOwners[0]) ? GridOwnerType.Player : GridOwnerType.Npc;
			if (_thisCubeGrid.SmallOwners.Count > _thisCubeGrid.BigOwners.Count && _ownerType != GridOwnerType.Player)
				CheckSmallOwners();
			else _hasPlayerSmallOwner = false;
		}

		private void CheckSmallOwners()
		{
			_hasPlayerSmallOwner = false;
			foreach (long id in _thisCubeGrid.SmallOwners)
			{
				if (!Statics.ValidPlayer(id)) continue;
				_hasPlayerSmallOwner = true;
				break;
			}
		}

		private void GetGridType()
		{
			if (_thisCubeGrid.Physics == null)
			{
				_gridType = GridType.Projection;
				return;
			}

			if (_thisCubeGrid.CubeBlocks.Count < UserSettings.DebrisBlockCountThreshold)
			{
				_gridType = GridType.Debris;
				return;
			}

			if (_thisCubeGrid.IsStatic || _thisCubeGrid.IsUnsupportedStation)
			{
				_gridType = GridType.Station;
				return;
			}
			_gridType = GridType.Ship;
		}
	}

	public class GridInfo
	{
		public Vector3 LinearVelocity;
		public Vector3D Position;
		public int ConsecutiveDebrisHits;
		public int ConsecutiveStandardHits;
		public int ConsecutiveAggressiveHits;
		public int ConsecutiveSuperAggressiveHits;
		public int BlockCount;
		
		public void ResetAll()
		{
			ConsecutiveDebrisHits = 0;
			ConsecutiveStandardHits = 0;
			ConsecutiveAggressiveHits = 0;
			ConsecutiveSuperAggressiveHits = 0;
		}

		public override string ToString()
		{
			return $"Linear Velocity: {LinearVelocity} | Position: {Position} | Debris: {ConsecutiveDebrisHits} | Standard: {ConsecutiveStandardHits} | Aggressive: {ConsecutiveAggressiveHits} | Super Aggressive: {ConsecutiveSuperAggressiveHits} | Block Count: {BlockCount}";
		}
	}

	public enum CleanupType
	{
		Debris, 
		Standard,
		Aggressive,
		SuperAggressive
	}
}