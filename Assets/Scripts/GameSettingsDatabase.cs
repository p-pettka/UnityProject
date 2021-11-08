using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Create Game Settings", order = 1)]

public class GameSettingsDatabase : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject TargetPrefab;
    public GameObject AnimPrefab;

    [Header("AudioClips")]
    public AudioClip RestartSound;
    public AudioClip PullSound;
    public AudioClip ShootSound;
    public AudioClip ImpactSound;
    public AudioClip BackgroudMusic;
    public AudioClip ScoreSound;

    [Header("Sprites")]
    public Sprite PlankSprite;
    public Sprite DamagedPlankSprite;
    public Sprite WinHeader;
    public Sprite FailedHeader;

    [Header("Season sprites")]
    public Sprite SkyDay;
    public Sprite SkyNight;
    public Sprite SkyMorning;
    public Sprite SkyAfternoon;
    public Sprite SkyEvening;
    public Sprite SummerGround;
    public Sprite SummerBg1;
    public Sprite SummerBg2;
    public Sprite SummerBg3;
    public Sprite AutumnGround;
    public Sprite AutumnBg1;
    public Sprite AutumnBg2;
    public Sprite AutumnBg3;
    public Sprite WinterGround;
    public Sprite WinterBg1;
    public Sprite WinterBg2;
    public Sprite WinterBg3;
    public Sprite SpringGround;
    public Sprite SpringBg1;
    public Sprite SpringBg2;
    public Sprite SpringBg3;
}