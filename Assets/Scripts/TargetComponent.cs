using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : InteractiveComponent
{
    private ParticleSystem targetParticle;

    public override void DoRestart()
    {
        base.DoRestart();

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.ImpactSound);
            targetParticle.Play();
            GameplayManager.Instance.LifetimeHits += 1;
            GameplayManager.Instance.Points += 1;
            //GameObject.Destroy(this.gameObject, 0.5f);
        }
    }

    private void OnDestroy()
    {
        GameplayManager.OnGamePlaying -= DoPlay;
        GameplayManager.OnGamePaused -= DoPause;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        targetParticle = GetComponent<ParticleSystem>();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
