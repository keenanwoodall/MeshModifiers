using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Curve Scale")]
public class CurveScaleModifier : MeshModifierBase
{
	private static Keyframe DEF_KEY_1 = new Keyframe (0f, 1f);
	private static Keyframe DEF_KEY_2 = new Keyframe (0.5f, 0.5f);
	private static Keyframe DEF_KEY_3 = new Keyframe (1f, 1f);

	public AnimationCurve
		xAxis = new AnimationCurve (DEF_KEY_1, DEF_KEY_2, DEF_KEY_3),
		yAxis = new AnimationCurve (DEF_KEY_1, DEF_KEY_2, DEF_KEY_3),
		zAxis = new AnimationCurve (DEF_KEY_1, DEF_KEY_2, DEF_KEY_3);

	private Bounds meshBounds;

	public override void PreMod ()
	{
		base.PreMod ();

		meshBounds = modObject.GetBounds ();
	}
	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		var position = vertexData.position;
		float
			z = xAxis.Evaluate (Mathf.InverseLerp (meshBounds.min.x, meshBounds.max.x, position.x)),
			x = yAxis.Evaluate (Mathf.InverseLerp (meshBounds.min.y, meshBounds.max.y, position.y)),
			y = zAxis.Evaluate (Mathf.InverseLerp (meshBounds.min.z, meshBounds.max.z, position.z));

		return Mathx.Vectorx.Multiply (position, new Vector3 (x, y, z));
	}
}