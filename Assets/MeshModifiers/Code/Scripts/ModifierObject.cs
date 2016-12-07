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

		[Range (0f, 1f)]
		public float modifierStrength = 1f;

		[Header ("Surface")]
		[Tooltip ("When set to Low Quality, the smooothing angle is not taken into account. High Quality is very expensive and shouldn't be used every frame, if possible. You can call RefreshSurfaces manually to calculate High Quality normals at the time of your choosing.")]
		public NormalsType recalcNormals = NormalsType.LowQuality;
		[Range (0f, 180f), Tooltip ("The smoothing angle is only used when calculation is high quality. The mesh can never be less smooth than the imported mesh.")]
		public float smoothingAngle = 60f;

		[Header ("Performance")]
		[Tooltip ("Only performs modifications when this object is being rendered by the camera.")]
		public bool updateWhenHidden = false;

		[Range (1, MeshModifierConstants.MAX_MOD_FRAMES), Tooltip ("Performs the modification over this number of frames.")]
		public int modifyFrames = 1;

		[System.NonSerialized]
		public List<MeshModifierBase> modifiers = new List<MeshModifierBase> ();

		/// <summary>
		/// Local to the modifier object. Used to sync modifications that occur over multiple frames and require a time value.
		/// </summary>
		public float Time { get; private set; }

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



		#region private Properties

		private bool isVisible = false;

		private bool
			refreshModifiersEveryFrame = true,
			modifyingChunk = false;

		protected Vector3[] baseVertices;
		protected Vector3[] modifiedVertices;
		protected Vector3[] baseNormals;
		protected Vector3[] modifiedNormals;

		#endregion



		#region Unity Methods

		void Awake ()
		{
			Filter = GetComponent<MeshFilter> ();
			Mesh = Filter.mesh;
			Mesh.MarkDynamic ();

			baseVertices = (Vector3[])Mesh.vertices.Clone ();
			modifiedVertices = (Vector3[])Mesh.vertices.Clone ();

			baseNormals = (Vector3[])Mesh.normals.Clone ();
			modifiedNormals = (Vector3[])Mesh.normals.Clone ();
		}

		void Start ()
		{
			RefreshModifiers ();
			StartCoroutine (ApplyModifiers ());
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



		#region Utility Methods

		/// <summary>
		/// Reverts any changes made to the vertices and their normals.
		/// </summary>
		public void ResetModdedVerticesandNormals ()
		{
			modifiedVertices = (Vector3[])baseVertices.Clone ();
			modifiedNormals = (Vector3[])baseNormals.Clone ();
		}

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
		/// Recalculates normals.
		/// </summary>
		public void RefreshSurface (NormalsType normalsQuality)
		{
			if (normalsQuality == NormalsType.None)
				return;

			switch (normalsQuality)
			{
				case NormalsType.LowQuality:
					Mesh.RecalculateNormals ();
					break;
				case NormalsType.HighQuality:
					Mesh.RecalculateNormals (smoothingAngle);
					break;
			}
		}

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
		public int[] FindUseableModifiers ()
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
		/// Updates the list of modifiers, including their order of execution.
		/// </summary>
		public void RefreshModifiers ()
		{
			modifiers = GetComponents<MeshModifierBase> ().ToList ();
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
		private IEnumerator ApplyModifiers ()
		{
			while (true)
			{
				if (CanModify ()/* && modifyFrames > 1*/)
				{
					// Store the number of splits in a local variable so that if modifyFrames is changed before the modifications are complete, nothing will get messed up.
					int chunks = modifyFrames;
					// Find the approximate number of vertices in a single split.
					int chunkSize = Mesh.vertexCount / chunks;

					// Increment time based on how much approximate time will have passed when the mods are done.
					Time += UnityEngine.Time.deltaTime * chunks;

					int[] useableModifierIndexes = FindUseableModifiers ();
					InvokePreMods (useableModifierIndexes);

					// For each split...
					for (int currentChunkIndex = 0; currentChunkIndex < chunks; currentChunkIndex++)
					{
						if (refreshModifiersEveryFrame)
							RefreshModifiers ();

						// A chunk of the mesh is about to be modified, so modifyingChunk must be true.
						modifyingChunk = true;

						// For each vertice in the current split... If the current split is the last split, find the remaining vertices (in case of a vertex count that can't be split perfectly) and add them to the vertices to loop through.
						for (int currentVert = chunkSize * currentChunkIndex; currentVert < chunkSize * (currentChunkIndex + 1) + ((currentChunkIndex + 1 == chunks) ? CalcVertRemainder (GetVertCount (), chunkSize, chunks) : 0); currentVert++)
						{
							// Make sure we won't try to access any modifiers that have been deleted since the last chunk was modded.
							useableModifierIndexes = FindUseableModifiers ();

							// For each modifier...
							for (int currentMod = 0; currentMod < useableModifierIndexes.Length; currentMod++)
								modifiedVertices[currentVert] = modifiers[useableModifierIndexes[currentMod]].ModifyOffset (modifiedVertices[currentVert], modifiedNormals[currentVert]);

							PostModPointOperation (currentVert);
						}

						// The current chunk's modifications are finished so modifyingChunk as false.
						modifyingChunk = false;

						// Wait a frame.
						yield return null;
					}

					// Update the mesh's vertices to reflect the modified vertices.
					Mesh.SetVertices (modifiedVertices.ToList ());

					// Reset the modded vertices to their base state so next frames modifications are based on the original vertices.
					ResetModdedVerticesandNormals ();

					RefreshSurface (recalcNormals);
					Mesh.RecalculateBounds ();
				}
				else
					yield return null;
			}
		}

		#endregion
	}
}