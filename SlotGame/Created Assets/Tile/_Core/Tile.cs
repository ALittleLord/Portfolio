using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Class for Tile scriptable object
/// Sets tile effects and cost
/// </summary>
[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile", order = 1)]
public class Tile : ScriptableObject
{
    public string tileName = "UNTITLED";
    public int type;
    public bool usable = true;
    public GameObject tilePrefab;
    public GameObject tileObject;

    
    [PreviewField]
    public Sprite tileSprite;

    [PreviewField]
    [PropertyTooltip("Effect for when Tile is selected")]
    [AssetsOnly]
    public Sprite selectedEffect;

    [PreviewField]
    [PropertyTooltip("Effect for when this Tile is applying an aura")]
    [AssetsOnly]
    public Sprite radiatingEffect;


    //Effect Settings
    [FoldoutGroup("Configuration")]
    public bool isEffect = false;

    [ShowIfGroup("Configuration/isEffect")]
    [BoxGroup("Configuration/isEffect/EffectConfig")]
    [DisableIf("xCost")]
    public int energyCost = 0;

    [BoxGroup("Configuration/isEffect/EffectConfig")]
    public bool xCost = false;

    [BoxGroup("Configuration/isEffect/EffectConfig")]
    public bool isAttack = false;

    [BoxGroup("Configuration/isEffect/EffectConfig")]
    [ShowIf("isAttack")]
    [MinValue(0)]
    [Indent]
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 3)]
    [HideLabel]
    public int attackAmount = 0;

    [BoxGroup("Configuration/isEffect/EffectConfig")]
    public bool isDefend = false;

    [BoxGroup("Configuration/isEffect/EffectConfig")]
    [ShowIf("isDefend")]
    [MinValue(0)]
    [Indent]
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 3)]
    [HideLabel]
    public int defendAmount = 0;

    [BoxGroup("Configuration/isEffect/EffectConfig")]
    public bool isStat = false;

    [BoxGroup("Configuration/isEffect/EffectConfig")]
    public bool isBuff = false;



    //Aura Settings
    [FoldoutGroup("Configuration")]
    public bool isAura = false;

    [ShowIfGroup("Configuration/isAura")]
    [BoxGroup("Configuration/isAura/AuraConfig")]
    public bool boostsAttack = false;

    [BoxGroup("Configuration/isAura/AuraConfig")]
    [ShowIf("boostsAttack")]
    [Indent]
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 3)]
    [HideLabel]
    public int boostsAttackAmount = 0;

    [BoxGroup("Configuration/isAura/AuraConfig")]
    public bool boostsDefend = false;

    [BoxGroup("Configuration/isAura/AuraConfig")]
    [ShowIf("boostsDefend")]
    [Indent]
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 3)]
    [HideLabel]
    public int boostsDefendAmount = 0;

    [BoxGroup("Configuration/isAura/AuraConfig")]
    public bool boostsStat = false;

    [BoxGroup("Configuration/isAura/AuraConfig")]
    public bool boostsBuff = false;

    //Colours
    [FoldoutGroup("Colours")]
    [TitleGroup("Colours/Tile")]
    public Color tileColor;

    [TitleGroup("Colours/Tile")]
    public Color activeColor;

    [TitleGroup("Colours/Sprite")]
    public Color spriteColor1;

    [TitleGroup("Colours/Sprite")]
    public Color spriteColor2;

    //Postion
    [BoxGroup("Position")]
    public int column;

    [BoxGroup("Position")]
    public int row;

    public Tile rootTile;

    public int bonusDamage = 0;
    public int bonusDefense = 0;


    Control controller;
    public int multiplier = 1; //Replace later


    //              //
    //Base Functions//
    //              //

    /// <summary>
    /// Tile initialization
    /// </summary>
    /// <param name="pos">Position to instantiate</param>
    /// <param name="parent">Parent to nest tile under</param>
    /// <param name="name">Tile Name</param>
    /// <param name="rootObj">Tile SO prefab</param>
    /// <param name="col">Column index</param>
    public void Initialize(Vector3 pos, GameObject parent, string name, Tile rootObj, int col)
    {
        controller = FindObjectOfType<Control>();
        column = col;

        if (tilePrefab != null)
        {   
            tileObject = Instantiate(tilePrefab, pos, Quaternion.identity);
            tileObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = tileSprite;
            tileObject.GetComponent<TileWatcher>().tile = this;
            if (parent != null)
            {
                tileObject.transform.parent = parent.transform;
                tileObject.transform.localPosition = pos;
            }

            if (name != null) tileObject.name = name + tileName;
        }
        if (rootObj != null) rootTile = rootObj;
    }

    /// <summary>
    /// Set tile usability state
    /// </summary>
    /// <param name="state">State to set to</param>
    public void ToggleUsable(bool state)
    {
        usable = state;
        tileObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = usable ? spriteColor1 : spriteColor2;
    }

    /// <summary>
    /// Apply aura affect from tile
    /// </summary>
    /// <param name="aura">Tile to apply aura from</param>
    public void ApplyAura(Tile aura)
    {
        if (aura.boostsAttack) bonusDamage += aura.boostsAttackAmount;
        if (aura.boostsDefend) bonusDefense += aura.boostsDefendAmount;
    }

    /// <summary>
    /// Reset bonuses on tile
    /// </summary>
    public void ResetBonuses()
    {
        bonusDamage = 0;
        bonusDefense = 0;
    }

    /// <summary>
    /// Call to destroy tile object
    /// </summary>
    public void DestroyObject()
    {
        Destroy(tileObject);
    }


    //                     //
    //Tile Effect Functions//
    //                     //

    /// <summary>
    /// Public call to run Tile Effect
    /// Distributes Effect Calls
    /// </summary>
    /// <param name="target">Target character of tile</param>
    /// <param name="runByPlayer">bool defining whether run from player or enemy</param>
    public void RunEffect(CharacterScript target, bool runByPlayer)
    {
        if (runByPlayer)
        {
            if (xCost || controller.currentEnergy >= energyCost)
            {
                if (xCost) multiplier = controller.currentEnergy;
                controller.currentEnergy -= xCost ? controller.currentEnergy : energyCost;

                if (isAttack) Attack(target);
                if (isDefend) Defend(target);

                if (controller.selectedTile) controller.selectedTile.ToggleUsable(false);
                controller.DeSelectTile();
            }
            else
            {
                Debug.Log("Not Enough Energy");
            }
        }
        else
        {
            if (isAttack) Attack(target);
            if (isDefend) Defend(target);
        }
    }

    /// <summary>
    /// Run Attack from tile
    /// </summary>
    /// <param name="target">Target Chararacter</param>
    void Attack(CharacterScript target)
    {
        for (int i = 0; i < multiplier; i++)
        {
            if (target.hp > 0)
            {
                Debug.Log(this + ": Attack for " + attackAmount + " + " + bonusDamage + " totaling " + (attackAmount + bonusDamage));
                target.DamageCharacter(attackAmount + bonusDamage);
            }
        }
    }

    /// <summary>
    /// Run Defend from tile
    /// </summary>
    /// <param name="target">Target to Defend</param>
    void Defend(CharacterScript target)
    {
        for (int i = 0; i < multiplier; i++)
        {
            Debug.Log(this + ": Defend for " + defendAmount + " + " + bonusDefense + " totaling " + (defendAmount + bonusDefense));
            target.ApplyArmour(defendAmount + bonusDefense);
        }
    }
}
