using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct GameSaveData
{
    public float m_timeSinceLastSave;
    public float m_overallTime;
    public int m_lifetimeHits;
}

public class SaveManager : Singleton<SaveManager>
{
    public GameSaveData SaveData;
    public bool UseBinary = true;
    private string m_pathBin;
    private string m_pathJSON;

    public void SaveSetting()
    {
        SaveData.m_overallTime += SaveData.m_timeSinceLastSave;
        SaveData.m_lifetimeHits = GameplayManager.Instance.LifetimeHits;
        Debug.Log("Saving overall time value: " + SaveData.m_overallTime);
        Debug.Log("Saving lifetime hits value: " + GameplayManager.Instance.LifetimeHits);
        SaveData.m_timeSinceLastSave = 0.0f;
        Debug.Log("GameSaveData overall Time: " + SaveData.m_overallTime);

        if (UseBinary)
        {
            string saveData = JsonUtility.ToJson(SaveData);
            File.WriteAllText(m_pathJSON, saveData);
            /*FileStream file = new FileStream(m_pathBin, FileMode.OpenOrCreate);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(file, SaveData);
            file.Close();*/
        }
        else
        {
            string saveData = JsonUtility.ToJson(SaveData);
            File.WriteAllText(m_pathJSON, saveData);
        }
    }

    public void LoadSettings()
    {
        Debug.Log("Loaded overall time value: " + SaveData.m_overallTime);
        Debug.Log("Lifetime hits: " + GameplayManager.Instance.LifetimeHits);

        if (UseBinary && File.Exists(m_pathBin))
        {
            string saveData = File.ReadAllText(m_pathJSON);
            SaveData = JsonUtility.FromJson<GameSaveData>(saveData);
            GameplayManager.Instance.LifetimeHits = SaveData.m_lifetimeHits;
            /*FileStream file = new FileStream(m_pathBin, FileMode.Open);
            BinaryFormatter binFormat = new BinaryFormatter();
            SaveData = (GameSaveData)binFormat.Deserialize(file);
            file.Close();*/
        }
        else if (!UseBinary && File.Exists(m_pathJSON))
        {
            string saveData = File.ReadAllText(m_pathJSON);
            SaveData = JsonUtility.FromJson<GameSaveData>(saveData);
        }
        else
        {
            SaveData.m_timeSinceLastSave = 0.0f;
        }
    }

    public void Start()
    {
        m_pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        m_pathJSON = Path.Combine(Application.persistentDataPath, "save.json");
        SaveData.m_timeSinceLastSave = 0.0f;

        LoadSettings();
    }

    private void Update()
    {
        SaveData.m_timeSinceLastSave += Time.deltaTime;
    }

    private void OnApplicationQuit()
    {
        SaveSetting();
    }
}
