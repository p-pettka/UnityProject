using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTargetComponent : MonoBehaviour, IRestartableObject
{
    private int points;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Debug.Log("Added points: " + points);
            GameplayManager.Instance.Points += points;
            ProgressBarController.Instance.UpdateProgressBar();
            this.gameObject.SetActive(false);
        }
    }
    public void DoRestart()
    {
        gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        var points = GameplayManager.Instance.MaxPoints * 0.3f;
        this.points = (int)points;
    }
}
