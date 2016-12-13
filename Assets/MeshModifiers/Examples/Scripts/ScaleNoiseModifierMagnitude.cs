using System.Collections;
using UnityEngine;
using MeshModifiers;

public class ScaleNoiseModifierMagnitude : MonoBehaviour
{
	public float strength = 1f;
	public NoiseModifierBase[] noiseModifiers;

	void Update ()
	{
		if (Input.GetMouseButton(1))
		{
			float scaleAmount = Input.GetAxis ("Mouse X") * Time.deltaTime * strength;
			for (int i = 0; i < noiseModifiers.Length; i++)
			{
				noiseModifiers[i].magnitudeMultiplier += scaleAmount;
			}
		}
	}
}