using UnityEngine;

namespace MeshModifiers
{
	[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Spherize")]
	public class SpherizeModifier : MeshModifierBase
	{
		[Range (0f, 1f)]
		public float amount = 1f;
		public float radius = 1f;

		protected override Vector3 _ModifyOffset (VertexData vertexData)
		{
			return Vector3.Lerp (vertexData.position, vertexData.position.normalized * radius, amount);
		}
	}
}