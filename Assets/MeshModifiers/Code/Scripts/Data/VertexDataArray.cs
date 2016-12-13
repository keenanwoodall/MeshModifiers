using System.Collections.Generic;
using UnityEngine;

namespace MeshModifiers
{
	[System.Serializable]
	public class VertexDataArray
	{
		#region Public Properties

		public VertexData[] vertices;

		public Mesh mesh;

		#endregion



		#region Private Properties


		#endregion



		#region Constructors

		public VertexDataArray (Mesh mesh)
		{
			ChangeMesh (mesh);
		}

		#endregion



		#region Main Methods

		public void ChangeMesh (Mesh mesh)
		{
			this.mesh = mesh;

			SetAllVertexDataToMesh ();
		}

		public void ResetCurrentData ()
		{
			for (int i = 0; i < vertices.Length; i++)
				vertices[i].ResetCurrentData ();
		}

		public void SetAllVertexDataToMesh ()
		{
			Vector3[] positions = mesh.vertices.Clone () as Vector3[];
			Vector3[] normals = mesh.normals.Clone () as Vector3[];

			vertices = new VertexData[positions.Length];

			for (int i = 0; i < vertices.Length; i++)
				vertices[i] = new VertexData (positions[i], normals[i], positions[i], normals[i]);
		}

		public void SetCurrentVertexDataToMesh ()
		{
			this.vertices = new VertexData[mesh.vertexCount];

			Vector3[] vertices = mesh.vertices;
			Vector3[] normals = mesh.normals;

			for (int i = 0; i < this.vertices.Length; i++)
			{
				this.vertices[i].CurrentPosition = vertices[i];
				this.vertices[i].CurrentNormal = normals[i];
			}
		}

		public void SetMeshToVertexData ()
		{
			mesh.SetVertices (GetCurrentPositions ());
			mesh.SetNormals (GetCurrentNormals ());
		}

		#endregion



		#region Utility Methods

		public List<Vector3> GetCurrentPositions ()
		{
			List<Vector3> currentPositions = new List<Vector3> (vertices.Length);

			for (int i = 0; i < currentPositions.Count; i++)
				currentPositions[i] = vertices[i].CurrentPosition;

			return currentPositions;
		}

		public List<Vector3> GetCurrentNormals ()
		{
			List<Vector3> currentNormals = new List<Vector3> (vertices.Length);

			for (int i = 0; i < currentNormals.Count; i++)
				currentNormals[i] = vertices[i].CurrentNormal;

			return currentNormals;
		}

		public Vector3[] GetCurrentPositionsArray ()
		{
			Vector3[] currentPositions = new Vector3[vertices.Length];

			for (int i = 0; i < currentPositions.Length; i++)
				currentPositions[i] = vertices[i].CurrentPosition;

			return currentPositions;
		}

		public Vector3[] GetCurrentNormalsArray ()
		{
			Vector3[] currentNormals = new Vector3[vertices.Length];

			for (int i = 0; i < currentNormals.Length; i++)
				currentNormals[i] = vertices[i].CurrentPosition;

			return currentNormals;
		}

		#endregion
	}
}