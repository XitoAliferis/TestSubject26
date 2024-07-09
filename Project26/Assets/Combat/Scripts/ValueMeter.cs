using TMPro;
using UnityEngine;

public class ValueMeter : MonoBehaviour {
	public TMP_Text meterText;
	public TMP_Text valueText;

	public char meterCharacter = '▌';
	public int meterLength = 46;

	public float initialValue = 0;

	public float lerpSpeed = 5f;

	public bool blinkDamage;
	public bool zeroIsErr;

	private float currentValue;
	private float targetValue;
	private float originalValue;
	private bool isDoingBlink;

	public float Max { get; set; } 
	public float Value {
		get => targetValue;
		set {
			originalValue = targetValue;
			targetValue = value;
		}
	}

	private string GetMeterText(float f) {
		return new string(meterCharacter, Mathf.Clamp(Mathf.RoundToInt(f * meterLength), 0, meterLength));
	}

	void Start() {
		currentValue = initialValue;
	}
	void Update() {
		currentValue = Mathf.Lerp(currentValue, targetValue, lerpSpeed * Time.deltaTime);
		meterText.text = GetMeterText(currentValue/Max);
		if (valueText != null) {
			valueText.text = Mathf.RoundToInt(currentValue).ToString();
		}
		if (zeroIsErr && targetValue <= 0) {
			valueText.text = "ERR";
		}
	}
}