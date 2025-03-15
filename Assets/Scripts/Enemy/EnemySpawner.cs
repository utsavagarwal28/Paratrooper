using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject jetPrefab;
    public GameObject heliPrefab;

    private bool onlyHelicopters = true;
    private bool onlyJets = false;
    public float spawnRate = 5f;
    public int maxDrops = 1;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnRate);
    }

    public void SetSpawnParameters(bool onlyHelicopters, bool onlyJets, float spawnRate, int maxDrops)
    {
        this.onlyHelicopters = onlyHelicopters;
        this.onlyJets = onlyJets;
        this.spawnRate = spawnRate;
        this.maxDrops = maxDrops;

        CancelInvoke(nameof(SpawnEnemy));
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnRate);
    }

    void SpawnEnemy()
    {
        if (onlyHelicopters && onlyJets)
        {
            return;
        }

        int enemyType = onlyHelicopters ? 1 : onlyJets ? 0 : Random.Range(0, 2);
        bool spawnFromRight = Random.value > 0.5f;
        float spawnHeight = Random.Range(3.5f, 4.5f);


        Vector3 spawnPosition;
        if (spawnFromRight)
            spawnPosition = new Vector3((9.5f), spawnHeight, 0);
        else
            spawnPosition = new Vector3((-9.5f), spawnHeight, 0);

        GameObject enemy = Instantiate((enemyType == 0 ? jetPrefab : heliPrefab), spawnPosition, Quaternion.identity);


        enemy.GetComponent<EnemyMovement>().SetDirection(spawnFromRight ? -1 : 1);
        enemy.GetComponent<EnemyMovement>().SetMaxDrops(maxDrops);
    }

}
