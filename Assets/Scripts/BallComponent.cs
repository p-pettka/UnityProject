using UnityEngine;

public class BallComponent : InteractiveComponent

{
    private SpringJoint2D m_connectedJoint;
    private Rigidbody2D m_connectedBody;
    private Animator m_animator;
    private ParticleSystem m_particles;
    public float SlingStart = 0.5f;
    public float PhysicsSpeed;
    public float MaxSpringDistance = 2.5f;
    public Sprite[] ballSprites;
    private LineRenderer m_lineRender;
    private TrailRenderer m_trailRenderer;
    private bool m_hitTheGround = false;
    private bool shooted = false;
    private bool missedTarget;
    private Vector2 m_currentVelocity;
    private float timer;

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
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.ImpactSound);
            m_animator.enabled = true;
            m_animator.Play(0);

            if (missedTarget)
            {
                AnalyticsManager.Instance.SendEvent("MissedTarget");
                missedTarget = false;
            }
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            //GameplayManager.Instance.ballVelocity = m_rigidbody.velocity.magnitude;
            missedTarget = false;
            timer = 0.0f;
        }
    }

    public override void DoRestart()
    {
        m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
        base.DoRestart();

        m_connectedJoint.enabled = true;
        m_lineRender.enabled = true;
        m_trailRenderer.enabled = false;

        shooted = false;
        SetLineRenderPoints();
        m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.RestartSound);
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.freezeRotation = true;
    }

    public override void DoPause()
    {
        m_currentVelocity = m_rigidbody.velocity;
        base.DoPause();
        Debug.Log(m_currentVelocity);
    }

    public override void DoPlay()
    {
        base.DoPlay();
        m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
        m_rigidbody.velocity = m_currentVelocity;
    }

    public bool IsSimulated()
    {
        return m_rigidbody.simulated;
    }

    private void OnDestroy()
    {
        GameplayManager.OnGamePaused -= DoPause;
        GameplayManager.OnGamePlaying -= DoPlay;
    }

    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;
        var main = m_particles.main;
        main.startSpeed = 5f;

        if (!shooted)
        {
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.ShootSound);
            m_particles.Play();
            timer = 0.0f;
        }
    }

    private void OnMouseDown()
    {
        if (!shooted)
        {
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.PullSound);
            m_particles.Play();
            Physics2D.IgnoreLayerCollision(8, 10, true);
        }

        var main = m_particles.main;
        main.startSpeed = -5f;
        m_rigidbody.freezeRotation = false;
    }

    public float get
    {
        get { return m_rigidbody.velocity.magnitude; }
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
        GetComponent<SpriteRenderer>().sprite = ballSprites[AnalyticsManager.Instance.GetIntParameter("SpriteIndex")];
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
        m_rigidbody.velocity = Vector3.zero;
        GameplayManager.OnGamePaused += DoPause;
        GameplayManager.OnGamePlaying += DoPlay;
        transform.position = new Vector3((-0.6f), -0.9280946f, 0);
        missedTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_trailRenderer.enabled = !m_hitTheGround;
        timer += Time.deltaTime;

        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
        {
            m_connectedJoint.enabled = false;
            m_lineRender.enabled = false;
            m_trailRenderer.enabled = true;
            Physics2D.IgnoreLayerCollision(8, 10, false);
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
        }
        else
        {
            m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void FixedUpdate()
    {
        PhysicsSpeed = m_rigidbody.velocity.magnitude;
        GameplayManager.Instance.ballVelocity = PhysicsSpeed;

        if (timer > 4.0f && shooted == true && GameplayManager.Instance.GameState == EGameState.Playing
            || PhysicsSpeed < 0.05f && shooted == true && GameplayManager.Instance.GameState == EGameState.Playing)
        {
            GameplayManager.Instance.BallRestart();
            GameplayManager.Instance.Balls--;
        }

        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart + 2.0f)
        {
            shooted = true;
        }
    }
}