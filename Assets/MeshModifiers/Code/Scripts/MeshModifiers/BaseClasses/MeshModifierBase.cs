/*
* Copyright (c) 2016 Keenan Woodall
*
* This software is provided 'as-is', without any express or implied
* warranty. In no event will the authors be held liable for any damages
* arising from the use of this software.
*
* Permission is granted to anyone to use this software for any purpose,
* including commercial applications, and to alter it and redistribute it
* freely, subject to the following restrictions:
* 
*    1. The origin of this software must not be misrepresented; you must not
*    claim that you wrote the original software. If you use this software
*    in a product, an acknowledgment in the product documentation would be
*    appreciated but is not required.
* 
*    2. Altered source versions must be plainly marked as such, and must not be
*    misrepresented as being the original software.
* 
*    3. This notice may not be removed or altered from any source
*    distribution.
*/

using UnityEngine;

namespace MeshModifiers
{
	[RequireComponent (typeof (ModifierObject))]
	[ExecuteInEditMode]
	public abstract class MeshModifierBase : MonoBehaviour
	{
		#region Fields

		public bool update = true;

		protected ModifierObject modObject;
		private Mesh mesh;

		#endregion



		#region Monobehaviour Methods

		void Awake ()
		{
			modObject = GetComponent<ModifierObject> ();
		}

		private void OnValidate ()
		{
			if (modObject == null)
				modObject = GetComponent<ModifierObject> ();
			if (modObject != null && modObject.autoUpdate)
			{
				modObject.ModifyAll (invokePreMods: true, invokePostMods: true);
				modObject.ApplyModifications ();
			}
		}

		#endregion



		#region Main Methods

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
			if (update)
				return _ModifyOffset (position, normal);
			else
				return position;
		}

		protected abstract Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal);

		#endregion
	}
}