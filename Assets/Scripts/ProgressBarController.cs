using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : Singleton<ProgressBarController>
{
    public Image Bar;
    public Image BarBackground;

    public void UpdateProgressBar()
    {
        float maxPoints = GameplayManager.Instance.MaxPoints;
        float points = GameplayManager.Instance.Points;
        float value = points / maxPoints;
        Bar.fillAmount = value;
    }
}
