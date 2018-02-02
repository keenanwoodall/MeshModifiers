using UnityEngine;

namespace MeshModifiers
{
	[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Anti Modifier")]
	public class AntiModifier : MeshModifierBase
	{
		public float strength = 0.5f;
		public float radius = 0.5f;
		public bool invert;
		public Vector3 position;

		private void OnDrawGizmosSelected ()
		{
			Gizmos.DrawWireSphere (transform.position + (transform.rotation * position), radius);
		}
		protected override Vector3 _ModifyOffset (VertexData vertexData)
		{
			var distance = Vector3.Distance (vertexData.position, position);
			if (!invert)
			{
				if (distance < radius)
				{
					return Vector3.Lerp (vertexData.position, vertexData.basePosition, (distance / radius) * strength);
				}
			}
			else
			{
				if (distance > radius)
				{
					return Vector3.Lerp (vertexData.position, vertexData.basePosition, (distance / radius) * strength);
				}
			}
			return vertexData.position;
		}
	}
}