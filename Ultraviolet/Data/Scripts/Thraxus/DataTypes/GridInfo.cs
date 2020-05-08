using VRageMath;

namespace Ultraviolet.Thraxus.DataTypes
{
	public class GridInfo
	{
		public Vector3 LinearVelocity;
		public Vector3D Position;
		public int ConsecutiveDebrisHits;
		public int ConsecutiveStandardHits;
		public int ConsecutiveAggressiveHits;
		public int ConsecutiveSuperAggressiveHits;
		public int BlockCount;
		public bool PrefabInfoObtained;
		public SpawnGroupPrefab PrefabInfo;

		public void ResetAll()
		{
			ConsecutiveDebrisHits = 0;
			ConsecutiveStandardHits = 0;
			ConsecutiveAggressiveHits = 0;
			ConsecutiveSuperAggressiveHits = 0;
		}

		public override string ToString()
		{
			return $"Debris: {ConsecutiveDebrisHits} | Standard: {ConsecutiveStandardHits} | Aggressive: {ConsecutiveAggressiveHits} | Super Aggressive: {ConsecutiveSuperAggressiveHits}";
		}
	}
}