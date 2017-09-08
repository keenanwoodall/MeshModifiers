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
		var newTwist = tm.twistAmount;
		newTwist = Mathf.Lerp (-10f, 10f, (Mathf.Sin (Time.time * 2f) + 1f) / 2f);
		tm.twistAmount = newTwist;
	}
}