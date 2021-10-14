using UnityEngine;

public class CameraController : MonoBehaviour
{
    private BallComponent followTarget;
    private Vector3 originalPosition;
    private float dragSpeed = 2;
    private Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        followTarget = FindObjectOfType<BallComponent>();
        originalPosition = transform.position;
    }
    private void LateUpdate()
    {
        if (!followTarget.IsSimulated()) return;

        transform.position = Vector3.MoveTowards(transform.position, originalPosition + followTarget.transform.position, followTarget.PhysicsSpeed + 0.5f);

        if (Input.GetKeyUp(KeyCode.R))
        {
            transform.position = originalPosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, 0);

        transform.Translate(move, Space.World);

    }
}
