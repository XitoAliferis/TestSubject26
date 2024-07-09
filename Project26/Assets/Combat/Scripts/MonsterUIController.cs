using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterUIController : MonoBehaviour
{
	public TMP_Text targetNumberText;
	public TMP_Text targetLabelText;

	public int index;

	public float blinkTime;
	[Range(0f, 1f)]
	public float blinkDutyCycle = 0.5f;

	public bool Targeted { get; set; }


	private static readonly Dictionary<int, string> indexIndicators = new Dictionary<int, string>() {
		{ 0, "A  " },
		{ 1, " B " },
		{ 2, "  C" },
	};

	void Update() {
		if (Targeted) {
			bool blink = (Time.time % blinkTime) < blinkTime * blinkDutyCycle;
			targetNumberText.text = ((blink) ? "[" : " ") + indexIndicators[index] + ((blink) ? "]" : " ");
			targetLabelText.text = "TARGET";
		} else {
			targetNumberText.text = "";
			targetLabelText.text = "";
		}
	}
}
