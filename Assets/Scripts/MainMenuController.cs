using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button PlayButton;
    public Button OptionsButton;
    public Button QuitButton;

    public GameObject MainPanel;

    void Start()
    {
        PlayButton.onClick.AddListener(delegate { OnPlay(); });
        QuitButton.onClick.AddListener(delegate { OnQuit(); });

        SetPanelVisible(true);
    }

    public void SetPanelVisible(bool visible)
    {
        MainPanel.SetActive(visible);
    }

    private void OnPlay()
    {
        SetPanelVisible(false);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

}