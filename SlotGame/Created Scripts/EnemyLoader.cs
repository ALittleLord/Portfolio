using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to Handle loading enemies
/// </summary>
public class EnemyLoader : MonoBehaviour
{
    public List<EnemyPool> enemyPools = new List<EnemyPool>();

    [SerializeField] float enemyMarginMulti = 1;

    EnemyPool enemies;
    Control controller;

    /// <summary>
    /// At Start find controller and set up enemy pool
    /// </summary>
    void Start()
    {
        controller = FindObjectOfType<Control>();
        SetPool(enemyPools[0]);
    }

    /// <summary>
    /// Public call load Enemies from new pool
    /// </summary>
    /// <param name="pool">EnemyPool to load from</param>
    public void SetPool(EnemyPool pool)
    {
        enemies = pool;
        LoadEnemies();
    }

    /// <summary>
    /// Call to load enemies from current pool
    /// </summary>
    public void LoadEnemies()
    {
        for (int i = 0; i < enemies.pool.Count; i++)
        {
            LoadEnemy(enemies.pool[i], (i+1));
        }
    }

    /// <summary>
    /// Load and position enemy
    /// Apply scaling to the enemy
    /// </summary>
    /// <param name="enemy">The enemy prefab to load</param>
    /// <param name="fieldPos">The enemies position index</param>
    void LoadEnemy(GameObject enemy, int fieldPos)
    {
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.SetParent(GameObject.Find("Position: " + fieldPos).transform);
        newEnemy.transform.localPosition = Vector3.zero;
        newEnemy.transform.GetComponent<EnemyControl>().scaling+=3;
    }
}
