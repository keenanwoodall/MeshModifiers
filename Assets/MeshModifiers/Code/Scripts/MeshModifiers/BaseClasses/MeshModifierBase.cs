using UnityEngine;

namespace MeshModifiers
{
	public struct VertexData
	{
		public Vector3 position;
		public Vector3 normal;

		public VertexData (Vector3 position, Vector3 normal)
		{
			this.position = position;
			this.normal = normal;
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