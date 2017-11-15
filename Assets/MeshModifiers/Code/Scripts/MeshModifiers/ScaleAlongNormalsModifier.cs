using UnityEngine;

namespace MeshModifiers
{
	[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Normal Scale")]
	public class ScaleAlongNormalsModifier : MeshModifierBase
	{
		public float amount = 0f;

		protected override Vector3 _ModifyOffset (VertexData vertexData)
		{
			return vertexData.position + vertexData.normal * amount;
		}
	}
}