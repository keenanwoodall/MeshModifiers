using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Twist")]
public class TwistModifier : MeshModifierBase
{
	public Axis axis;
	public float twistAmount;
	public AnimationCurve
		strengthCurve = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, 1f), new Keyframe (1f, 0f));

	private Bounds bounds;


	public override void PreMod ()
	{
		bounds = modObject.GetBounds ();
	}

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		var position = vertexData.position;
		float angle, cos, sin;

		switch (axis)
		{
			case Axis.X:
				angle = position.x * twistAmount;
				angle *= strengthCurve.Evaluate (Mathf.InverseLerp (-bounds.extents.x, bounds.extents.x, position.x));
				cos = Mathf.Cos (angle);
				sin = Mathf.Sin (angle);

				position.z = position.z * cos - position.y * sin;
				position.y = position.y * cos + position.z * sin;
				break;
			case Axis.Y:
				angle = position.y * twistAmount;
				angle *= strengthCurve.Evaluate (Mathf.InverseLerp (-bounds.extents.y, bounds.extents.y, position.y));
				cos = Mathf.Cos (angle);
				sin = Mathf.Sin (angle);

				position.x = position.x * cos - position.z * sin;
				position.z = position.z * cos + position.x * sin;
				break;
			case Axis.Z:
				angle = position.z * twistAmount;
				angle *= strengthCurve.Evaluate (Mathf.InverseLerp (-bounds.extents.z, bounds.extents.z, position.z));
				cos = Mathf.Cos (angle);
				sin = Mathf.Sin (angle);

				position.x = position.x * cos - position.y * sin;
				position.y = position.y * cos + position.x * sin;
				break;
		}
		return position;
	}
}