using UnityEngine;
using Mathx;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Pinch")]
public class PinchModifier : MeshModifierBase
{
	public Vector3
		negativeAxis = Vector3.one,
		positiveAxis = Vector3.one;

	private Vector3 extents, doubleExtents;

	public override void PreMod ()
	{
		base.PreMod ();

		extents = modObject.GetBounds ().extents;
		doubleExtents = extents * 2;

		negativeAxis = Vectorx.Clamp (negativeAxis, Vector3.zero, Vector3.one);
		positiveAxis = Vectorx.Clamp (positiveAxis, Vector3.zero, Vector3.one);
	}

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		var v = basePosition;

		v.x = Mathf.Lerp (v.x, v.x * (positiveAxis.y), Mathf.Clamp01 ((v.y + extents.y) / doubleExtents.y) * (1.0f - positiveAxis.y));
		v.x = Mathf.Lerp (v.x, v.x * (negativeAxis.y), Mathf.Clamp01 ((-v.y + extents.y) / doubleExtents.y) * (1.0f - negativeAxis.y));
		v.x = Mathf.Lerp (v.x, v.x * (positiveAxis.z), Mathf.Clamp01 ((v.z + extents.z) / doubleExtents.z) * (1.0f - positiveAxis.z));
		v.x = Mathf.Lerp (v.x, v.x * (negativeAxis.z), Mathf.Clamp01 ((-v.z + extents.z) / doubleExtents.z) * (1.0f - negativeAxis.z));

		v.y = Mathf.Lerp (v.y, v.y * (positiveAxis.x), Mathf.Clamp01 ((v.x + extents.x) / doubleExtents.x) * (1.0f - positiveAxis.x));
		v.y = Mathf.Lerp (v.y, v.y * (negativeAxis.x), Mathf.Clamp01 ((-v.x + extents.x) / doubleExtents.x) * (1.0f - negativeAxis.x));
		v.y = Mathf.Lerp (v.y, v.y * (positiveAxis.z), Mathf.Clamp01 ((v.z + extents.z) / doubleExtents.z) * (1.0f - positiveAxis.z));
		v.y = Mathf.Lerp (v.y, v.y * (negativeAxis.z), Mathf.Clamp01 ((-v.z + extents.z) / doubleExtents.z) * (1.0f - negativeAxis.z));

		v.z = Mathf.Lerp (v.z, v.z * (positiveAxis.x), Mathf.Clamp01 ((v.x + extents.x) / doubleExtents.x) * (1.0f - positiveAxis.x));
		v.z = Mathf.Lerp (v.z, v.z * (negativeAxis.x), Mathf.Clamp01 ((-v.x + extents.x) / doubleExtents.x) * (1.0f - negativeAxis.x));
		v.z = Mathf.Lerp (v.z, v.z * (positiveAxis.y), Mathf.Clamp01 ((v.y + extents.y) / doubleExtents.y) * (1.0f - positiveAxis.y));
		v.z = Mathf.Lerp (v.z, v.z * (negativeAxis.y), Mathf.Clamp01 ((-v.y + extents.y) / doubleExtents.y) * (1.0f - negativeAxis.y));

		return v;
	}
}