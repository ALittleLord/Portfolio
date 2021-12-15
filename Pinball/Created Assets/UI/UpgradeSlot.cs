using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MPUIKIT;

/// <summary>
/// Handles individual pinball upgrade slots in UI
/// </summary>
public class UpgradeSlot : MonoBehaviour
{
    UIBallMenu ballMenu;
    SceneData sceneData;
    public Stat.StatType statType;
    public MPImage iconObj;
    
    public Sprite icon;
    public TMP_Text slotLabelObj;
    public string slotLabel;
    public TMP_Text currentValueObj;
    public int currentValue;
    public TMP_Text currentCostObj;
    public int currentCost;


    /// <summary>
    /// When created grab scene objects
    /// </summary>
    void Start()
    {
        ballMenu = transform.parent.parent.GetComponent<UIBallMenu>();
        sceneData = ballMenu.sceneData;
    }

    /// <summary>
    /// Public call for slot to refresh its UI to its data
    /// Data changes to be pushed to slot prior to refreshing
    /// </summary>
    public void Refresh()
    {
        iconObj.sprite = icon;
        slotLabelObj.text = slotLabel;
        currentValueObj.text = "$" + currentValue;
        currentCostObj.text = "$" + currentCost;
    }

    /// <summary>
    /// Public call to handle slot being activated and attempt to purchase upgrading it
    /// </summary>
    public void Upgrade()
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank.SubtractValue(currentCost))
        {
            sceneData.CurrentPinballs[ballMenu.activeBallIndex].Upgrade(statType);
            ballMenu.SetActiveBall(ballMenu.activeBallIndex);
        }
    }
}
