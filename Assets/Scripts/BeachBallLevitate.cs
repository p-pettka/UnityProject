﻿using System.Collections;
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
    private float m_oldYPos = 0.0f;
    private float m_curZRot = 0.0f;
    private float m_curScale;
    private float startTime;
    private float elapsedTime;
    private bool isMoving;
    private int timer;
    private int directionChangeCount;
    private bool aSyncActive;

    public float MaxSize = 3.0f;
    public float Amplitude = 1.0f;
    public float RotationSpeed = 50;
    public float OldTime;

    /*IEnumerator BeachBallCoroutine()
    {
        while (true)
        {

            if (isMoving)
            {
                m_oldYPos = m_curYPos;
                m_curYPos = Mathf.PingPong(Time.time, Amplitude) - Amplitude * 0.5f;
                transform.position = m_startPosition + Vector3.up * m_curYPos;

                m_curZRot += Time.deltaTime * RotationSpeed;
                transform.rotation = Quaternion.Euler(0, 0, m_curZRot);

                //m_curScale = (Mathf.PingPong(Time.time, Amplitude) - Amplitude * 0.5f) * MaxSize;
                //m_curScale = Mathf.Clamp(m_curScale, -m_scale.x, MaxSize);
                //transform.localScale = m_scale + Vector3.one * m_curScale;

                if (m_oldYPos * m_curYPos < 0.0f)
                    ++directionChangeCount;

                if (directionChangeCount == 3 && Mathf.Abs(m_curYPos) < 0.02f)
                {
                    directionChangeCount = 1;
                    isMoving = false;
                }

                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(2);
                isMoving = true;
            }
        }
    }*/

    async void BeachBallAsync()
    {
        while (aSyncActive)
        {
            var timeDifference = Time.time - OldTime;
            if (timeDifference < Time.deltaTime)
                await Task.Yield();

            OldTime = Time.time;

            if (isMoving)
            {
                m_oldYPos = m_curYPos;
                m_curYPos = Mathf.PingPong(Time.time, Amplitude) - Amplitude * 0.5f;
                transform.position = m_startPosition + Vector3.up * m_curYPos;

                m_curZRot += Time.deltaTime * RotationSpeed;
                transform.rotation = Quaternion.Euler(0, 0, m_curZRot);

                m_curScale = (Mathf.PingPong(Time.time, Amplitude) - Amplitude * 0.5f) * MaxSize;
                m_curScale = Mathf.Clamp(m_curScale, -m_scale.x, MaxSize);
                transform.localScale = m_scale + Vector3.one * m_curScale;

                if (m_oldYPos * m_curYPos < 0.0f)
                    ++directionChangeCount;

                if (directionChangeCount == 3 && Mathf.Abs(m_curYPos) < 0.03f)
                {
                    directionChangeCount = 1;
                    isMoving = false;
                }
                await Task.Yield();
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                isMoving = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        aSyncActive = true;
        startTime = Time.time;
        m_startPosition = transform.position;
        m_loopPosition = m_startPosition;
        m_scale = transform.localScale;
        m_loopScale = transform.localScale;
        isMoving = true;
        m_curYPos = 0.0f;
        directionChangeCount = 0;
        elapsedTime = 0.0f;
        BeachBallAsync();
        //StartCoroutine(BeachBallCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Direction change counter: " + directionChangeCount);
        elapsedTime += Time.deltaTime;
        //if (!isMoving) startTime = 0;
        //elapsedTime = Time.time - startTime;
        //transform.localScale = new Vector3(Mathf.PingPong(Time.time, MaxSize),transform.localScale.y, transform.localScale.z);
    }

    private void OnApplicationQuit()
    {
        aSyncActive = false;
    }
}
