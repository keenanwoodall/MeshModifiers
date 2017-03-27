using System;
using UnityEngine;

namespace MeshModifiers
{
	//[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "FFD")]
	public class FreeFormDeformer : MeshModifierBase
	{
		#region Public Properties

		public Transform
			left,
			right,
			bottom,
			top,
			back,
			front;

		public Vector3 size;

		#endregion

		void Start ()
		{
			size = modObject.GetBounds ().size;
			left.position = transform.position + (-Vector3.right * size.x);
			right.position = transform.position + (Vector3.right * size.x);
			bottom.position = transform.position + (-Vector3.up * size.y);
			top.position = transform.position + (Vector3.up * size.y);
			back.position = transform.position + (-Vector3.forward * size.z);
			front.position = transform.position + (Vector3.forward * size.z);
		}

		void OnDrawGizmos ()
		{
			if (modObject == null)
				return;

			Gizmos.DrawWireCube (modObject.GetBounds ().center, modObject.GetBounds ().size);
		}

		#region Inherited Methods

		protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
		{
			float remapX, remapY, remapZ;

			remapX = Mathf.InverseLerp (-size.x , size.x, basePosition.x);
			remapY = Mathf.InverseLerp (-size.y, size.y, basePosition.y);
			remapZ = Mathf.InverseLerp (-size.z, size.z, basePosition.z);

			Vector3 xPosition = Vector3.LerpUnclamped (left.position - transform.position, right.position - transform.position, remapX);
			Vector3 yPosition = Vector3.LerpUnclamped (bottom.position - transform.position, top.position - transform.position, remapY);
			Vector3 zPosition = Vector3.LerpUnclamped (back.position - transform.position, front.position - transform.position, remapZ);

			return (xPosition + yPosition + zPosition) / 3f;
		}

		#endregion
	}
}