using UnityEngine;

public class LerpCamera : MonoBehaviour {
	public Transform target;
	public float speed = 1.0f; //Exponential decay seconds or somethin.

	void Update() {
		transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
		transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * speed);
	}
}