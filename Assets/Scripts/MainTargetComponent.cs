using System.Collections;
using UnityEngine;

public class MainTargetComponent : InteractiveComponent
{
    public int pointsToAdd;

    IEnumerator ScorePoints()
    {
        Debug.Log("Added points: " + pointsToAdd);
        GameplayManager.Instance.Points += pointsToAdd;
        ProgressBarController.Instance.UpdateProgressBar();
        m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.ScoreSound);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            StartCoroutine(ScorePoints());
        }
    }
    public override void DoRestart()
    {
        base.DoRestart();
        gameObject.SetActive(true);
        m_rigidbody.velocity = Vector3.zero;
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_startPosition = transform.position;
        m_startRotation = transform.rotation;
    }
}