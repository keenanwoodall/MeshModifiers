using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (TransformModifier))]
public class AnimateTransformModifier : MonoBehaviour
{
	private TransformModifier tm;

	private void Start ()
	{
		tm = GetComponent<TransformModifier> ();
	}
	private void Update ()
	{
		var t = (Mathf.Sin (Time.time * 2f) + 1f) / 2f;
		var t2 = (Mathf.Sin (Time.time * 3f) + 1f) / 2f;
		tm.position = Vector3.Lerp (Vector3.down, Vector3.up, t);
		tm.rotation = Quaternion.Lerp (Quaternion.Euler (new Vector3 (0f, -45f, 0f)), Quaternion.Euler (new Vector3 (0f, 45f, 0f)), t2).eulerAngles;
		tm.scale = Vector3.Lerp (new Vector3 (0.5f, 2f, 0.5f), new Vector3 (2f, 0.25f, 2f), t);
	}
}