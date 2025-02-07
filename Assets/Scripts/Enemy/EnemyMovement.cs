using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyType { Helicopter, Jet };
    public EnemyType enemyType;

    public float speed = 2f;
    private float direction = 1f;
    private int maxDrops = 1;

    public GameObject bombPrefab;
    public GameObject paratrooperPrefab;

    public void SetDirection(float moveDir)
    {
        direction = moveDir;
        transform.localScale = new Vector3(5.5f * direction, 5.5f, 5.5f);
    }

    public void SetMaxDrops(int maxDrops)
    {
        this.maxDrops = maxDrops;
    }


    void Start()
    {
        if (bombPrefab != null || paratrooperPrefab != null)
        {
            for (int i = 0; i < maxDrops; i++)
            {
                float dropTime = Random.Range(1f, 3f);
                Invoke(nameof(DropPayload), dropTime);
            }
        }
    }


    void Update()
    {
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);

        if (Mathf.Abs(transform.position.x) > 10)
        {
            Destroy(gameObject);
        }
    }

    void DropPayload()
    {
        if (enemyType == EnemyType.Helicopter && paratrooperPrefab != null)
        {
            Instantiate(paratrooperPrefab, transform.position, Quaternion.identity);
        }
        else if (enemyType == EnemyType.Jet && bombPrefab != null)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }
}
