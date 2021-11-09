using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public enum EGameState
{
    Playing,
    Paused
}

public class GameplayManager : Singleton<GameplayManager>
{
    private EGameState m_state;
    private HUDController m_HUD;
    public int m_points = 0;
    public int m_maxPoints;
    public float m_passPoints;
    public int m_balls = 3;
    private float m_frames;
    private BallComponent m_ball;
    public int currentLevel = 1;
    public float ballVelocity;
    public int m_LifetimeHits;
    public delegate void GameStateCallBack();
    public static event GameStateCallBack OnGamePaused;
    public static event GameStateCallBack OnGamePlaying;
    public GameSettingsDatabase GameDatabase;

    public List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();

    public void GetAllRestartableObjects()
    {
        m_restartableObjects.Clear();
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (var rootGameObject in rootGameObjects)
        {
            IRestartableObject[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IRestartableObject>();

            foreach (var childInterface in childrenInterfaces)
            {
                m_restartableObjects.Add(childInterface);
            }
        }

        m_restartableObjects.Add(m_ball);
        m_maxPoints = (m_restartableObjects.Count - 1);
        m_passPoints = m_maxPoints * 0.7f;
    }

    public void SpawnTestTargets(int numberOfTargets)
    {
        numberOfTargets = TestDrivenDevelopment.Instance.numberOfTargetsToSpawn;

        for (int i = 0; i < numberOfTargets; i++)
        {
            float xPosition = UnityEngine.Random.Range(2.0f, 15.0f);
            GameObject.Instantiate(GameDatabase.TargetPrefab, new Vector3(xPosition, 0.17f, 0.0f), Quaternion.identity);
            TestDrivenDevelopment.Instance.numberOfSpawnedTargets++;
        }
        GetAllRestartableObjects();
    }

    public void LoadLevel(int levelNumber)
    {
        StartCoroutine(LoadScene(levelNumber));
    }

    public void LoadNextLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(currentLevel));
        StartCoroutine(LoadScene(currentLevel + 1));
        currentLevel += 1;
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        if (SceneManager.GetSceneByBuildIndex(sceneIndex).IsValid())
        {
            yield break;
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Progress: " + asyncOperation.progress);
        while (!asyncOperation.isDone)
        {
            Debug.Log("Loading progress: " + asyncOperation.progress + "%");
            yield return null;
            asyncOperation.allowSceneActivation = true;
        }

        if (asyncOperation.allowSceneActivation)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
            GetAllRestartableObjects();
            GameState = EGameState.Playing;
            Debug.Log("Max points: " + MaxPoints);
            Debug.Log("Loaded scene number: " + sceneIndex);
        }
    }

    async void FPSCounter()
    {
        while (true)
        {
            float m_fps;
            await Task.Delay(TimeSpan.FromSeconds(1));
            m_fps = (Time.frameCount / Time.time);
            Debug.Log("FPS: " + m_fps);
        }
    }

    IEnumerator FPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            m_frames = (Time.frameCount / Time.time);
            Debug.Log("FPS: " + (Mathf.Round(m_frames)));
        }
    }

    IEnumerator TestCoroutine()
    {
        Debug.Log("Starting coroutine method");
        yield return new WaitForSeconds(3);
        Debug.Log("Coroutine done after 3 seconds");
    }

    public void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
        {
            restartableObject.DoRestart();
        }
        Points = 0;
        Balls = 3;
        ProgressBarController.Instance.UpdateProgressBar();
        GetAllRestartableObjects();
    }

    public void BallRestart()
    {
        foreach (var restartableObject in m_restartableObjects)
        {
            if (restartableObject.ToString() == "Ball (BallComponent)")
            {
                restartableObject.DoRestart();
            }
        }
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

    public int MaxPoints
    {
        get { return m_maxPoints; }
        set
        {
            m_maxPoints = value;
            m_HUD.UpdatePoints(m_maxPoints);
        }
    }

    public int Balls
    {
        get { return m_balls; }
        set
        {
            m_balls = value;
            m_HUD.UpdateBalls(m_balls);
        }
    }

    public int LifetimeHits
    {
        get { return m_LifetimeHits; }
        set { m_LifetimeHits = value; }
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
        m_state = EGameState.Paused;
        m_HUD = FindObjectOfType<HUDController>();
        m_ball = FindObjectOfType<BallComponent>();
        Points = 0;
        Balls = 3;

        //GameObject.Instantiate(GameDatabase.TargetPrefab, new Vector3(0.35f, 4.25f, 0.0f), Quaternion.identity);
        //GameObject.Instantiate(GameDatabase.AnimPrefab, new Vector3(-2.0f, -1.7f, -0.7f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            Restart();

        if (Input.GetKeyUp(KeyCode.Escape))
            GameState = EGameState.Paused;
    }
}
