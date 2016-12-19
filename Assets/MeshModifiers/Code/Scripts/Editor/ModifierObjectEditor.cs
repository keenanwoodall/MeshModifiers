using UnityEngine;
using UnityEditor;
using MeshModifiers;

[CustomEditor (typeof (ModifierObject)), CanEditMultipleObjects]
public class ModifierObjectEditor : Editor
{
	private readonly string DEFAULT_PATH = "Assets/";

	private ModifierObject current;

	int delayedVPS;
	float vpsRefreshCounter = 0;
	float vpsRefreshDelay = 1f;
	float
		lastTime, 
		deltaTime;

	public override void OnInspectorGUI ()
	{
		if (current == null || current != target)
			current = target as ModifierObject;

		base.OnInspectorGUI ();

		deltaTime = (float)EditorApplication.timeSinceStartup - lastTime;

		EditorGUILayout.Space ();
		DrawGUI ();


		lastTime = (float)EditorApplication.timeSinceStartup;

		if (Application.isPlaying)
			Repaint ();
	}

	public void DrawGUI ()
	{
		EditorGUILayout.LabelField ("\tTotal Verts - " + current.GetVertCount ());
		EditorGUILayout.LabelField ("\tVerts Modded/frame - " + current.GetModifiedVertsPerFrame ());

		vpsRefreshCounter += deltaTime;
		if (Application.isPlaying)
		{
			if (vpsRefreshCounter > vpsRefreshDelay)
			{
				delayedVPS = current.GetModifiedVertsPerSecond ();
				vpsRefreshCounter = 0f;
			}

			EditorGUILayout.LabelField ("\tVerts Modded/second ~ " + delayedVPS);

			if (GUILayout.Button ("Save Mesh"))
			{
				Mesh tempMesh = (Mesh)Object.Instantiate (current.Mesh);
				AssetDatabase.CreateAsset (tempMesh, AssetDatabase.GenerateUniqueAssetPath (DEFAULT_PATH + current.name + ".asset"));
			}

			if (!current.autoUpdate)
			{
				if (GUILayout.Button ("Apply Modifiers"))
				{
					current.ModifyAll (invokePreMods: true, invokePostMods: true);
					current.ApplyModifications ();
				}
			}

			if (current.normalQuality == NormalsQuality.None)
			{
				if (GUILayout.Button ("Recalculate Normals"))
					current.RefreshSurface (NormalsQuality.HighQuality);
			}
		}
	}
}