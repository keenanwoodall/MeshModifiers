using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Melt")]
public class MeltModifier : MeshModifierBase
{
	#region Fields

	public float 
		spread = 2f, 
		floorHeight = 0f;

	private float minY, maxY;

	#endregion



	#region Inherited Methods

	public override void PreMod ()
	{
		base.PreMod ();

		minY = modObject.GetBounds ().min.y;
		maxY = modObject.GetBounds ().max.y;
	}

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		float normalizedHeight = Mathf.InverseLerp (maxY, minY, basePosition.y + floorHeight);
		float scaleAmount = Mathf.Lerp (1f, 1f + Mathf.Abs (spread), normalizedHeight);
		return Vector3.Scale (basePosition, new Vector3 (scaleAmount, 1f, scaleAmount));
	}

	#endregion
}
