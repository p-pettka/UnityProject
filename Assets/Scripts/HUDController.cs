using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Button PauseButton;
    public Button RestartButton;
    public Button LoadSpriteButton;
    public Button LoadSceneButton;
    public TMPro.TextMeshProUGUI PointsText;
    public GameObject Hud;
    public void SetHudVisible(bool visible)
    {
        Hud.SetActive(visible);
    }

    private void OnPause()
    {
        SetHudVisible(false);
    }
    private void OnResume()
    {
        SetHudVisible(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetHudVisible(true);
        GameplayManager.OnGamePaused += OnPause;
        GameplayManager.OnGamePlaying += OnResume;

        PauseButton.onClick.AddListener(delegate {
            GameplayManager.Instance.PlayPause();
        });
        RestartButton.onClick.AddListener(delegate {
            GameplayManager.Instance.Restart();
        });
        LoadSpriteButton.onClick.AddListener(delegate {
            SpriteAssetLoader.Instance.SetSprites();
        });
        LoadSceneButton.onClick.AddListener(delegate {
            AssetBundlesManager.Instance.LoadScene(1);
        });

    }

    public void UpdatePoints(int points)
    {
        PointsText.text = "Points: " + points;
    }
}
