using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : InteractiveComponent, IRestartableObject

{
    Rigidbody2D m_rigidbody;
    private SpringJoint2D m_connectedJoint;
    private Rigidbody2D m_connectedBody;
    private Vector3 m_startPosition;
    private Quaternion m_startRotation;
    private AudioSource m_audioSource;
    private Animator m_animator;
    private ParticleSystem m_particles;
    public AudioClip PullSound;
    public AudioClip ShootSound;
    public AudioClip ImpactSound;
    public AudioClip RestartSound;
    public float SlingStart = 0.5f;
    public float PhysicsSpeed;
    public float MaxSpringDistance = 2.5f;
    private float SlingerArm;
    private LineRenderer m_lineRender;
    private TrailRenderer m_trailRenderer;
    private bool m_hitTheGround = false;
    private bool shooted = false;

    private void SetLineRenderPoints()
    {
        m_lineRender.positionCount = 3;
        Vector2 armPosition = new Vector2(m_connectedBody.position.x + 0.8f, m_connectedBody.position.y);
        m_lineRender.SetPositions(new Vector3[] { m_connectedBody.position, transform.position, armPosition });
    }

    private void OnMouseDrag()
    {
        if (GameplayManager.Instance.GameState == EGameState.Paused) return;
        if (!shooted)
        {
            m_rigidbody.simulated = false;
            m_hitTheGround = false;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPos.x, worldPos.y, 0);
            Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);
            float CurJointDistance = Vector3.Distance(newBallPos, m_connectedBody.transform.position);
            if (CurJointDistance > MaxSpringDistance)
            {
                Vector2 direction = (newBallPos - m_connectedBody.position).normalized;
                transform.position = m_connectedBody.position + direction * MaxSpringDistance;
            }
            else
            {
                transform.position = newBallPos;
            }
            SetLineRenderPoints();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_hitTheGround = true;
            m_audioSource.PlayOneShot(ImpactSound);
            m_animator.enabled = true;
            m_animator.Play(0);
        }
    }

    private void DoPlay()
    {
        m_rigidbody.simulated = true;
    }

    private void DoPause()
    {
        m_rigidbody.simulated = false;
    }

    public void DoRestart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = 0.0f;
        m_rigidbody.simulated = true;

        m_connectedJoint.enabled = true;
        m_lineRender.enabled = true;
        m_trailRenderer.enabled = false;

        shooted = false;
        SetLineRenderPoints();
        m_audioSource.PlayOneShot(RestartSound);

    }

    private void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }

    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;
        m_particles.startSpeed = 5f;
        m_particles.Play();
        if(!shooted)m_audioSource.PlayOneShot(ShootSound);
    }

    private void OnMouseDown()
    {
        if (!shooted)m_audioSource.PlayOneShot(PullSound);
        m_particles.startSpeed = -5f;
        m_particles.Play();
    }

    public bool IsSimulated()
    {
        return m_rigidbody.simulated;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_connectedJoint = GetComponent<SpringJoint2D>();
        m_connectedBody = m_connectedJoint.connectedBody;
        m_lineRender = GetComponent<LineRenderer>();
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_audioSource = GetComponent<AudioSource>();
        m_animator = GetComponentInChildren<Animator>();
        m_particles = GetComponentInChildren<ParticleSystem>();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;

    }

    // Update is called once per frame
    void Update()
    {
        m_trailRenderer.enabled = !m_hitTheGround;
        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
        {
            m_connectedJoint.enabled = false;
            m_lineRender.enabled = false;
            m_trailRenderer.enabled = true;
            shooted = true;
        }
        if (transform.position.x < m_connectedBody.transform.position.x + SlingStart)
        {
            m_trailRenderer.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
                transform.position += new Vector3(0, 1, 0);

        if (Input.GetKeyDown(KeyCode.DownArrow))
                transform.position -= new Vector3(0, 1, 0);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.position -= new Vector3(1, 0, 0);

        if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.position += new Vector3(1, 0, 0);

        if (GameplayManager.Instance.GameState == EGameState.Paused)
        {
            m_rigidbody.bodyType = RigidbodyType2D.Static;
        }else
        {
            m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

    }
    private void FixedUpdate()
    {
        PhysicsSpeed = m_rigidbody.velocity.magnitude;
    }
}
