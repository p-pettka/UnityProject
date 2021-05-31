using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : InteractiveComponent, IRestartableObject
{
    private AudioSource targetAudioSource;
    private ParticleSystem targetParticle;
    private Rigidbody2D m_rigidbody;
    private Vector3 m_startPosition;
    private Quaternion m_startRotation;
    public AudioClip ImpactSound;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            targetAudioSource.PlayOneShot(ImpactSound);
            targetParticle.Play();
            GameplayManager.Instance.Points += 1;
        }
    }

    public void DoRestart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.simulated = true;

    }

    private void DoPlay()
    {
        m_rigidbody.simulated = true;
    }

    private void DoPause()
    {
        m_rigidbody.simulated = false;
    }

    private void OnDestroy()
    {
        GameplayManager.OnGamePlaying -= DoPlay;
        GameplayManager.OnGamePaused -= DoPause;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetAudioSource = GetComponent<AudioSource>();
        targetParticle = GetComponent<ParticleSystem>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            DoRestart();
    }
}
