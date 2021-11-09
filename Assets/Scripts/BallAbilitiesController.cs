using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAbilitiesController : MonoBehaviour
{
    public float explosionArea;
    public float explosionForce;

    public LayerMask LayerToHit;
    public AudioSource m_audioSource;

    private void ballExplode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionArea, LayerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
        }
        objects = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ballExplode();
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.TNTSound);
        }
    }
}
