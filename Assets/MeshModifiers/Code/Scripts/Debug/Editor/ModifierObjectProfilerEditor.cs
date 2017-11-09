using UnityEngine;
using UnityEditor;

namespace MeshModifiers
{
	[CustomEditor (typeof (ModifierObjectProfiler))]
	public class ModifierObjectProfilerEditor : Editor
	{
		private ModifierObjectProfiler current;

		private string
			consistentTime = "?",
			averageTime = "?";

		public override void OnInspectorGUI ()
		{
			if (current == null || current != target)
				current = target as ModifierObjectProfiler;

			if (Application.isPlaying)
			{
				consistentTime = (current.ConsistentExecutionTime).ToString ("n2");
				averageTime = (current.AverageExecutionTime).ToString ("n2");
			}
			else
			{
				EditorGUILayout.HelpBox ("Enter play mode to update profiler.", MessageType.Info);
				consistentTime = averageTime = "?";
			}

			base.OnInspectorGUI ();

			GUILayout.Label ("\bExcecution Time (ms)");
			GUILayout.Label ("\tConsistent Time ~ " + consistentTime);
			GUILayout.Label ("\tAverage Time ~ " + averageTime);
		}
	}
}