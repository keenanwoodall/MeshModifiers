using UnityEngine;
using System.Collections;

public class PulseScale : MonoBehaviour
{
	#region Public Properties

	public float
		variance = 0.1f,
		speed = 1f;

	#endregion



	#region Private Properties

	private Vector3 baseScale;
	private float randomOffset;

	#endregion



	#region Unity Methods

	void Start ()
	{
		baseScale = transform.localScale;

		randomOffset = Random.Range (0f, 10f);
	}

	void Update ()
	{
		float samplePosition = randomOffset + Time.time * speed;
		transform.localScale = baseScale + Vector3.one * ((Mathf.PerlinNoise (samplePosition, samplePosition) / 2f) * variance);
	}

	#endregion
}