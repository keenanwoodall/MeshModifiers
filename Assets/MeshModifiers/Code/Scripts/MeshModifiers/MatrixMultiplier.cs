using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Matrix")]
public class MatrixMultiplier : MeshModifierBase
{
	public Matrix4x4 matrix = Matrix4x4.identity;

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		basePosition = matrix.MultiplyPoint3x4 (basePosition);
		return basePosition;
	}
}