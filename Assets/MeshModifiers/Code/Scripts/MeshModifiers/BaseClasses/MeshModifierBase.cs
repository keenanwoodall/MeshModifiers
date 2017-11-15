using UnityEngine;

namespace MeshModifiers
{
	public struct VertexData
	{
		public Vector3 basePosition;
		public Vector3 position;
		public Vector3 normal;
		public Vector4 tangent;

		public VertexData (Vector3 basePosition, Vector3 position, Vector3 normal, Vector4 tangent)
		{
			this.basePosition = basePosition;
			this.position = position;
			this.normal = normal;
			this.tangent = tangent;
		}
	}
	[RequireComponent (typeof (ModifierObject))]
	public abstract class MeshModifierBase : MonoBehaviour
	{
		public bool update = true;

		protected ModifierObject modObject;
		private Mesh mesh;

		private void Awake ()
		{
			modObject = GetComponent<ModifierObject> ();
		}
		
		/// <summary>
		/// Performs any calculations that shouldn't be executed for each vertice.
		/// An example would be updating the modifiers reference to the meshes new bounds.
		/// </summary>
		public virtual void PreMod () { }
		
		/// <summary>
		/// Performs any calculations that shouldn't be executed for each vertice, after the modification is complete.
		/// An example would be updating the modifiers reference to the meshes new bounds.
		/// </summary>
		public virtual void PostMod () { }

		public Vector3 ModifyOffset (VertexData vertexData)
		{
			return update ? _ModifyOffset (vertexData) : vertexData.position;
		}

		protected abstract Vector3 _ModifyOffset (VertexData vertexData);
	}
}