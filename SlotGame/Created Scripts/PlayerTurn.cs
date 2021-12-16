using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Class to handle player turn states
/// </summary>
public class PlayerTurn : MonoBehaviour
{
    Control controller;
    SlotGrid slot;

    public bool running = false; //Whether or not it is actively the players turn
    
    /// <summary>
    /// Initialize scene object references at Start
    /// </summary>
    void Start()
    {
        controller = FindObjectOfType<Control>();
        slot = FindObjectOfType<SlotGrid>();
    }

    /// <summary>
    /// Public call to start turn
    /// </summary>
    public void RunTurn()
    {
        Debug.Log("start turn");
        running = true;
        slot.SetUp();
    }
    /// <summary>
    /// Public call to end turn 
    /// </summary>
    public void EndTurn()
    {
        running = false;
        controller.tilesSelectable = false;
        controller.DeSelectTile();
    }
}
