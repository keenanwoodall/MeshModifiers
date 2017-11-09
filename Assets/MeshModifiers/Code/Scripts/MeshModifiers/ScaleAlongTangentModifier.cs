using UnityEngine;

namespace MeshModifiers
{
	[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Tangent Scale")]
	public class ScaleAlongTangentModifier : MeshModifierBase
	{
		public float amount;

		protected override Vector3 _ModifyOffset (VertexData vertexData)
		{
			return vertexData.position + ((Vector3)vertexData.tangent * amount);
		}
	}
}