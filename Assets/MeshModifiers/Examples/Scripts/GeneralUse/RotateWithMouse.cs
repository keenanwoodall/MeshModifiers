using UnityEngine;
using System.Collections;

public class RotateWithMouse : MonoBehaviour
{
	public float
		horizontalSpeed = 5f,
		verticalSpeed = 2.5f,
		deaccelerationTime = 0.1f,
		maxSpeed = 2f;

	private Vector2 velocity = new Vector2 ();
	private Vector2 refVelocity = new Vector2 ();

	void Update ()
	{
		if (Input.GetMouseButton (0) && !Input.GetKey (KeyCode.LeftAlt))
		{
			velocity.x += -Input.GetAxis ("Mouse X") * horizontalSpeed * Time.deltaTime;
			velocity.y += Input.GetAxis ("Mouse Y") * verticalSpeed * Time.deltaTime;
		}

		velocity = Vector2.SmoothDamp (velocity, Vector2.zero, ref refVelocity, deaccelerationTime, (horizontalSpeed + verticalSpeed) / 2f, Time.deltaTime);

		transform.RotateAround (Vector3.zero, Vector3.up, velocity.x);
		transform.RotateAround (Vector3.zero, Vector3.right, velocity.y);
	}
}