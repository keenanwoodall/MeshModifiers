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

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		if (absolute)
		{
			vertexData.position.x *= Mathf.Exp (Mathf.Abs (vertexData.position.x * value.x));
			vertexData.position.y *= Mathf.Exp (Mathf.Abs (vertexData.position.y * value.y));
			vertexData.position.z *= Mathf.Exp (Mathf.Abs (vertexData.position.z * value.z));
		}
		else
		{
			vertexData.position.x *= Mathf.Exp (vertexData.position.x * value.x);
			vertexData.position.y *= Mathf.Exp (vertexData.position.y * value.y);
			vertexData.position.z *= Mathf.Exp (vertexData.position.z * value.z);
		}

		return vertexData.position;
	}
}