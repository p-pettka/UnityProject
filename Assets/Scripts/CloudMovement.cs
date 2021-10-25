using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private float backgroundPosition;
    private float startPosition;
    private float length;
    public float movementEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        backgroundPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(backgroundPosition - movementEffect, transform.position.y, transform.position.z);
        backgroundPosition = transform.position.x;

        if (backgroundPosition < startPosition - length)
        {
            backgroundPosition += startPosition + length;
        }
    }
}
