using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InputRequirement
{
	MouseUp,
	MouseDown,
	None
}

public class LerpPositionFromMouse : MonoBehaviour
{
	public float
		speed = 3f,
		xOffset = 0.1f,
		yOffset = 0.1f;

	public InputRequirement requires = InputRequirement.MouseUp;


	private Vector3 
		basePosition,
		offsetPosition;


	void Start ()
	{
		basePosition = transform.position;
		offsetPosition = basePosition;
	}

	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, offsetPosition, speed * Time.deltaTime);

		if (requires == InputRequirement.MouseDown && (!Input.GetMouseButton (0) || !Input.GetMouseButton (1)))
			return;
		else if (requires == InputRequirement.MouseUp && (Input.GetMouseButton (0) || Input.GetMouseButton (1)))
			return;

		Vector3 mousePosition = Input.mousePosition;

		float xt = Mathf.InverseLerp (0, Screen.width, mousePosition.x);
		float yt = Mathf.InverseLerp (0, Screen.height, mousePosition.y);

		offsetPosition = new Vector3 (Mathf.Lerp (-xOffset, xOffset, xt), Mathf.Lerp (-yOffset, yOffset, yt), 0f) + basePosition;

	}
}