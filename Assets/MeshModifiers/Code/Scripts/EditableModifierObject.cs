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

/* // This is too rough to be used right now.
using UnityEngine;

namespace MeshModifiers
{
	[RequireComponent (typeof (ModifierObject))]
	[AddComponentMenu ("Mesh Modifier/In Game Editor")]
	public class EditableModifierObject : MonoBehaviour
	{
		#region Static Properties

		protected static EditableModifierObject current;

		#endregion



		#region Public Properties

		public bool editable = true;

		public bool showingGUI { get; private set; }

		#endregion



		#region Private Properties

		private ModifierObject modObject;

		#endregion



		#region Unity Methods

		void Start ()
		{
			gameObject.AddComponent<BoxCollider> ();
			modObject = GetComponent<ModifierObject> ();
		}


		Rect viewRect = new Rect (0, 0, Screen.width / 8, Screen.height);
		void OnGUI ()
		{
			if (current != this)
				return;

			GUI.Box (viewRect, "");

			if (GUI.Button (new Rect (Screen.width / 8, 0, Screen.width / 15, Screen.height / 20), "Close"))
			{
				showingGUI = false;
				current = null;
				return;
			}

			GUILayout.BeginArea (viewRect);
			GUILayout.BeginVertical ();
			GUILayout.Label (modObject.transform.name);
			modObject.update = GUILayout.Toggle (modObject.update, "Update");

			GUILayout.Label ("Modifier Strength");
			modObject.modifierStrength = GUILayout.HorizontalSlider (modObject.modifierStrength, 0f, 1f);
			GUILayout.Label ("Modify Frames");
			modObject.modifyFrames = (int)GUILayout.HorizontalSlider (modObject.modifyFrames, 1f, MeshModifierConstants.MAX_MOD_FRAMES);

			GUILayout.Space (10);

			for (int i = 0; i < modObject.modifiers.Count; i++)
			{
				GUILayout.Space (10);
				GUILayout.Label (modObject.modifiers[i].GetType ().ToString ());

				modObject.modifiers[i].DrawEditorGUI ();
			}

			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}

		void OnMouseUpAsButton ()
		{
			if (!editable)
				return;

			if (current == this)
				current = null;
			else
				current = this;
		}

		#endregion
	}
}
*/