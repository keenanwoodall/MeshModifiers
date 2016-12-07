using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Squash and Stretch")]
public class SquashAndStretchModifier : MeshModifierBase
{
	#region Public Properties

	public Vector3 direction = Vector3.up;

	public float amount = 1f;

	public bool inWorldSpace = true;

	#endregion


	#region Private Properties

	private Matrix4x4
		scaleSpace,
		meshSpace;

	private Vector3
		scaleAxisX,
		scaleAxisY,
		scaleAxisZ;

	#endregion



	#region Inherited Methods

	public override void PreMod ()
	{
		base.PreMod ();

		if (inWorldSpace)
			scaleAxisX = transform.worldToLocalMatrix.MultiplyPoint3x4 (direction);
		else
			scaleAxisX = direction;

		Vector3.OrthoNormalize (ref scaleAxisX, ref scaleAxisY, ref scaleAxisZ);

		scaleSpace.SetRow (0, scaleAxisX);
		scaleSpace.SetRow (1, scaleAxisY);
		scaleSpace.SetRow (2, scaleAxisZ);
		scaleSpace[3, 3] = 1f;

		meshSpace = scaleSpace.inverse;
	}

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		Vector3 basePositionOnAxis = scaleSpace.MultiplyPoint3x4 (basePosition);
		float nonZeroAmount = (Mathf.Abs (amount) > 0.001f) ? amount : ((amount > 0f) ? 0.001f : -0.001f);


		basePositionOnAxis.x *= nonZeroAmount;
		basePositionOnAxis.y *= 1f / nonZeroAmount;
		basePositionOnAxis.z *= 1f / nonZeroAmount;

		return meshSpace.MultiplyPoint3x4 (basePositionOnAxis);
	}

	#endregion
}