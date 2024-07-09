using UnityEngine;

public class GenericShakeAnimation : MonoBehaviour {
	public Transform target;
	public Vector3 axis;
	public float strength;
	public float speed;
	public float timer;

	public AnimatedAction action;

	private Vector3 originalLocation;
	private float originalTime;

	void Start(){
		originalLocation = target.position;
		originalTime = timer;
	}

	void Update() {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			timer = 0;
			target.position = originalLocation;
			action.Complete();
			Destroy(gameObject);
			return;
		}
		target.position = originalLocation + axis * strength * (timer / originalTime) * Mathf.Sin(timer * speed);
	}

	public static AnimatedAction Create(Transform target, Vector3 axis, float strength, float speed, float time) {
		return new(a => {
			GameObject shaker = new GameObject("Shaker");
			var shakeAnimation = shaker.AddComponent<GenericShakeAnimation>();
			shakeAnimation.target = target;
			shakeAnimation.axis = axis;
			shakeAnimation.strength = strength;
			shakeAnimation.speed = speed;
			shakeAnimation.timer = time;
			shakeAnimation.action = a;
		});
	}
}