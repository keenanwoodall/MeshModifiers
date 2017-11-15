using UnityEngine;

namespace MeshModifiers
{
	[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Round")]
	public class RoundModifier : MeshModifierBase
	{
		public float nearestAmount = 1f;

		private void OnValidate ()
		{
			if (Mathf.Approximately (nearestAmount, 0f))
				nearestAmount = 0.0001f;
		}

		protected override Vector3 _ModifyOffset (VertexData vertexData)
		{
			var position = vertexData.position /= nearestAmount;
			return new Vector3 (Mathf.Round (position.x), Mathf.Round (position.y), Mathf.Round (position.z)) * nearestAmount;
		}
	}
}