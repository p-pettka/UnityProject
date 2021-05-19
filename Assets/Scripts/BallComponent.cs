﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    Rigidbody2D m_rigidbody;
    private SpringJoint2D m_connectedJoint;
    private Rigidbody2D m_connectedBody;
    public float SlingStart = 0.5f;
    public float PhysicsSpeed;
    public float MaxSpringDistance = 2.5f;
    private float SlingerArm;
    private LineRenderer m_lineRender;
    private TrailRenderer m_trailRenderer;
    private void OnMouseDrag()
    {
        if (GameplayManager.Instance.Pause) return;
        m_rigidbody.simulated = false;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        Vector2 newBallPos = new Vector3(worldPos.x, worldPos.y);
        float CurJointDistance = Vector3.Distance(newBallPos, m_connectedBody.transform.position);
        if(CurJointDistance > MaxSpringDistance)
        {
            Vector2 direction = (newBallPos - m_connectedBody.position).normalized;
            transform.position = m_connectedBody.position + direction * MaxSpringDistance;
        }else
        {
            transform.position = newBallPos;
        }
        m_lineRender.positionCount = 3;
        Vector2 armPosition = new Vector2(m_connectedBody.position.x + 0.8f, m_connectedBody.position.y);
        m_lineRender.SetPositions(new Vector3[] { m_connectedBody.position, transform.position, armPosition});
    }
    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > m_connectedBody.transform.position.x + SlingStart)
        {
            m_connectedJoint.enabled = false;
            m_lineRender.enabled = false;
            m_trailRenderer.enabled = true;
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

        if (GameplayManager.Instance.Pause)
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
