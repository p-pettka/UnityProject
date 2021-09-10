using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System;

[Serializable]

public struct GameSaveData
{
    public float m_timeSinceLastSave;
    public float m_overallTime;
    public int m_lifetimeHits;
    public float m_masterVolume;
}

public class SaveManager : Singleton<SaveManager>
{
    public GameSaveData SaveData;
    public bool UseBinary = true;
    private string m_pathBin;
    private string m_pathJSON;

    public void SaveSettings()
    {
        SaveData.m_overallTime += SaveData.m_timeSinceLastSave;
        SaveData.m_lifetimeHits = GameplayManager.Instance.LifetimeHits;
        Debug.Log("Saving overall time value: " + SaveData.m_overallTime);
        Debug.Log("Saving lifetime hits value: " + GameplayManager.Instance.LifetimeHits);
        SaveData.m_timeSinceLastSave = 0.0f;
        Debug.Log("GameSaveData overall Time: " + SaveData.m_overallTime);

        if (UseBinary)
        {
            FileStream file = new FileStream(m_pathBin, FileMode.OpenOrCreate);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(file, SaveData);
            file.Close();
        }
        else
        {
            string saveData = JsonUtility.ToJson(SaveData);
            File.WriteAllText(m_pathJSON, saveData);
        }
    }

    public void LoadSettings()
    {
        if (UseBinary && File.Exists(m_pathBin))
        {
            FileStream file = new FileStream(m_pathBin, FileMode.Open);
            BinaryFormatter binFormat = new BinaryFormatter();
            SaveData = (GameSaveData)binFormat.Deserialize(file);
            file.Close();
            GameplayManager.Instance.m_LifetimeHits = SaveData.m_lifetimeHits;
            ApplySettings();
        }
        else if (!UseBinary && File.Exists(m_pathJSON))
        {
            string saveData = File.ReadAllText(m_pathJSON);
            SaveData = JsonUtility.FromJson<GameSaveData>(saveData);
            GameplayManager.Instance.m_LifetimeHits = SaveData.m_lifetimeHits;
            ApplySettings();
        }
        else
        {
            SaveData.m_timeSinceLastSave = 0.0f;
            SaveData.m_masterVolume = AudioListener.volume;
        }
        Debug.Log("Loaded overall time value: " + SaveData.m_overallTime);
        Debug.Log("Lifetime hits: " + GameplayManager.Instance.LifetimeHits);
    }

    private void ApplySettings()
    {
        AudioListener.volume = SaveData.m_masterVolume;
    }

    public void Start()
    {
        UseBinary = true;
        m_pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        m_pathJSON = Path.Combine(Application.persistentDataPath, "save.json");

        SaveData.m_masterVolume = AudioListener.volume;
        LoadSettings();
    }

    private void Update()
    {
        SaveData.m_timeSinceLastSave += Time.deltaTime;
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }
}
