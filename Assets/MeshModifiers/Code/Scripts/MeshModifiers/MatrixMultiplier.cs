using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Matrix")]
public class MatrixMultiplier : MeshModifierBase
{
	public Matrix4x4 matrix = Matrix4x4.identity;

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		vertexData.position = matrix.MultiplyPoint3x4 (vertexData.position);
		return vertexData.position;
	}
}