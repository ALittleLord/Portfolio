using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Class to load level and enemies
/// </summary>
public class LevelLoader : MonoBehaviour
{
    [AssetList(Path = "Character/Enemy/EnemyPools")]
    public List<EnemyPool> enemyPools = new List<EnemyPool>();
    public GameObject _restSite;
    Control controller;

    void Start()
    {
        controller = FindObjectOfType<Control>();
        LoadFight();
    }

    /// <summary>
    /// Loads rest site level
    /// </summary>
    public void LoadRest()
    {
        GameObject restSite = Instantiate(_restSite);
        restSite.transform.SetParent(GameObject.Find("Position: 2").transform);
        restSite.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Loads a fight
    /// </summary>
    public void LoadFight()
    {
        EnemyPool enemies = enemyPools[Random.Range(0, enemyPools.Count)];

        for (int i = 0; i < enemies.pool.Count; i++)
        {
            LoadEnemy(enemies.pool[i], (i + 1));
        }
    }

    /// <summary>
    /// Loads enemy
    /// </summary>
    /// <param name="enemy">enemy to load</param>
    /// <param name="fieldPos">Position index</param>
    void LoadEnemy(GameObject enemy, int fieldPos)
    {
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.SetParent(GameObject.Find("Position: " + fieldPos).transform);
        newEnemy.transform.localPosition = Vector3.zero;
        newEnemy.transform.GetComponent<EnemyControl>().scaling += 3;
    }
}
