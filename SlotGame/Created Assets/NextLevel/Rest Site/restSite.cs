using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle restSite
/// Heals player on click
/// </summary>
public class restSite : MonoBehaviour
{
    CharacterScript player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterScript>();        
    }

    void OnMouseUp()
    {
        player.Heal(20);
        FindObjectOfType<Control>().LoadRound();
        Destroy(gameObject);
    }
}
