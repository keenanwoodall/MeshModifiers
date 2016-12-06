using UnityEngine;
using Mathx.Trig;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Trig")]
public class TrigModifier : MeshModifierBase
{
	#region Public Properties

	public Vector3 offset = Vector3.zero;
	public Vector3 speed = Vector3.one;

	public Trig 
		xAxis,
		yAxis,
		zAxis;

	public bool useWorldPos = false;

	public bool radial = false;

	#endregion



	#region Inherited Methods

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		if (radial)
		{
			if (useWorldPos)
				return basePosition * (0.5f + TrigOffset (transform.position + Vector3.one * Vector3.Distance (transform.position, basePosition + transform.position + offset)).magnitude);
			else
				return basePosition * (0.5f + TrigOffset (Vector3.one * Vector3.Distance (transform.position, basePosition + transform.position + offset)).magnitude);
		}
		else
		{
			if (useWorldPos)
				return basePosition + TrigOffset (basePosition + transform.position + offset);
			else
				return basePosition + TrigOffset (basePosition + offset);
		}
	}

	#endregion



	#region Utility Methods

	private Vector3 TrigOffset (Vector3 basePosition)
	{
		return new Vector3 (Trig3 (basePosition).x, Trig3 (basePosition).y, Trig3 (basePosition).z);
	}

	private Vector3 Trig3 (Vector3 t)
	{
		Vector3 offset = speed * modObject.Time;
		return new Vector3 (xAxis.Solve (t.z, offset.x), yAxis.Solve (t.x, offset.y), zAxis.Solve (t.y, offset.z));
	}

	#endregion
}