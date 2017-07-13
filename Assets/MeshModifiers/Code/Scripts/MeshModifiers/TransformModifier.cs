using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Transform")]
public class TransformModifier : MeshModifierBase
{
	public Vector3 
		position = Vector3.zero,
		rotation = Vector3.zero,
		scale = Vector3.one;

	private Matrix4x4 transformSpace;

	public override void PreMod ()
	{
		base.PreMod ();

		transformSpace = Matrix4x4.identity;
		transformSpace *= Matrix4x4.TRS (position, Quaternion.Euler (rotation), scale);
	}

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		return transformSpace.MultiplyPoint3x4 (basePosition);
	}
}