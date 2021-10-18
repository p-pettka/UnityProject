using System.Collections;
using UnityEngine;

public class MainTargetComponent : MonoBehaviour, IRestartableObject
{
    public int pointsToAdd;
    private AudioSource m_audioSource;

    IEnumerator ScorePoints()
    {
        Debug.Log("Added points: " + pointsToAdd);
        GameplayManager.Instance.Points += pointsToAdd;
        ProgressBarController.Instance.UpdateProgressBar();
        m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.ScoreSound);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            StartCoroutine(ScorePoints());
        }
    }
    public void DoRestart()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
}
