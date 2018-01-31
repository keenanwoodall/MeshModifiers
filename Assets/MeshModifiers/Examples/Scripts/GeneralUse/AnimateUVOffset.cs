using UnityEngine;

[RequireComponent (typeof (Renderer))]
public class AnimateUVOffset : MonoBehaviour
{
	public Vector2 offset = Vector2.right;
	private Renderer renderer;

	private void Awake ()
	{
		renderer = GetComponent<Renderer> ();
	}
	private void Update ()
	{
		renderer.material.mainTextureOffset += offset * Time.deltaTime;
	}
}