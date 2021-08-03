using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kodilla.Module8.Scripts;

public class FadeRemoteControl : MonoBehaviour
{
    public FadingImage fading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.Space))
        {
            fading.DoFade();
        }   
    }
}
