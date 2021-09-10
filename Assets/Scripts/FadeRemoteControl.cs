using UnityEngine;
using Kodilla.Module8.Scripts;

public class FadeRemoteControl : MonoBehaviour
{
    public FadingImage fading;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fading.DoFade();
        }   
    }
}
