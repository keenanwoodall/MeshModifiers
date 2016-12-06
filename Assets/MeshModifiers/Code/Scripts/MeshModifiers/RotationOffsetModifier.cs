using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Rotation")]
public class RotationOffsetModifier : MeshModifierBase
{
	#region Public Properties

	public Vector3 offset = Vector3.zero;

	#endregion



	#region Inherited Methods

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		return Quaternion.Euler (offset) * basePosition;
	}

	#endregion
}