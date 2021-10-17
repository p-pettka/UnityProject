using UnityEngine;

public class MainTargetComponent : MonoBehaviour, IRestartableObject
{
    public int pointsToAdd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Debug.Log("Added points: " + pointsToAdd);
            GameplayManager.Instance.Points += pointsToAdd;
            ProgressBarController.Instance.UpdateProgressBar();
            this.gameObject.SetActive(false);
        }
    }
    public void DoRestart()
    {
        gameObject.SetActive(true);
    }
}
