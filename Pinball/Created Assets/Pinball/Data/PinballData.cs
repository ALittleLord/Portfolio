using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Pinball Data storage
/// Unique Data sheets need to be created for each ball prefab
/// Data Sheet is used to save data if storeData == true
/// Stores Stat a stat file prefab, and an instance of that stat to be used to avoid the base prefab being modified
/// </summary>
[CreateAssetMenu(fileName = "PinballData", menuName = "Pinball/PinballData", order = 1)]
public class PinballData : ScriptableObject
{
    public bool storeData = false;
    public Material baseMaterial;
    public float baseWeight = 1;

    [InlineEditor(InlineEditorModes.FullEditor)]
    [SerializeField] 
    Stat valueStatPrefab;
    [InlineEditor(InlineEditorModes.FullEditor)]
    [SerializeField] 
    Stat valueStatActive;

    /// <summary>
    /// Public call to return the active value Stat
    /// If there is no active stat creates a new one from prefab
    /// </summary>
    public Stat GetValueStat()
    {
        if (valueStatActive == null)
        {
            valueStatActive = CreateInstance<Stat>();
            valueStatActive.Initialize(valueStatPrefab);
        }

        return valueStatActive;
    }

    /// <summary>
    /// Future proofing public switch statement to be able to grab stat based on input Stat.StatType
    /// </summary>
    public Stat GetStat(Stat.StatType statType)
    {
        switch(statType)
        {
            case Stat.StatType.Value:
                return GetValueStat();
            default:
                return null;
        }
    }

    /// <summary>
    /// Sets ValueStat values to that of its prefab
    /// </summary>
    public void ResetValueStat()
    {
        GetValueStat().Initialize(valueStatPrefab);
    }

    /// <summary>
    /// Call to Reset all Stats on objects
    /// Duture proofing stat expansion
    /// </summary>
    public void ResetData()
    {
        ResetValueStat();
    }

    /// <summary>
    /// Upgrade Stat by Stat.StatType
    /// </summary>
    public void Upgrade(Stat.StatType statType)
    {
        GetStat(statType).Upgrade();
    }
}
