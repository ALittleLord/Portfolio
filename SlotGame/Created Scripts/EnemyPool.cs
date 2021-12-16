using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "ScriptableObjects/EnemyPool", order = 1)]
public class EnemyPool : ScriptableObject
{
    [AssetsOnly]
    [InlineEditor]
    public List<GameObject> pool = new List<GameObject>();
}
