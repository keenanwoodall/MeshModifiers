using UnityEngine;

namespace MeshModifiers
{
	[System.Serializable]
	public struct VertexData
	{
		#region Public Properties

		public Vector3 CurrentPosition;
		public Vector3 CurrentNormal;

		public Vector3 UnModifiedPosition { get; private set; }
		public Vector3 UnModifiedNormal { get; private set; }

		#endregion



		#region Constructors

		public VertexData (Vector3 currentPosition, Vector3 currentNormal, Vector3 unModifiedPosition, Vector3 unModifiedNormal)
		{
			CurrentPosition = currentPosition;
			CurrentNormal = currentNormal;
			UnModifiedPosition = unModifiedPosition;
			UnModifiedNormal = unModifiedNormal;
		}

		#endregion



		#region Utility Methods

		public void SetCurrentData (Vector3 currentPosition, Vector3 currentNormal)
		{
			CurrentPosition = currentPosition;
			CurrentNormal = currentNormal;
		}

		public void ResetCurrentData ()
		{
			SetCurrentData (UnModifiedPosition, UnModifiedNormal);
		}

		#endregion
	}
}