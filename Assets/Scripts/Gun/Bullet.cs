using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trooper"))
        {
            other.GetComponent<ParatrooperController>().GetShot();
            Destroy(gameObject);
            uiManager.UpdateScore(5);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Helicopter") || other.CompareTag("Jet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            uiManager.UpdateScore(10);
        }
    }
};

