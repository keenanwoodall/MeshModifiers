using UnityEngine;

namespace MeshModifiers
{
	[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Curve Scale")]
	public class CurveScaleModifier : MeshModifierBase
	{
		public AnimationCurve curve = AnimationCurve.Linear (0f, 0f, 1f, 1f);
		public Axis by = Axis.X;
		public Axis along = Axis.Y;

		private Bounds meshBounds;

		public override void PreMod ()
		{
			meshBounds = modObject.GetBounds ();
		}
		protected override Vector3 _ModifyOffset (VertexData vertexData)
		{
			var position = vertexData.position;
			switch (along)
			{
				case Axis.X:
					switch (by)
					{
						case Axis.X:
							position.x *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.x, meshBounds.max.x, position.x));
							break;
						case Axis.Y:
							position.x *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.y, meshBounds.max.y, position.y));
							break;
						case Axis.Z:
							position.x *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.z, meshBounds.max.z, position.z));
							break;
					}
					break;
				case Axis.Y:
					switch (by)
					{
						case Axis.X:
							position.y *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.x, meshBounds.max.x, position.x));
							break;
						case Axis.Y:
							position.y *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.y, meshBounds.max.y, position.y));
							break;
						case Axis.Z:
							position.y *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.z, meshBounds.max.z, position.z));
							break;
					}
					break;
				case Axis.Z:
					switch (by)
					{
						case Axis.X:
							position.z *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.x, meshBounds.max.x, position.x));
							break;
						case Axis.Y:
							position.z *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.y, meshBounds.max.y, position.y));
							break;
						case Axis.Z:
							position.z *= curve.Evaluate (Mathf.InverseLerp (meshBounds.min.z, meshBounds.max.z, position.z));
							break;
					}
					break;
			}
			return position;
		}
	}
}