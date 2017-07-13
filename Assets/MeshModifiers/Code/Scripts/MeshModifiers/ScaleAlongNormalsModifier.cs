using UnityEngine;
using System;
using System.Collections;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Normal Scale")]
public class ScaleAlongNormalsModifier : MeshModifierBase
{
	public float amount = 0f;

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		return basePosition + baseNormal * amount;
	}
}