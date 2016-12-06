using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Light))]
public class FlickerLight : MonoBehaviour
{
	#region Public Properties

	public float 
		variance = 0.1f,
		speed = 1f;

	#endregion



	#region Private Properties

	private Light l;

	private float 
		baseIntensity,
		randomOffset;

	#endregion



	#region Unity Methods

	void Start ()
	{
		l = GetComponent<Light> ();
		baseIntensity = l.intensity;

		randomOffset = Random.Range (0f, 10f);
	}

	void Update ()
	{
		float samplePosition = randomOffset + Time.time * speed;
		l.intensity = baseIntensity + 1 - (Mathf.PerlinNoise (samplePosition, samplePosition) / 2f) * variance;
	}

	#endregion
}