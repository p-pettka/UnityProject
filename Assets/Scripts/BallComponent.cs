using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    public float ScaleSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale != new Vector3(3, 3, 3))
        {
            transform.localScale += Vector3.one * ScaleSpeed;
        }
    }
}
