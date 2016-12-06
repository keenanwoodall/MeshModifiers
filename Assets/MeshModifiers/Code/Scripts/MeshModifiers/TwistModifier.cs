using UnityEngine;
using Mathx;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Twist")]
public class TwistModifier : MeshModifierBase
{
	#region Public Properties

	public Vector3 twist = Vector3.zero;

	#endregion


	#region Inherited Methods

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		Vector3 v = basePosition;
		float angle, cos, sin;

		if (twist.x != 0.0f)
		{
			angle = v.x * twist.x;
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			v.z = v.z * cos - v.y * sin;
			v.y = v.y * cos + v.z * sin;
		}

		if (twist.y != 0.0f)
		{
			angle = v.y * twist.y;
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			v.x = v.x * cos - v.z * sin;
			v.z = v.z * cos + v.x * sin;
		}

		if (twist.z != 0.0f)
		{
			angle = v.z * twist.z;
			cos = Mathf.Cos (angle);
			sin = Mathf.Sin (angle);

			v.x = v.x * cos - v.y * sin;
			v.y = v.y * cos + v.x * sin;
		}

		return v;
	}

	#endregion
}