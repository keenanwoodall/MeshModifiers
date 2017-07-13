using System;
using UnityEngine;

namespace MeshModifiers
{
	//[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "FFD")]
	public class FreeFormDeformer : MeshModifierBase
	{
		public Transform
			left,
			right,
			bottom,
			top,
			back,
			front;

		public Vector3 size;

		private void Start ()
		{
			size = modObject.GetBounds ().size;
			left.position = transform.position + (-Vector3.right * size.x);
			right.position = transform.position + (Vector3.right * size.x);
			bottom.position = transform.position + (-Vector3.up * size.y);
			top.position = transform.position + (Vector3.up * size.y);
			back.position = transform.position + (-Vector3.forward * size.z);
			front.position = transform.position + (Vector3.forward * size.z);
		}

		private void OnDrawGizmos ()
		{
			if (modObject == null)
				return;

			Gizmos.DrawWireCube (modObject.GetBounds ().center, modObject.GetBounds ().size);
		}

		protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
		{
			var remapX = Mathf.InverseLerp (-size.x , size.x, basePosition.x);
			var remapY = Mathf.InverseLerp (-size.y, size.y, basePosition.y);
			var remapZ = Mathf.InverseLerp (-size.z, size.z, basePosition.z);

			var xPosition = Vector3.LerpUnclamped (left.position - transform.position, right.position - transform.position, remapX);
			var yPosition = Vector3.LerpUnclamped (bottom.position - transform.position, top.position - transform.position, remapY);
			var zPosition = Vector3.LerpUnclamped (back.position - transform.position, front.position - transform.position, remapZ);

			return (xPosition + yPosition + zPosition) / 3f;
		}
	}
}