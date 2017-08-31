using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Twist")]
public class TwistModifier : MeshModifierBase
{
	public Vector3 twist = Vector3.zero;

	public AnimationCurve
		xStrength = new AnimationCurve (new Keyframe(0f, 0f), new Keyframe (0.5f, 1f), new Keyframe (1f, 0f)),
		yStrength = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, 1f), new Keyframe (1f, 0f)),
		zStrength = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, 1f), new Keyframe (1f, 0f));

	private Bounds bounds;

	public override void PreMod ()
	{
		bounds = modObject.GetBounds ();
	}

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		var position = vertexData.position;
		float angle, cos, sin;

		if (twist.x != 0.0f)
		{
			angle = position.x * twist.x;
			angle *= xStrength.Evaluate(Mathf.InverseLerp (-bounds.extents.x, bounds.extents.x, position.x));
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			position.z = position.z * cos - position.y * sin;
			position.y = position.y * cos + position.z * sin;
		}

		if (twist.y != 0.0f)
		{
			angle = position.y * twist.y;
			angle *= yStrength.Evaluate (Mathf.InverseLerp (-bounds.extents.y, bounds.extents.y, position.y));
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			position.x = position.x * cos - position.z * sin;
			position.z = position.z * cos + position.x * sin;
		}

		if (twist.z != 0.0f)
		{
			angle = position.z * twist.z;
			angle *= zStrength.Evaluate (Mathf.InverseLerp (-bounds.extents.z, bounds.extents.z, position.z));
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			position.x = position.x * cos - position.y * sin;
			position.y = position.y * cos + position.x * sin;
		}
		return position;
	}
}