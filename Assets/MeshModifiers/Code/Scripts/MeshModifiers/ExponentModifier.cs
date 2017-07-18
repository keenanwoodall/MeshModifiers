using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Exponent")]
public class ExponentModifier : MeshModifierBase
{
	public Vector3 value = Vector3.zero;
	public bool absolute = true;

	public override void PreMod ()
	{
		base.PreMod ();
	}

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		if (absolute)
		{
			basePosition.x *= Mathf.Exp (Mathf.Abs (basePosition.x * value.x));
			basePosition.y *= Mathf.Exp (Mathf.Abs (basePosition.y * value.y));
			basePosition.z *= Mathf.Exp (Mathf.Abs (basePosition.z * value.z));
		}
		else
		{
			basePosition.x *= Mathf.Exp (basePosition.x * value.x);
			basePosition.y *= Mathf.Exp (basePosition.y * value.y);
			basePosition.z *= Mathf.Exp (basePosition.z * value.z);
		}

		return basePosition;
	}
}