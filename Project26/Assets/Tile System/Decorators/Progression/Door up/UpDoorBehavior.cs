using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDoorBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            GameManager.Instance.SwitchToNextFloor();
        }
    }
}
