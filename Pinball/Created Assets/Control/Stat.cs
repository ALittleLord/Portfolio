using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Multipurpose scriptable object for adding stats to scene elements
/// Stores what type of stat it is, its scaling types/stats for both its value and its upgrading cost
/// Stores the level of the stat
/// 
/// *USE*
/// For each object being leveled seperatly (eg. seperate pinball types), a new stat object must be created for each of its stats
/// </summary>
[CreateAssetMenu(fileName = "Stat", menuName = "Pinball/Stat", order = 1)]
public class Stat : ScriptableObject
{
    public enum StatType{Default, Value, Friction, Growth}
    public StatType statType = StatType.Default;
    public int Level = 0;
    public float valueBase = 1;
    public float valueConstant = 1;
    public Scaling.ScalingType valueScaling = Scaling.ScalingType.Polynomal;
    public float upgradeCostBase = 1;
    public float upgradeCostConstant = 1;
    public Scaling.ScalingType upgradeCostScaling = Scaling.ScalingType.Exponential;

    /// <summary>
    /// Public call to get stat Upgrade Cost in accordance with its scaling and current level
    /// </summary>
    public int GetUpgradeCost()
    {
        return Scaling.RunScaleFunction(upgradeCostScaling, upgradeCostBase, upgradeCostConstant, Level);
    }

    /// <summary>
    /// Public call to get stat Value in accordance with its scaling and current level
    /// </summary>
    public int GetValue()
    {
        return Scaling.RunScaleFunction(valueScaling, valueBase, valueConstant, Level);
    }

    /// <summary>
    /// Public call to get stat Upgrade Cost in accordance with its scaling at input int level
    /// </summary>
    public int GetUpgradeCostAtLevel(int customLevel)
    {
        return Scaling.RunScaleFunction(upgradeCostScaling, upgradeCostBase, upgradeCostConstant, customLevel);
    }

    /// <summary>
    /// Public call to get stat Value in accordance with its scaling at input int level
    /// </summary>
    public int GetValueAtLevel(int customLevel)
    {
        return Scaling.RunScaleFunction(valueScaling, valueBase, valueConstant, customLevel);
    }

    /// <summary>
    ///Public call to initialize this stats values based on those of another input Stat
    /// </summary>
    public void Initialize(Stat prefab)
    {
        statType            = prefab.statType;
        Level               = prefab.Level;
        valueBase           = prefab.valueBase;
        valueConstant       = prefab.valueConstant;
        valueScaling        = prefab.valueScaling;
        upgradeCostBase     = prefab.upgradeCostBase;
        upgradeCostConstant = prefab.upgradeCostConstant;
        upgradeCostScaling  = prefab.upgradeCostScaling;
    }

    /// <summary>
    /// Public call to increase the level of this stat
    /// </summary>
    public void Upgrade()
    {
        Level++;
    }
}
