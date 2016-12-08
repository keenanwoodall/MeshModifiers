using UnityEngine;
using MeshModifiers;

[RequireComponent(typeof (ModifierObject))]
public class ChangeMeshAtRuntimeExample : MonoBehaviour
{
	[SerializeField]
	private Mesh newMesh;

	private ModifierObject mo;


	void Start ()
	{
		mo = GetComponent<ModifierObject> ();

		Invoke ("ChangeMesh", 3f);
	}

	public void ChangeMesh ()
	{
		mo.ChangeMesh (newMesh);
	}
}