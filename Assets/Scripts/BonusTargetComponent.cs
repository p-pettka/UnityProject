using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTargetComponent : InteractiveComponent
{
    public int ballsToAdd;

    IEnumerator AddBonusBall()
    {
        Debug.Log("Added bonus ball: " + ballsToAdd);
        GameplayManager.Instance.Balls += ballsToAdd;
        m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.ScoreSound);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            StartCoroutine(AddBonusBall());
        }
    }
    public override void DoRestart()
    {
        base.DoRestart();
        gameObject.SetActive(true);
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
    }
}
