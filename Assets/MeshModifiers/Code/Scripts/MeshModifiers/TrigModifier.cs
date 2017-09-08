using UnityEngine;
using Mathx.Trig;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Trig")]
public class TrigModifier : MeshModifierBase
{
	public float
		offset,
		speed;
	public Axis along;
	public Axis by;
	public Trig
		trig = new Trig (TrigType.Sin);
	public bool useWorldPos = false;

	private Vector3 axisOffset;


	public override void PreMod ()
	{
		switch (along)
		{
			case Axis.X:
				axisOffset = Vector3.right * offset;
				break;
			case Axis.Y:
				axisOffset = Vector3.up * offset;
				break;
			case Axis.Z:
				axisOffset = Vector3.forward * offset;
				break;
		}
	}
	protected override Vector3 _ModifyOffset (VertexData vertexData)
	{
		var position = vertexData.position;
		if (useWorldPos)
			return position + TrigOffset (position + transform.position + axisOffset);
		else
			return position + TrigOffset (position + axisOffset);
	}

	private Vector3 TrigOffset (Vector3 basePosition)
	{
		return new Vector3 (Trig3 (basePosition).x, Trig3 (basePosition).y, Trig3 (basePosition).z);
	}

	private Vector3 Trig3 (Vector3 t)
	{
		var animatedOffset = speed * modObject.Time;
		var byValue = 0f;
		switch (by)
		{
			case Axis.X:
				byValue = t.x;
				break;
			case Axis.Y:
				byValue = t.y;
				break;
			case Axis.Z:
				byValue = t.z;
				break;
		}
		switch (along)
		{
			case Axis.X:
				return new Vector3 (trig.Solve (byValue, animatedOffset), 0f, 0f);
			case Axis.Y:
				return new Vector3 (0f, trig.Solve (byValue, animatedOffset), 0f);
			default:
				return new Vector3 (0f, 0f, trig.Solve (byValue, animatedOffset));
		}
	}
}