using UnityEngine;
using Mathx.Trig;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Trig")]
public class TrigModifier : MeshModifierBase
{
	public Vector3 
		offset = Vector3.zero,
		speed = Vector3.one;

	public Trig 
		xAxis = new Trig (TrigType.Sin),
		yAxis = new Trig (TrigType.Sin),
		zAxis = new Trig (TrigType.Sin);

	public bool 
		useWorldPos = false,
		radial = false;

	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		var position = vertexData.position;
		if (radial)
		{
			if (useWorldPos)
				return position * (0.5f + TrigOffset (transform.position + Vector3.one * Vector3.Distance (transform.position, position + transform.position + offset)).magnitude);
			else
				return position * (0.5f + TrigOffset (Vector3.one * Vector3.Distance (transform.position, position + transform.position + offset)).magnitude);
		}
		else
		{
			if (useWorldPos)
				return position + TrigOffset (position + transform.position + offset);
			else
				return position + TrigOffset (position + offset);
		}
	}

	private Vector3 TrigOffset (Vector3 basePosition)
	{
		return new Vector3 (Trig3 (basePosition).x, Trig3 (basePosition).y, Trig3 (basePosition).z);
	}

	private Vector3 Trig3 (Vector3 t)
	{
		var offset = speed * modObject.Time;
		return new Vector3 ((xAxis.amplitude == 0) ? 0f : xAxis.Solve (t.z, offset.x), (yAxis.amplitude == 0) ? 0f : yAxis.Solve (t.x, offset.y), (zAxis.amplitude == 0) ? 0f : zAxis.Solve (t.y, offset.z));
	}
}