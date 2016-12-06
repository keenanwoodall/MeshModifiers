using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Curve Scale")]
public class CurveScaleModifier : MeshModifierBase
{
	#region Constant Properties

	private static Keyframe DEF_KEY_1 = new Keyframe (0f, 1f);
	private static Keyframe DEF_KEY_2 = new Keyframe (0.5f, 0.5f);
	private static Keyframe DEF_KEY_3 = new Keyframe (1f, 1f);

	#endregion



	#region Public Properties

	public AnimationCurve
		xAxis = new AnimationCurve (DEF_KEY_1, DEF_KEY_2, DEF_KEY_3),
		yAxis = new AnimationCurve (DEF_KEY_1, DEF_KEY_2, DEF_KEY_3),
		zAxis = new AnimationCurve (DEF_KEY_1, DEF_KEY_2, DEF_KEY_3);

	#endregion



	#region Private Properties

	private Bounds meshBounds;

	#endregion



	#region Inherited Methods

	public override void PreMod ()
	{
		base.PreMod ();

		meshBounds = modObject.GetBounds ();
	}
	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		float z = xAxis.Evaluate (Mathf.InverseLerp (meshBounds.min.x, meshBounds.max.x, basePosition.x));
		float x = yAxis.Evaluate (Mathf.InverseLerp (meshBounds.min.y, meshBounds.max.y, basePosition.y));
		float y = zAxis.Evaluate (Mathf.InverseLerp (meshBounds.min.z, meshBounds.max.z, basePosition.z));

		return Mathx.Vectorx.Multiply (basePosition, new Vector3 (x, y, z));
	}

	#endregion
}