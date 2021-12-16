using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Sirenix.OdinInspector;

/// <summary>
/// Core class for all characters
/// Handles stats and applying effects
/// </summary>
public class CharacterScript : MonoBehaviour
{
    [PreviewField]
    [HorizontalGroup]
    [Required]
    public GameObject model;


    [HorizontalGroup]
    [Title("Rotate")]
    [HideLabel]
    public float modelRotation = 0;

    public bool player = false;
    [BoxGroup("Stats")]
    public int maxHp = 20;

    [BoxGroup("Stats")]
    public int hp;

    [BoxGroup("Stats")]
    public int armour = 0;

    [SerializeField]TMP_Text statDisplay;
    Control controller;
    ValueIndicator indicator;

    /// <summary>
    /// Editor testing button to kill character
    /// </summary>
    [Button(ButtonSizes.Large), GUIColor(1, 0, 0)]
    private void KillButton()
    {
        controller.KillCharacter(this);
    }


    /// <summary>
    /// At start spawn the sprite and initialize variables
    /// </summary>
    void Start()
    {
        SpawnModel();
        controller = FindObjectOfType<Control>();
        indicator = GetComponent<ValueIndicator>();
        hp = maxHp;
    }

    /// <summary>
    /// On update refresh stats
    /// </summary>
    /// Note: Inefficient move elsewhere
    void Update()
    {
        UpdateStatDisplay();
    }

    /// <summary>
    /// On click run effects of selected tile, if selected
    /// </summary>
    void OnMouseDown()
    {   
        Debug.Log(this + " was clicked");
        if(controller.selectedTile != null)
        {
            controller.selectedTile.RunEffect(this, true);
        }
    }

    /// <summary>
    /// Spawn/Position the character model
    /// </summary>
    void SpawnModel()
    {
        Transform spawnedModel = Instantiate(model).transform;
        spawnedModel.SetParent(transform);
        spawnedModel.localPosition = new Vector3(0, -0.5f, 0);
        spawnedModel.Rotate(0, modelRotation, 0);
    }


    /// <summary>
    /// Update character stat ui
    /// </summary>
    void UpdateStatDisplay()
    {
        statDisplay.text = "Hp: " + hp + " Armour: " + armour;
    }

    /// <summary>
    /// Set Hp and Armor from input
    /// </summary>
    /// <param name="newHp">New hp</param>
    /// <param name="newArmour">New armor value, defaults to 0</param>
    public void SetHp(int newHp, int newArmour = 0)
    {
        maxHp = newHp;
        hp = newHp;
        armour = newArmour;
    }
    /// <summary>
    /// Public call to damage character
    /// Disperses damage to armour and then character
    /// </summary>
    public void DamageCharacter(int damage)
    {
        if(armour > 0)
        {
            damage = DamageArmour(damage);
        }

        ApplyDamageToHealth(damage);
    }

    /// <summary>
    /// Public call to add armour to character
    /// </summary>
    /// <param name="value">Armour to apply</param>
    public void ApplyArmour(int value)
    {
        armour += value;
    }

    /// <summary>
    /// Public call to heal character
    /// </summary>
    /// <param name="value">Amount to heal by</param>
    public void Heal(int value)
    {
        hp += value;
    }

    /// <summary>
    /// Call to damage health
    /// Protected through DamageCharacter()
    /// Kills character if it takes health below 0
    /// </summary>
    /// <param name="value">Amount of damage</param>
    private void ApplyDamageToHealth(int value)
    {
        hp -= value;
        if (indicator != null) indicator.createIndicator(value, 1);
        if (hp <= 0) controller.KillCharacter(this);
    }

    /// <summary>
    /// Call to Damage Armour
    /// Run through DamageCharacter()
    /// </summary>
    /// <param name="value">Amount of damage</param>
    /// <returns></returns>
    private int DamageArmour(int value)
    {
        armour -= value;
        if (armour <= 0)
        {
            int overage = -armour;
            armour = 0;
            return overage;
        }

        return 0;
    }
}
