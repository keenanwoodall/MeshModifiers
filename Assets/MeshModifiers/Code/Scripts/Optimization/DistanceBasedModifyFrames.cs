using System;
using UnityEngine;
using MeshModifiers;

[RequireComponent (typeof (ModifierObject))]
public class DistanceBasedModifyFrames : MonoBehaviour
{
	#region Public Properties

	public Transform target;

	[Range (1, 10)]
	public int
		minFrames = 2,
		maxFrames = 10;

	public float
		minFramesDistance = 0.1f,
		maxFramesDistance = 50f;


	[NonSerialized]
	public ModifierObject modObject;

	#endregion



	#region Unity Methods

	void Start ()
	{
		if (modObject == null)
			modObject = GetComponent<ModifierObject> ();
	}

	void Update ()
	{
		if (target == null || modObject == null || !modObject.update)
			return;

		float distance = Vector3.Distance (modObject.transform.position, target.position);

		modObject.modifyFrames = (int)Mathf.Lerp (minFrames, maxFrames, Mathf.InverseLerp (minFramesDistance, maxFramesDistance, distance));
	}

	#endregion
}