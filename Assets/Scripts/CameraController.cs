using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private BallComponent followTarget;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        followTarget = FindObjectOfType<BallComponent>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
       
    }
    private void LateUpdate()
    {
        if (!followTarget.IsSimulated()) return;
        transform.position = Vector3.MoveTowards(transform.position, originalPosition + followTarget.transform.position, followTarget.PhysicsSpeed);
        //transform.position = followTarget.transform.position + originalPosition;

    }

}
