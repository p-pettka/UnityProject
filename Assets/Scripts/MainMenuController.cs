using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button PlayButton;
    public Button OptionsButton;
    public Button QuitButton;

    public GameObject MainPanel;
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;

    void Start()
    {
        PlayButton.onClick.AddListener(delegate {
            OnPlay();
            //SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            GameplayManager.Instance.LoadLevel(GameplayManager.Instance.currentLevel);
            GameplayManager.Instance.Restart();
            GameplayManager.Instance.GameState = EGameState.Playing;});
        OptionsButton.onClick.AddListener(delegate {
            ShowOptions(true); });
        QuitButton.onClick.AddListener(delegate {
            OnQuit(); });

        SetPanelVisible(true);
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void SetPanelVisible(bool visible)
    {
        MainPanel.SetActive(visible);
    }

    private void OnPlay()
    {
        SetPanelVisible(false);

    }

    public void ShowOptions(bool bShow)
    {
        OptionsPanel.SetActive(bShow);
        MainMenuPanel.SetActive(!bShow);
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}