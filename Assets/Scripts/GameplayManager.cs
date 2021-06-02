using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EGameState
{
    Playing,
    Paused
}


public class GameplayManager : Singleton<GameplayManager>
{

    private EGameState m_state;
    private HUDController m_HUD;
    private int m_points = 0;
    public delegate void GameStateCallBack();
    public static event GameStateCallBack OnGamePaused;
    public static event GameStateCallBack OnGamePlaying;

    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();


    private void GetAllRestartableObjects()
    {
        m_restartableObjects.Clear();

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            IRestartableObject[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IRestartableObject>();

            foreach (var childInterface in childrenInterfaces)
                m_restartableObjects.Add(childInterface);
        }
    }

    public void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();
        Points = 0;
    }

    public int Points
    {
        get { return m_points; }
        set
        {
            m_points = value;
            m_HUD.UpdatePoints(m_points);
        }
    }

    public void PlayPause()
    {
        switch (GameState)
        {
            case EGameState.Playing: { GameState = EGameState.Paused; } break;
            case EGameState.Paused: { GameState = EGameState.Playing; } break;
        }
    }

    public EGameState GameState
    {
        get { return m_state; }
        set
        {
            m_state = value;

            switch (m_state)
            {
                case EGameState.Paused:
                    {
                        if (OnGamePaused != null)
                            OnGamePaused();
                    }
                    break;

                case EGameState.Playing:
                    {
                        if (OnGamePlaying != null)
                            OnGamePlaying();
                    }
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_state = EGameState.Playing;
        GetAllRestartableObjects();
        m_HUD = FindObjectOfType<HUDController>();
        Points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            PlayPause();

        if (Input.GetKeyUp(KeyCode.R))
            Restart();

        if (Input.GetKeyUp(KeyCode.Escape))
            GameState = EGameState.Paused;
       
    }
}
