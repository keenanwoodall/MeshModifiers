using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (TwistModifier))]
public class AnimateTwistModifier : MonoBehaviour
{
	private TwistModifier tm;

	private void Start ()
	{
		tm = GetComponent<TwistModifier> ();
	}
	private void Update ()
	{
		var newTwist = tm.twist;
		newTwist.y = Mathf.Lerp (-10f, 10f, (Mathf.Sin (Time.time * 2f) + 1f) / 2f);
		tm.twist = newTwist;
	}
}