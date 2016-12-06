using UnityEngine;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Round To Nearest Amount")]
public class NearestXModifier : MeshModifierBase
{
	#region Public Properties

	public float nearestAmount = 1f;

	#endregion



	#region Unity Methods

	void OnValidate ()
	{
		if (nearestAmount == 0f)
			nearestAmount = 0.0001f;
	}

	#endregion



	#region Inherited Methods

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		basePosition /= nearestAmount;
		return new Vector3 (Mathf.Round (basePosition.x), Mathf.Round (basePosition.y), Mathf.Round (basePosition.z)) * nearestAmount;
	}

	#endregion
}