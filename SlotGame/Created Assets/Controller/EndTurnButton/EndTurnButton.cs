using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic handler for end turn button
/// </summary>
public class EndTurnButton : MonoBehaviour
{   

    [SerializeField] Control controller;
    void Start()
    {
        controller = FindObjectOfType<Control>();
    }
    
    void OnMouseDown()
    {
        controller.playerTurn.EndTurn();
    }
}
