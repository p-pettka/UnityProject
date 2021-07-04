using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsController : MonoBehaviour
{
    public Button AcceptButton;
    public Button CancelButton;

    public MainMenuController MainMenu;

    private float m_initialVolume = 0.0f;

    void Start()
    {
        AcceptButton.onClick.AddListener(delegate { OnAccept(); });
        CancelButton.onClick.AddListener(delegate { OnCancel(); });
        OnEnable();
    }

    private void OnEnable()
    {
        m_initialVolume = AudioListener.volume;
    }

    private void OnAccept()
    {
        SaveManager.Instance.SaveData.m_masterVolume = AudioListener.volume;
        SaveManager.Instance.SaveSettings();
        MainMenu.ShowOptions(false);
    }

    private void OnCancel()
    {
        AudioListener.volume = m_initialVolume;
        MainMenu.ShowOptions(false);
    }
}