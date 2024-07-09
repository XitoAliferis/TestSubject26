using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public Transform playerTransform;
    public float visibilityRange = 10f; // The range within which the fog appears

    private Renderer fogRenderer;

    // Start is called before the first frame update
    void Start()
    {
        fogRenderer = GetComponent<Renderer>();

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }

        // Initially set the fog to be invisible
        if (fogRenderer != null)
        {
            fogRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null && fogRenderer != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            fogRenderer.enabled = distance <= visibilityRange;
        }
    }
}
