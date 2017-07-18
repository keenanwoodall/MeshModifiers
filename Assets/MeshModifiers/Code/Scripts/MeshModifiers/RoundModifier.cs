using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Round")]
public class RoundModifier : MeshModifierBase
{
	public float nearestAmount = 1f;

	private void OnValidate ()
	{
		if (Mathf.Approximately(nearestAmount, 0f))
			nearestAmount = 0.0001f;
	}
	
	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		basePosition /= nearestAmount;
		return new Vector3 (Mathf.Round (basePosition.x), Mathf.Round (basePosition.y), Mathf.Round (basePosition.z)) * nearestAmount;
	}
}