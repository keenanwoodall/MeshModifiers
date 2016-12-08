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
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace MeshModifiers
{
	[RequireComponent (typeof (MeshFilter))]
	public class ModifierObject : MonoBehaviour
	{
		#region Public Properties

		[Header ("General")]

		public bool update = true;

		[Range (0f, 1f)] public float modifierStrength = 1f;


		[Header ("Surface")]

		[Tooltip (MeshModifierConstants.NORMALS_QUALITY_TOOLTIP)]
		public NormalsQuality normalQuality = NormalsQuality.LowQuality;

		[Range (0f, 180f), Tooltip (MeshModifierConstants.SMOOTHING_ANGLE_TOOLTIP)]
		public float smoothingAngle = 60f;


		[Header ("Performance")]

		[System.NonSerialized, Tooltip (MeshModifierConstants.UPDATE_WHEN_HIDDEN_TOOLTIP)]
		public bool updateWhenHidden = false;

		[Range (1, MeshModifierConstants.MAX_MOD_FRAMES), Tooltip (MeshModifierConstants.MODIFY_FRAMES_TOOLTIP)]
		public int modifyFrames = 1;



		[System.NonSerialized]
		public List<MeshModifierBase> modifiers = new List<MeshModifierBase> ();

		[System.NonSerialized]
		public bool refreshModifiersEveryFrame = true;

		/// <summary>
		/// Updated based on Unity's OnWillRenderObject and OnBecameVisible.
		/// </summary>
		public bool isVisible { get; private set; }

		/// <summary>
		/// The index that corresponds the current chunk of vertices being modified.
		/// </summary>
		public int currentModifiedChunkIndex { get; private set; }

		/// <summary>
		/// Local to the modifier object. Used to sync modifications that occur over multiple frames and require a time value.
		/// </summary>
		public float Time { get; private set; }

		#endregion



		#region Backed Properties

		private MeshFilter filter;
		public MeshFilter Filter
		{
			get
			{
				if (filter == null)
					filter = GetComponent<MeshFilter> ();
				return filter;
			}
			set { filter = value; }
		}

		private Mesh mesh;
		public Mesh Mesh
		{
			get
			{
				if (mesh == null)
					mesh = filter.mesh;
				return mesh;
			}
			set { mesh = value; }
		}

		#endregion



		#region Private Properties

		private Vector3[] baseVertices;
		private Vector3[] modifiedVertices;
		private Vector3[] baseNormals;
		private Vector3[] modifiedNormals;

		#endregion



		#region Unity Methods

		void Awake ()
		{
			Filter = GetComponent<MeshFilter> ();

			ChangeMesh (Filter.mesh);
		}

		void Start ()
		{
			RefreshModifiers ();
			StartCoroutine (AutoApplyModifiers ());
		}

		void OnWillRenderObject ()
		{
			isVisible = true;
		}

		void OnBecameInvisible ()
		{
			isVisible = false;
		}

		#endregion



		#region Main Methods

		/// <summary>
		/// Modifies the whole mesh, as one chunk. If the parameter 'modifierIndexes' is null it will be set using GetUseableModifiers;
		/// [HINT]: This method only changes the internal data, call ApplyModifications for the mesh data to reflect the internal data.
		/// </summary>
		/// <param name="modifierIndexes"></param>
		public void ModifyAll (int[] modifierIndexes = null)
		{
			ModifyChunk (0, baseVertices.Length, modifierIndexes);
		}

		/// <summary>
		/// Modifies chunk of the mesh. If the parameter, modifierIndexes, is null it will be set using GetUseableModifiers. 
		/// [HINT]: This method only changes the internal data, call ApplyModifications for the mesh data to reflect the internal data.
		/// </summary>
		/// <param name="startIndex"></param>
		/// <param name="stopIndex"></param>
		/// <param name="modifierIndexes"></param>
		public void ModifyChunk (int startIndex, int stopIndex, int[] modifierIndexes = null)
		{
			// If modifierIndexes are not supplied, use all useabled modifiers.
			if (modifierIndexes == null)
				modifierIndexes = GetUseableModifiers ();

			// For each vertex in this chunk...
			for (int currentVert = startIndex; currentVert < stopIndex; currentVert++)
			{
				// For each modifier...
				for (int currentMod = 0; currentMod < modifierIndexes.Length; currentMod++)
					modifiedVertices[currentVert] = modifiers[modifierIndexes[currentMod]].ModifyOffset (modifiedVertices[currentVert], modifiedNormals[currentVert]);

				PostModPointOperation (currentVert);
			}
		}

		/// <summary>
		/// Call this after calling ModifyAll or ModifyChunk for the changes to be applied the the actual mesh.
		/// </summary>
		public void ApplyModifications ()
		{
			// Update the mesh's vertices to reflect the modified vertices.
			Mesh.SetVertices (modifiedVertices.ToList ());

			// Update the normals.
			RefreshSurface (normalQuality);

			Mesh.RecalculateBounds ();

			// Reset the modded vertices to their base state so next frames modifications are based on the original vertices.
			ResetVerticesAndNormals ();
		}

		/// <summary>
		/// A safe way to change the modified mesh.
		/// </summary>
		/// <param name="newMesh"></param>
		public void ChangeMesh (Mesh newMesh)
		{
			Filter.mesh = newMesh;
			Mesh = Filter.mesh;
			Mesh.MarkDynamic ();

			baseVertices = (Vector3[])Filter.sharedMesh.vertices.Clone ();
			baseNormals = (Vector3[])Filter.sharedMesh.normals.Clone ();
			ResetVerticesAndNormals ();
		}

		/// <summary>
		/// Recalculates normals.
		/// </summary>
		public void RefreshSurface (NormalsQuality normalsQuality)
		{
			if (normalsQuality == NormalsQuality.None)
				return;

			switch (normalsQuality)
			{
				case NormalsQuality.LowQuality:
					Mesh.RecalculateNormals ();
					break;
				case NormalsQuality.HighQuality:
					Mesh.RecalculateNormals (smoothingAngle);
					break;
			}
		}

		/// <summary>
		/// Updates the list of modifiers, including their order of execution.
		/// </summary>
		public void RefreshModifiers ()
		{
			modifiers = GetComponents<MeshModifierBase> ().ToList ();
		}

		/// <summary>
		/// Reverts any changes made to the vertices and their normals, and updates the base vertices and normals in case custom code has changed the mesh.
		/// </summary>
		private void ResetVerticesAndNormals ()
		{
			modifiedVertices = (Vector3[])baseVertices.Clone ();
			modifiedNormals = (Vector3[])baseNormals.Clone ();
		}

		#endregion



		#region Utility Methods

		/// <summary>
		/// Returns the number of vertices and handles missing references
		/// </summary>
		/// <returns></returns>
		public int GetVertCount ()
		{
			return (Filter.sharedMesh) ? Filter.sharedMesh.vertexCount : 0;
		}

		/// <summary>
		/// Returns the position of a vertex and handles missing references.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Vector3 GetVertPosition (int index)
		{
			return (Filter.sharedMesh) ? transform.rotation * Filter.sharedMesh.vertices[index] : Vector3.zero;
		}

		/// <summary>
		/// Returns the world position of a vertex at the given index and handles missing references.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Vector3 GetVertWorldPosition (int index)
		{
			return transform.position + GetVertPosition (index);
		}

		/// <summary>
		/// Gets the number of vertices being modified each frame.
		/// </summary>
		/// <returns></returns>
		public int GetModifiedVertsPerFrame ()
		{
			return GetVertCount () / modifyFrames;
		}

		/// <summary>
		/// Gets the number of vertices being modified per second.
		/// </summary>
		/// <returns></returns>
		public int GetModifiedVertsPerSecond ()
		{
			return (int)(60f * (GetModifiedVertsPerFrame () * UnityEngine.Time.deltaTime));
		}

		/// <summary>
		/// Returns the current state of the modified vertices.
		/// </summary>
		/// <returns></returns>
		public Vector3[] GetCurrentVerts ()
		{
			return modifiedVertices.Clone () as Vector3[];
		}

		/// <summary>
		/// Returns an array of the vertices' world positions.
		/// </summary>
		/// <returns></returns>
		public Vector3[] GetCurrentWorldVerts ()
		{
			Vector3[] worldVerts = GetCurrentVerts ();

			for (int i = 0; i < worldVerts.Length; i++)
				worldVerts[i] = (Mathx.Vectorx.Multiply (transform.rotation * worldVerts[i], transform.localScale)) + transform.position;

			return worldVerts;
		}

		/// <summary>
		/// Returns an array of modifiers that wants to be used.
		/// </summary>
		/// <returns></returns>
		public int[] GetUseableModifiers ()
		{
			List<int> modIndexes = new List<int> ();

			for (int i = 0; i < modifiers.Count; i++)
			{
				if (modifiers[i] == null)
				{
					modifiers.RemoveAt (i);
					i--;
					continue;
				}
				if (modifiers[i].update)
					modIndexes.Add (i);
			}

			return modIndexes.ToArray ();
		}

		/// <summary>
		/// Returns the bounds of the mesh in its current state.
		/// </summary>
		/// <returns></returns>
		public Bounds GetBounds ()
		{
			return new Bounds (Mesh.bounds.center, Mesh.bounds.size);
		}

		/// <summary>
		/// Returns the bounds of the original mesh.
		/// </summary>
		/// <returns></returns>
		public Bounds GetBaseBounds ()
		{
			return new Bounds (Filter.sharedMesh.bounds.center, Filter.sharedMesh.bounds.size);
		}

		#endregion



		#region Internal Utility Methods

		/// <summary>
		/// Should this object perform modifications to the mesh?
		/// </summary>
		/// <returns></returns>
		private bool CanModify ()
		{
			return (isVisible || updateWhenHidden) && update;
		}

		/// <summary>
		/// Finds the number of vertices that can't be grouped in the chunks.
		/// </summary>
		/// <param name="vertCount"></param>
		/// <param name="chunkSize"></param>
		/// <param name="chunkAmount"></param>
		/// <returns></returns>
		private int CalcVertRemainder (int vertCount, int chunkSize, int chunkAmount)
		{
			return vertCount - (chunkSize * chunkAmount);
		}

		/// <summary>
		/// Invokes the PreMod methods on all of the modifiers.
		/// </summary>
		/// <param name="modIndexes"></param>
		private void InvokePreMods (int[] modIndexes)
		{
			for (int i = 0; i < modIndexes.Length; i++)
				modifiers[modIndexes[i]].PreMod ();
		}

		/// <summary>
		/// Invokes the PostMod methods on all of the modifiers.
		/// </summary>
		/// <param name="modIndexes"></param>
		private void InvokePostMods (int[] modIndexes)
		{
			for (int i = 0; i < modIndexes.Length; i++)
				modifiers[modIndexes[i]].PostMod ();
		}

		/// <summary>
		/// This is where any final modifications will be performed.
		/// </summary>
		/// <param name="currentVertex"></param>
		private void PostModPointOperation (int currentVertex)
		{
			if (!Mathf.Approximately (modifierStrength, 1f))
				modifiedVertices[currentVertex] = Vector3.Lerp (baseVertices[currentVertex], modifiedVertices[currentVertex], modifierStrength);
		}

		#endregion



		#region Coroutines

		/// <summary>
		/// Does ApplyModifiers over x frames to lessen performance impact.
		/// </summary>
		/// <returns></returns>
		private IEnumerator AutoApplyModifiers ()
		{
			while (true)
			{
				if (CanModify ())
				{
					// The current chunk index starts at zero.
					currentModifiedChunkIndex = 0;

					// Store the number of splits in a local variable so that if modifyFrames is changed before the modifications are complete, nothing will get messed up.
					int chunks = modifyFrames;
					// Find the approximate number of vertices in a single split.
					int chunkSize = Mesh.vertexCount / chunks;

					// Increment time based on how much approximate time will have passed when the mods are done.
					Time += UnityEngine.Time.deltaTime * chunks;

					InvokePreMods (GetUseableModifiers ());

					// For each chunk...
					for (int currentChunkIndex = 0; currentChunkIndex < chunks; currentChunkIndex++)
					{
						if (refreshModifiersEveryFrame)
							RefreshModifiers ();

						ModifyChunk (chunkSize * currentChunkIndex, chunkSize * (currentChunkIndex + 1) + ((currentChunkIndex + 1 == chunks) ? CalcVertRemainder (GetVertCount (), chunkSize, chunks) : 0), GetUseableModifiers ());

						// The current chunk's modifications are finished so the next chunk will start being processed.
						currentModifiedChunkIndex++;

						// Wait a frame.
						yield return null;
					}

					ApplyModifications ();
				}
				else
					yield return null;
			}
		}

		#endregion
	}
}