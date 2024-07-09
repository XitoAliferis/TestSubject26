using System;
using UnityEngine;

public class AnimatedAction {
	public Action<AnimatedAction> onStart;
	public Action onComplete;

	public AnimatedAction(Action<AnimatedAction> onStart) {
		this.onStart = onStart;
	}

	public void Start() {
		onStart?.Invoke(this);
	}

	public void Complete() {
		onComplete?.Invoke();
	}

	public static AnimatedAction Instant() => new(a => a.Complete());

	public static AnimatedAction All(params AnimatedAction[] actions) {
		int count = actions.Length;
		AnimatedAction all = new(a => {
			foreach (var action in actions) {
				action.Start();
			}
		});
		foreach (var action in actions) {
			action.onComplete = () => {
				count--;
				if (count == 0) {
					all.Complete();
				}
			};
		}
		return all;
	}

	private class DelayCounter : MonoBehaviour {
		public AnimatedAction action;
		public float counter;

		void Update() {
			counter -= Time.deltaTime;
			if (counter <= 0) {
				action.Complete();
				Destroy(gameObject);
			}
		}
	}
	public static AnimatedAction Delay(float seconds) {
		AnimatedAction delay = new(a => {
			var timer = new GameObject("Timer").AddComponent<DelayCounter>();
			timer.action = a;
			timer.counter = seconds;
		});
		return delay;
	}
}