using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    public bool Pause = false;
    // Start is called before the first frame update
    void Start()
    {
        Pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            Pause = !Pause;
    }
}
