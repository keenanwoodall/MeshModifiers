using System.Collections;
using UnityEngine;
using MeshModifiers;

public class ScaleNoiseModifierSampleSize : MonoBehaviour
{
	public float strength = 1f;
	public NoiseModifierBase[] noiseModifiers;

	void Update ()
	{
		if (Input.GetMouseButton (1))
		{
			float scaleAmount = Input.GetAxis ("Mouse Y") * Time.deltaTime * strength;
			for (int i = 0; i < noiseModifiers.Length; i++)
			{
				noiseModifiers[i].sampleScale += scaleAmount;
			}
		}
	}
}