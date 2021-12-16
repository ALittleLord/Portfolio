using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


/// <summary>
/// Scriptable Object Class to  create enemy moves
/// </summary>
[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Move", order = 1)]
public class EnemyMove : ScriptableObject
{
    public string Description;
    public List<GameObject> usedBy = new List<GameObject>();

    [FoldoutGroup("Configuration")]
    public bool isAttack = false;

    [HorizontalGroup("Configuration/Horiz0")]
    [ShowIf("isAttack")]
    [MinValue(0)]
    [Indent]
    [HideLabel]
    public int attackAmount = 0;

    [HorizontalGroup("Configuration/Horiz0")]
    [ShowIf("isAttack")]
    [MinValue(0)]
    [Indent]
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 3)]
    [HideLabel]
    [SuffixLabel("time(s)")]
    public int attackMultiplier = 1;

    [FoldoutGroup("Configuration")]
    public bool isDefend = false;

    [HorizontalGroup("Configuration/Horiz1")]
    [ShowIf("isDefend")]
    [MinValue(0)]
    [Indent]
    [HideLabel]
    public int defendAmount = 0;

    [HorizontalGroup("Configuration/Horiz1")]
    [ShowIf("isDefend")]
    [MinValue(0)]
    [Indent]
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 3)]
    [HideLabel]
    [SuffixLabel("time(s)")]
    public int defendMultiplier = 1;

    [FoldoutGroup("Configuration")]
    public bool isStat = false;

    [FoldoutGroup("Configuration")]
    public bool isBuff = false;
}
