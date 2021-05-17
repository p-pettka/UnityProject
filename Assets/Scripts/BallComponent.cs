using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    Rigidbody2D m_rigidbody;

    private void OnMouseDrag()
    {
        if (GameplayManager.Instance.Pause) return;
        m_rigidbody.simulated = false;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);

    }
    private void OnMouseUp()
    {
        m_rigidbody.simulated = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
                transform.position += new Vector3(0, 1, 0);

            if (Input.GetKeyDown(KeyCode.DownArrow))
                transform.position -= new Vector3(0, 1, 0);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.position -= new Vector3(1, 0, 0);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.position += new Vector3(1, 0, 0);

        if (GameplayManager.Instance.Pause)
        {
            m_rigidbody.bodyType = RigidbodyType2D.Static;
        }else
        {
            m_rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        }


    }
