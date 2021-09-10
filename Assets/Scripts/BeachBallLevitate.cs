using System.Collections;
using UnityEngine;

public class BeachBallLevitate : MonoBehaviour
{
    private Vector3 m_startPosition;
    private float m_curYPos = 0.0f;
    private float m_oldYPos = 0.0f;
    private float m_curZRot = 0.0f;
    private bool isMoving;
    private int directionChangeCount;
    public float MaxSize = 3.0f;
    public float Amplitude = 1.0f;
    public float RotationSpeed = 50;
    public float OldTime;

    IEnumerator BeachBallCoroutine()
    {
        while (true)
        {

            if (isMoving)
            {
                m_oldYPos = m_curYPos;
                m_curYPos = Mathf.PingPong(Time.time, Amplitude) - Amplitude * 0.5f;
                transform.position = m_startPosition + Vector3.up * m_curYPos;

                m_curZRot += Time.deltaTime * RotationSpeed;
                transform.rotation = Quaternion.Euler(0, 0, m_curZRot);

                if (m_oldYPos * m_curYPos < 0.0f)
                    ++directionChangeCount;

                if (directionChangeCount == 3 && Mathf.Abs(m_curYPos) < 0.02f)
                {
                    directionChangeCount = 1;
                    isMoving = false;
                }

                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(2);
                isMoving = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = transform.position;
        isMoving = true;
        m_curYPos = 0.0f;
        directionChangeCount = 0;
        StartCoroutine(BeachBallCoroutine());
    }
}
