using UnityEngine;
using System.Collections.Generic;

namespace MeshModifiers
{
	public class SamplesContainer
	{
		private int sampleSize = 10;
		public int SampleSize
		{
			get { return sampleSize; }
			set
			{
				if (value < 1)
					sampleSize = 1;
				else
					sampleSize = value;
			}
		}

		public int CurrentSampleSize { get; private set; }

		private List<double> samples = new List<double> ();

		public SamplesContainer (int sampleSize)
		{
			SampleSize = sampleSize;
		}

		public void AddSample (double value)
		{
			samples.Add (value);
			samples.Sort ();

			CurrentSampleSize = samples.Count;
			if (CurrentSampleSize > SampleSize)
			{
				samples.RemoveAt (0);
				samples.RemoveAt (samples.Count - 1);
			}
		}

		public double GetConsistentSample ()
		{
			CurrentSampleSize = samples.Count;
			if (CurrentSampleSize > 2)
			{
				int medianIndex = CurrentSampleSize / 2;
				return (samples[medianIndex - 1] + samples[medianIndex] + samples[medianIndex + 1]) / 3;
			}
			else if (CurrentSampleSize == 2)
				return (samples[0] + samples[1]) / 2;
			else
				return samples[0];
		}

		public double GetAverageSample ()
		{
			double sampleSum = 0;

			for (int i = 0; i < CurrentSampleSize; i++)
				sampleSum += samples[i];

			return sampleSum / CurrentSampleSize;
		}

		public double GetRange ()
		{
			return Mathf.Abs ((int)(samples[CurrentSampleSize] - samples[0]));
		}
	}
}