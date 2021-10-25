using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelMenu : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Header;
    public GameObject FailHeader;
    public Button NextLevelButton;
    public Button RestartButton;
    private Image HeaderImage;

    public void SetPanelVisible(bool visible)
    {
        Panel.SetActive(visible);
    }

    private void NextLevel()
    {
        SetPanelVisible(false);
        GameplayManager.Instance.Restart();
        GameplayManager.Instance.LoadNextLevel();
    }

    void Start()
    {
        HeaderImage = Header.GetComponent<Image>();
        SetPanelVisible(false);

        NextLevelButton.onClick.AddListener(delegate {
            NextLevel();
        });

        RestartButton.onClick.AddListener(delegate {
            GameplayManager.Instance.Restart();
            SetPanelVisible(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.Instance.m_balls == 0 || GameplayManager.Instance.m_points >= GameplayManager.Instance.m_maxPoints)
        {
            SetPanelVisible(true);

            if (GameplayManager.Instance.m_points >= GameplayManager.Instance.m_passPoints)
            {
                FailHeader.SetActive(false);
                NextLevelButton.gameObject.SetActive(true);
                HeaderImage.sprite = GameplayManager.Instance.GameDatabase.WinHeader;
            }
            else
            {
                FailHeader.SetActive(true);
                NextLevelButton.gameObject.SetActive(false);
                HeaderImage.sprite = GameplayManager.Instance.GameDatabase.FailedHeader;
            }
        }
    }
}
