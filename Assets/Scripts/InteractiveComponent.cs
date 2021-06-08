﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveComponent : MonoBehaviour, IRestartableObject
{
    protected Vector3 m_startPosition;
    protected Quaternion m_startRotation;
    protected Rigidbody2D m_rigidbody;
    protected AudioSource m_audioSource;
    public AudioClip RestartSound;
    public AudioClip PullSound;
    public AudioClip ShootSound;
    public AudioClip ImpactSound;

    public virtual void DoRestart()
    {
        transform.position = m_startPosition;
        transform.rotation = m_startRotation;

        m_rigidbody.simulated = true;
    }

    protected virtual void DoPlay()
    {
        m_rigidbody.simulated = true;
    }

    protected virtual void DoPause()
    {
        m_rigidbody.simulated = false;
    }
}
