using UnityEngine;

namespace MeshModifiers
{
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

		public Vector3 ModifyOffset (Vector3 position, Vector3 normal)
		{
			return update ? _ModifyOffset (position, normal) : position;
		}

		protected abstract Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal);
	}
}