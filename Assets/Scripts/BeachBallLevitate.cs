using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBallLevitate : MonoBehaviour
{
    private Vector3 m_startPosition;
    private Vector3 m_scale;
    private float m_curYPos = 0.0f;
    private float m_curZRot = 0.0f;
    private float m_curScale;

    public float MaxSize = 3.0f;
    public float Amplitude = 1.0f;
    public float RotationSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = transform.position;
        m_scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(Mathf.PingPong(Time.time, MaxSize),transform.localScale.y, transform.localScale.z);
        m_curScale = Mathf.PingPong(Time.time, MaxSize - 1);
        transform.localScale = new Vector3(m_scale.x + m_curScale,
                                           m_scale.y + m_curScale,
                                           m_scale.z + m_curScale);
        m_curYPos = Mathf.PingPong(Time.time, Amplitude) - Amplitude * 0.5f;
        transform.position = new Vector3(m_startPosition.x,
                                         m_startPosition.y + m_curYPos,
                                         m_startPosition.z);

        m_curZRot += Time.deltaTime * RotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, m_curZRot);
    }
}
