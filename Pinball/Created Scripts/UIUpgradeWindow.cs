using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the UpgradeWindow for the BallMenu
/// Set Upgrades switch will need to be extended upon creation of new stat types for balls
/// </summary>
public class UIUpgradeWindow : MonoBehaviour
{
    public Sprite valueIcon;
    public List<UpgradeSlot> upgradeSlots = new List<UpgradeSlot>();
    UIBallMenu uiBallMenu;
    PinballData activeData;

    /// <summary>
    /// Grab scene objects on awake
    /// </summary>
    void Awake()
    {
        uiBallMenu = transform.parent.GetComponent<UIBallMenu>();
    }

    /// <summary>
    /// Sets data to each UpgradeSlot
    /// </summary>
    public void SetUpgrades(PinballData data)
    {
        activeData = data;
        foreach (UpgradeSlot slot in upgradeSlots)
        {
            switch(slot.statType)
            {
                case Stat.StatType.Value:
                    slot.currentCost = activeData.GetValueStat().GetUpgradeCost();
                    slot.currentValue = activeData.GetValueStat().GetValue();
                    slot.icon = valueIcon;
                    slot.slotLabel = "Upgrade " + slot.statType;
                    break;  
                default:
                    break;
            }

            slot.Refresh();
        }
    }
}
