using UnityEngine;
using Mathx;
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

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		var v = basePosition;
		float angle, cos, sin;

		if (twist.x != 0.0f)
		{
			angle = v.x * twist.x;
			angle *= xStrength.Evaluate(Mathf.InverseLerp (-bounds.extents.x, bounds.extents.x, v.x));
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			v.z = v.z * cos - v.y * sin;
			v.y = v.y * cos + v.z * sin;
		}

		if (twist.y != 0.0f)
		{
			angle = v.y * twist.y;
			angle *= yStrength.Evaluate (Mathf.InverseLerp (-bounds.extents.y, bounds.extents.y, v.y));
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			v.x = v.x * cos - v.z * sin;
			v.z = v.z * cos + v.x * sin;
		}

		if (twist.z != 0.0f)
		{
			angle = v.z * twist.z;
			angle *= zStrength.Evaluate (Mathf.InverseLerp (-bounds.extents.z, bounds.extents.z, v.z));
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			v.x = v.x * cos - v.y * sin;
			v.y = v.y * cos + v.x * sin;
		}
		return v;
	}
}