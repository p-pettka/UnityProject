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

public enum EDayTime
{
    Morning,
    Afternoon,
    Evening
}

public class BackgroundController : MonoBehaviour
{
    public SpriteRenderer Background01;
    public SpriteRenderer Background02;
    public SpriteRenderer Background03;
    public SpriteRenderer Clouds;
    public SpriteRenderer Sky;
    public SpriteRenderer Ground;
    public ParticleSystem Snow;
    private Color MorningColor;
    private Color AfternoonColor;
    private Color AfternoonWinterColor;
    private Color EveningColor;
    private int currentLevel;

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
                        Background01.sprite = GameplayManager.Instance.GameDatabase.SpringBg1;
                        Background02.sprite = GameplayManager.Instance.GameDatabase.SpringBg2;
                        Background03.sprite = GameplayManager.Instance.GameDatabase.SpringBg3;
                        Sky.sprite = GameplayManager.Instance.GameDatabase.SkyDay;
                        Ground.sprite = GameplayManager.Instance.GameDatabase.SpringGround;
                    }
                    break;
                case ESeason.Winter:
                    {
                        Background01.sprite = GameplayManager.Instance.GameDatabase.WinterBg1;
                        Background02.sprite = GameplayManager.Instance.GameDatabase.WinterBg2;
                        Background03.sprite = GameplayManager.Instance.GameDatabase.WinterBg3;
                        Sky.sprite = GameplayManager.Instance.GameDatabase.SkyEvening;
                        Ground.sprite = GameplayManager.Instance.GameDatabase.WinterGround;
                        Snow.Play();
                    }
                    break;
            }
        }
    }

    private EDayTime m_dayTime;

    public EDayTime DayTme
    {
        get { return m_dayTime; }
        set
        {
            m_dayTime = value;

                switch (m_dayTime)
            {
                case EDayTime.Morning:
                    {
                        Sky.sprite = GameplayManager.Instance.GameDatabase.SkyMorning;
                        changeColor(MorningColor);
                    }
                    break;

                case EDayTime.Afternoon:
                    {
                        Sky.sprite = GameplayManager.Instance.GameDatabase.SkyAfternoon;
                        if (Season == ESeason.Winter)
                        {
                            changeColor(AfternoonWinterColor);
                        }
                        else
                        {
                            changeColor(AfternoonColor);
                        }
                    }
                    break;

                case EDayTime.Evening:
                    {
                        Sky.sprite = GameplayManager.Instance.GameDatabase.SkyEvening;
                        changeColor(EveningColor);
                    }
                    break;
            }
        }
    }

    private void changeColor(Color color)
    {
        Ground.color = color;
        Background01.color = color;
        Background02.color = color;
        Background03.color = color;
    }

    private void Start()
    {
        Season = ESeason.Spring;
        Snow.Stop();
        MorningColor = new Color(1, 1, 1, 1);
        AfternoonColor = new Color(1, 0.66f, 0, 1);
        AfternoonWinterColor = new Color(1, 0.85f, 0.76f, 1);
        EveningColor = new Color(0.35f, 0.4f, 0.58f, 1);
    }

    private void Update()
    {
        currentLevel = GameplayManager.Instance.currentLevel;

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

        if (DayTme != EDayTime.Afternoon && (currentLevel == 2 || currentLevel == 5 || currentLevel == 8 || currentLevel == 11))
        {
            DayTme = EDayTime.Afternoon;
        }
        else if (DayTme != EDayTime.Evening && (currentLevel == 3 || currentLevel == 6 || currentLevel == 9 || currentLevel == 12))
        {
            DayTme = EDayTime.Evening;
        }
        else if (DayTme != EDayTime.Morning && (currentLevel == 1 || currentLevel == 4 || currentLevel == 7 || currentLevel == 10))
        {
            DayTme = EDayTime.Morning;
        }
    }
}
