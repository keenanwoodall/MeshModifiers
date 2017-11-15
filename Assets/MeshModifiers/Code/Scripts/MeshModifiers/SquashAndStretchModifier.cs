using UnityEngine;

namespace MeshModifiers
{
	[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Squash and Stretch")]
	public class SquashAndStretchModifier : MeshModifierBase
	{
		public float amount = 0f;

		public bool inWorldSpace = false;

		private Matrix4x4
			scaleSpace,
			meshSpace;

		private Vector3
			scaleAxisX,
			scaleAxisY,
			scaleAxisZ;

		[SerializeField]
		private Vector3 skewRotation = new Vector3 (90f, 0f, 0f);
		public Vector3 SkewDirection
		{
			get { return Quaternion.Euler (skewRotation) * ((inWorldSpace) ? Vector3.forward : transform.forward); }
		}

		private void OnValidate ()
		{
			if (amount <= -0.9f)
				amount = -0.89f;
		}

		public override void PreMod ()
		{
			base.PreMod ();

			scaleAxisX = inWorldSpace ? transform.worldToLocalMatrix.MultiplyPoint3x4 (SkewDirection) : SkewDirection;

			Vector3.OrthoNormalize (ref scaleAxisX, ref scaleAxisY, ref scaleAxisZ);

			scaleSpace.SetRow (0, scaleAxisX);
			scaleSpace.SetRow (1, scaleAxisY);
			scaleSpace.SetRow (2, scaleAxisZ);
			scaleSpace[3, 3] = 1f;

			if (!inWorldSpace)
				scaleSpace *= Matrix4x4.TRS (Vector3.zero, modObject.transform.rotation, Vector3.one);

			meshSpace = scaleSpace.inverse;
		}

		protected override Vector3 _ModifyOffset (VertexData vertexData)
		{
			if (Mathf.Approximately (amount, 0f))
				return vertexData.position;

			var basePositionOnAxis = scaleSpace.MultiplyPoint3x4 (vertexData.position);
			var amountPlus1 = amount + 1f;

			basePositionOnAxis.x *= amountPlus1;
			basePositionOnAxis.y *= 1f / amountPlus1;
			basePositionOnAxis.z *= 1f / amountPlus1;

			return meshSpace.MultiplyPoint3x4 (basePositionOnAxis);
		}
	}
}