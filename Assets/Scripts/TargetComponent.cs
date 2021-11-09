using System.Collections;
using UnityEngine;

public class TargetComponent : InteractiveComponent
{
    private ParticleSystem targetParticle;
    private ParticleSystem explosionParticle;
    private SpriteRenderer m_spriteRender;
    private bool gotHit;
    private int numberOfHits;
    private bool pointAdded;
    public float targetHP;
    public int hitsToDestroy;
    public float currentHP;
    private float m_RGB;
    private bool exploaded;

    IEnumerator DestroyPlank(int seconds)
    {
        if (numberOfHits == hitsToDestroy || currentHP <= 0)
        {
            explosionParticle.Play();
            if (pointAdded == false)
            {
                GameplayManager.Instance.Points += 1;
                pointAdded = true;
            }

            ProgressBarController.Instance.UpdateProgressBar();
            yield return new WaitForSeconds(seconds);
            this.gameObject.SetActive(false);
        }
    }

    public override void DoRestart()
    {
        base.DoRestart();
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        this.m_spriteRender.color = new Color(1, 1, 1, 1);
        currentHP = targetHP;
        numberOfHits = 0;
        pointAdded = false;
        exploaded = false;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.ImpactSound);
            targetParticle.Play();
            GameplayManager.Instance.LifetimeHits += 1;
            currentHP -= GameplayManager.Instance.ballVelocity;
            m_RGB = Mathf.Clamp(currentHP / targetHP, 0.4f, 1.0f);
            this.m_spriteRender.color = new Color(m_RGB, m_RGB, m_RGB, 1);
            explosionParticle.Play();
            StartCoroutine(DestroyPlank(1));
            this.numberOfHits++;
        }

        if (!gotHit)
        {
            AnalyticsManager.Instance.SendEvent("HitTarget");
            gotHit = true;
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
        explosionParticle = GetComponentInChildren<ParticleSystem>();
        m_spriteRender = GetComponent<SpriteRenderer>();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
        pointAdded = false;
        exploaded = false;
        currentHP = targetHP;
    }

    private void FixedUpdate()
    {
        if (currentHP <= 0 && exploaded == false)
        {
            StartCoroutine(DestroyPlank(1));
            exploaded = true;
        }
    }
}
