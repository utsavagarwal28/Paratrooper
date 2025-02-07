using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;

    public void AdjustDifficulty(int level)
    {
        if (level == 1)
        {
            enemySpawner.SetSpawnParameters(onlyHelicopters: true, onlyJets: false, spawnRate: 5f, maxDrops: 1);
        }
        else if (level == 2)
        {
            enemySpawner.SetSpawnParameters(onlyHelicopters: false, onlyJets: true, spawnRate: 4f, maxDrops: 1);
        }
        else if (level == 3)
        {
            enemySpawner.SetSpawnParameters(onlyHelicopters: false, onlyJets: false, spawnRate: 3f, maxDrops: 2);
        }
        else if (level >= 4)
        {
            enemySpawner.SetSpawnParameters(onlyHelicopters: false, onlyJets: false, spawnRate: Mathf.Max(2f, 5f - (level * 0.3f)), maxDrops: Mathf.Min(level, 5));
        }
    }
}
