using UnityEngine;
using System.Collections;

public class ZoomFOV : MonoBehaviour
{
	[Range (0f, 1f)]
	public float t = 0f;

	public float speed = 10f;

	public float
		min = 20f,
		max = 60f;

	void Start ()
	{
		t = Mathf.InverseLerp (max, min, Camera.main.fieldOfView);
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftAlt) && Input.GetMouseButton (0) && !Input.GetMouseButton (1))
			t += Input.GetAxis ("Mouse X") * speed * Time.deltaTime;

		Camera.main.fieldOfView = Mathf.Lerp (max, min, t);
	}
}