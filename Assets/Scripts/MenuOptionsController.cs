using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsController : MonoBehaviour
{
    public Button BackButton;
    public MainMenuController MainMenu;

    void Start()
    {
        BackButton.onClick.AddListener(delegate { OnBack(); });
    }

    private void OnBack()
    {
        MainMenu.ShowOptions(false);
    }
}