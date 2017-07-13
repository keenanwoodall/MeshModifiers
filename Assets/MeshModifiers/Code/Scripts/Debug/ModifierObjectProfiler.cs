using System.Diagnostics;
using UnityEngine;

namespace MeshModifiers
{
	[RequireComponent (typeof (ModifierObject))]
	[AddComponentMenu (MeshModifierConstants.ADD_COMP_DEBUG_NAME + "Profiler")]
	public class ModifierObjectProfiler : MonoBehaviour
	{
		private const int
			MIN_SAMPLE_SIZE = 10,
			MAX_SAMPLE_SIZE = 100;

		private const float PROCESSED_EXECUTION_TIME_UPDATE_DELAY = 0.5f;


		[Range (MIN_SAMPLE_SIZE, MAX_SAMPLE_SIZE)]
		public int sampleSize = MIN_SAMPLE_SIZE;

		public double ExecutionTime { get; private set; }
		public double ConsistentExecutionTime { get; private set; }
		public double AverageExecutionTime { get; private set; }

		private ModifierObject modifierObject;
		private Stopwatch modifierStopWatch = new Stopwatch ();

		private float updateTimer = 0f;
		[SerializeField]
		private SamplesContainer modifierTimeSampler = new SamplesContainer (MIN_SAMPLE_SIZE);


		private void Start ()
		{
			modifierObject = GetComponent<ModifierObject> ();

			modifierObject.OnAutoUpdateStart.AddListener (() => OnModsStart ());
			modifierObject.OnAutoUpdateFinish.AddListener (() => OnModsFinish ());
		}

		private void OnModsStart ()
		{
			modifierStopWatch = Stopwatch.StartNew ();
		}

		private void OnModsFinish ()
		{
			modifierStopWatch.Stop ();
			ExecutionTime = modifierStopWatch.ElapsedMilliseconds;

			modifierTimeSampler.SampleSize = sampleSize;
			modifierTimeSampler.AddSample (ExecutionTime);

			updateTimer += Time.deltaTime;
			if (updateTimer > PROCESSED_EXECUTION_TIME_UPDATE_DELAY)
			{
				ConsistentExecutionTime = modifierTimeSampler.GetConsistentSample ();
				AverageExecutionTime = modifierTimeSampler.GetAverageSample ();
				updateTimer = 0f;
			}
		}
	}
}