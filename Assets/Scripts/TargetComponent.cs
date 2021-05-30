using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    private AudioSource targetAudioSource;
    private ParticleSystem targetParticle;
    public AudioClip ImpactSound;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            targetAudioSource.PlayOneShot(ImpactSound);
            targetParticle.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        targetAudioSource = GetComponent<AudioSource>();
        targetParticle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
