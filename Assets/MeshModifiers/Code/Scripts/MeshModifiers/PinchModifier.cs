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

		//negativeAxis = Vectorx.Clamp (negativeAxis, Vector3.zero, Vector3.one);
		//positiveAxis = Vectorx.Clamp (positiveAxis, Vector3.zero, Vector3.one);
	}

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		var position = vertexData.position;

		position.x = Mathf.Lerp (position.x, position.x * (positiveAxis.y), Mathf.Clamp01 ((position.y + extents.y) / doubleExtents.y) * (1.0f - positiveAxis.y));
		position.x = Mathf.Lerp (position.x, position.x * (negativeAxis.y), Mathf.Clamp01 ((-position.y + extents.y) / doubleExtents.y) * (1.0f - negativeAxis.y));
		position.x = Mathf.Lerp (position.x, position.x * (positiveAxis.z), Mathf.Clamp01 ((position.z + extents.z) / doubleExtents.z) * (1.0f - positiveAxis.z));
		position.x = Mathf.Lerp (position.x, position.x * (negativeAxis.z), Mathf.Clamp01 ((-position.z + extents.z) / doubleExtents.z) * (1.0f - negativeAxis.z));

		position.y = Mathf.Lerp (position.y, position.y * (positiveAxis.x), Mathf.Clamp01 ((position.x + extents.x) / doubleExtents.x) * (1.0f - positiveAxis.x));
		position.y = Mathf.Lerp (position.y, position.y * (negativeAxis.x), Mathf.Clamp01 ((-position.x + extents.x) / doubleExtents.x) * (1.0f - negativeAxis.x));
		position.y = Mathf.Lerp (position.y, position.y * (positiveAxis.z), Mathf.Clamp01 ((position.z + extents.z) / doubleExtents.z) * (1.0f - positiveAxis.z));
		position.y = Mathf.Lerp (position.y, position.y * (negativeAxis.z), Mathf.Clamp01 ((-position.z + extents.z) / doubleExtents.z) * (1.0f - negativeAxis.z));

		position.z = Mathf.Lerp (position.z, position.z * (positiveAxis.x), Mathf.Clamp01 ((position.x + extents.x) / doubleExtents.x) * (1.0f - positiveAxis.x));
		position.z = Mathf.Lerp (position.z, position.z * (negativeAxis.x), Mathf.Clamp01 ((-position.x + extents.x) / doubleExtents.x) * (1.0f - negativeAxis.x));
		position.z = Mathf.Lerp (position.z, position.z * (positiveAxis.y), Mathf.Clamp01 ((position.y + extents.y) / doubleExtents.y) * (1.0f - positiveAxis.y));
		position.z = Mathf.Lerp (position.z, position.z * (negativeAxis.y), Mathf.Clamp01 ((-position.y + extents.y) / doubleExtents.y) * (1.0f - negativeAxis.y));

		return position;
	}
}