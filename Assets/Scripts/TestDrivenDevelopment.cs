using UnityEngine;

public class TestDrivenDevelopment : Singleton<TestDrivenDevelopment>
{
    public int passNumberOfTestTargets = 3;
    public int numberOfTargetsToSpawn;
    public int numberOfSpawnedTargets;
    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.Instance.SpawnTestTargets(numberOfTargetsToSpawn);
        if (numberOfSpawnedTargets == passNumberOfTestTargets)
        {
            Debug.LogWarning("Test-driven Development - " +passNumberOfTestTargets+ " test prefabs: true");
        }
        else
        {
            Debug.LogWarning("Test-driven Developemnt - "+ passNumberOfTestTargets+ " test prefabs: false");
        }
    }
}