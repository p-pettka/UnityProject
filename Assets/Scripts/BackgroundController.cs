using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESeason
{
    Spring,
    Summer,
    Autumn,
    Winter
}

public class BackgroundController : MonoBehaviour
{
    public SpriteRenderer Background01;
    public SpriteRenderer Background02;
    public SpriteRenderer Background03;
    public SpriteRenderer Clouds;
    public SpriteRenderer Sky;
    public SpriteRenderer Ground;

    private ESeason m_season;

    public ESeason Season
    {
        get { return m_season; }
        set
        {
            m_season = value;

            switch (m_season)
            {
                case ESeason.Autumn:
                    {
                        Background01.sprite = GameplayManager.Instance.GameDatabase.AutumnBg1;
                        Background02.sprite = GameplayManager.Instance.GameDatabase.AutumnBg2;
                        Background03.sprite = GameplayManager.Instance.GameDatabase.AutumnBg3;
                        Sky.sprite = GameplayManager.Instance.GameDatabase.SkyDay;
                        Ground.sprite = GameplayManager.Instance.GameDatabase.AutumnGround;
                    }
                    break;
                case ESeason.Summer:
                    {
                        Background01.sprite = GameplayManager.Instance.GameDatabase.SummerBg1;
                        Background02.sprite = GameplayManager.Instance.GameDatabase.SummerBg2;
                        Background03.sprite = GameplayManager.Instance.GameDatabase.SummerBg3;
                        Sky.sprite = GameplayManager.Instance.GameDatabase.SkyDay;
                        Ground.sprite = GameplayManager.Instance.GameDatabase.SummerGround;
                    }
                    break;
                case ESeason.Spring:
                    {

                    }
                    break;
                case ESeason.Winter:
                    {
                        Background01.sprite = GameplayManager.Instance.GameDatabase.WinterBg1;
                        Background02.sprite = GameplayManager.Instance.GameDatabase.WinterBg2;
                        Background03.sprite = GameplayManager.Instance.GameDatabase.WinterBg3;
                        Sky.sprite = GameplayManager.Instance.GameDatabase.SkyNight;
                        Ground.sprite = GameplayManager.Instance.GameDatabase.WinterGround;
                    }
                    break;
            }
        }
    }

    private void Update()
    {
        if(GameplayManager.Instance.currentLevel == 1 && Season != ESeason.Spring)
        {
            Season = ESeason.Spring;
        }
        else if (GameplayManager.Instance.currentLevel == 4 && Season != ESeason.Summer)
        {
            Season = ESeason.Summer;
        }
        else if (GameplayManager.Instance.currentLevel == 7 && Season != ESeason.Autumn)
        {
            Season = ESeason.Autumn;
        }
        else if (GameplayManager.Instance.currentLevel == 10 && Season != ESeason.Winter)
        {
            Season = ESeason.Winter;
        }
    }
}
