﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private int fps;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        fps = (int)(1 / Time.deltaTime);
        Debug.Log("FPS: " + fps);
    }
}
