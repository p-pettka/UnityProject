using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class BeachBallLevitate : MonoBehaviour
{
    private Vector3 m_startPosition;
    private Vector3 m_scale;
    private Vector3 m_loopPosition;
    private Vector3 m_loopScale;
    private float m_curYPos = 0.0f;
    private float m_curZRot = 0.0f;
    private float m_curScale;
    private float startTime;
    private float elapsedTime;
    private bool isMoving;
    private int timer;

    public float MaxSize = 3.0f;
    public float Amplitude = 1.0f;
    public float RotationSpeed = 50;

    IEnumerator BeachBallCoroutine()
    {
        while (true)
        {
            if (isMoving)
            {
                m_curScale = Mathf.PingPong(elapsedTime, MaxSize - 1);
                m_curYPos = Mathf.PingPong(elapsedTime, Amplitude) - Amplitude * 0.5f;
                m_curZRot += Time.deltaTime * RotationSpeed;
                timer++;
                yield return null;
                transform.localScale = new Vector3(m_scale.x + m_curScale,
                                       m_scale.y + m_curScale,
                                       m_scale.z + m_curScale);
                transform.position = new Vector3(m_startPosition.x,
                                     m_startPosition.y + m_curYPos,
                                     m_startPosition.z);
                transform.rotation = Quaternion.Euler(0, 0, m_curZRot);
                Debug.Log(timer);
                if (timer == 179)
                {
                    isMoving = false;
                    startTime = 0.0f;
                    timer = 0;
                }
            }
            else
            {
                yield return new WaitForSeconds(2);
                isMoving = true;
            }
        }
    }

    /*async void BeachBallAsync()
    {
        while (true)

            if (isMoving)
            {
                transform.localScale = m_scale;
                transform.position = m_startPosition;

                transform.localScale = new Vector3(m_scale.x + m_curScale,
                                       m_scale.y + m_curScale,
                                       m_scale.z + m_curScale);
                transform.position = new Vector3(m_startPosition.x,
                                     m_startPosition.y + m_curYPos,
                                     m_startPosition.z);
                transform.rotation = Quaternion.Euler(0, 0, m_curZRot);

                if (transform.localScale.x <= checkScale)
                {
                    isMoving = false;
                }
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                isMoving = true;
            }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        m_startPosition = transform.position;
        m_loopPosition = m_startPosition;
        m_scale = transform.localScale;
        m_loopScale = transform.localScale;
        isMoving = true;
        //BeachBallAsync();
        StartCoroutine(BeachBallCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) startTime = 0;
        elapsedTime = Time.time - startTime;
        //transform.localScale = new Vector3(Mathf.PingPong(Time.time, MaxSize),transform.localScale.y, transform.localScale.z);
    }
}
