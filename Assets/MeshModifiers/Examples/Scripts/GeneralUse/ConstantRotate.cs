using UnityEngine;
using System.Collections;

public class ConstantRotate : MonoBehaviour
{
	#region Public Properties

	public float globalSpeed = 1f;
	public Vector3 axialSpeed = Vector3.up;

	#endregion



	#region Unity Methods

	void Update ()
	{
		transform.Rotate (axialSpeed * globalSpeed * Time.deltaTime);
	}

	#endregion
}