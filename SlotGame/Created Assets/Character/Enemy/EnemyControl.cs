using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

/// <summary>
/// Enemy Controller Class
/// </summary>
public class EnemyControl : MonoBehaviour
{    
    //Config
    [FoldoutGroup("Configuration")]
    [FoldoutGroup("Configuration/Core")]
    public int scaling = 0;

    [FoldoutGroup("Configuration/Core")]
    TextMeshPro moveIndicator;

    [FoldoutGroup("Configuration/Core")]
    [SerializeField] CharacterScript target;

    [FoldoutGroup("Configuration/Core")]
    Control controller;

    //BaseStats
    [FoldoutGroup("Configuration/BaseStats")]
    [Title("HP")]
    [MinValue(1)]
    [MaxValue("hpMax")]
    public int hpMin = 1;

    [FoldoutGroup("Configuration/BaseStats")]
    [MinValue("hpMin")]
    public int hpMax = 100;

    [FoldoutGroup("Configuration/BaseStats")]
    [Title("Armour")]
    [MinValue(0)]
    public int armour = 0;

    //Moves
    [FoldoutGroup("Configuration")]
    [Searchable]
    [AssetsOnly]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public List<EnemyMove> moves = new List<EnemyMove>();

    EnemyMove nextMove;


    CharacterScript characterScript;

    //Functions

    /// <summary>
    /// At Start inititliaze variables and declare an attack
    /// </summary>
    void Start()
    {
        controller = FindObjectOfType<Control>();
        controller.activeEnemies.Add(this);
        characterScript = GetComponent<CharacterScript>();
        characterScript.SetHp(Random.Range(hpMin,hpMax), armour);
        target = GameObject.Find("Player").GetComponent<CharacterScript>();
        moveIndicator = transform.GetComponentInChildren<TextMeshPro>();
        DeclareAttack();

        controller.fightRunning = true;
    }

    /// <summary>
    /// Run Turn
    /// </summary>
    public void RunTurn()
    {   
        if(target!=null)
        {
           
        }
    }

    //Selects random attack value for next turn and posts beside character
    public void DeclareAttack()
    {
        nextMove = moves[Random.Range(0,moves.Count)];
        SetText();
    }

    /// <summary>
    /// Set declared attack text
    /// </summary>
    void SetText()
    {
        string setText = "";

        if (nextMove.isAttack)
        {
            Debug.Log("SettingAttack");
            setText = setText + nextMove.attackAmount + " DMG";
            if (nextMove.attackMultiplier > 1) setText = setText + " X " + nextMove.attackMultiplier;
            setText = setText + "\n";
        }

        if (nextMove.isDefend)
        {
            Debug.Log("SettingDefend");
            setText = setText + nextMove.defendAmount + " DFND";
            if (nextMove.defendMultiplier > 1) setText = setText + " X " + nextMove.defendMultiplier;
            setText = setText + "\n";
        }
        transform.GetChild(1).GetComponent<TextMeshPro>().text = setText;
    }
}
