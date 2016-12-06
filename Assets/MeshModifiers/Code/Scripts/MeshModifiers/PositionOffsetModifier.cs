using UnityEngine;
using System;
using System.Collections;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Position Offset")]
public class PositionOffsetModifier : MeshModifierBase
{
	#region Public Properties

	public Vector3 offset = Vector3.zero;

	#endregion



	#region Inherited Methods

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		return basePosition + offset;
	}

	#endregion
}
