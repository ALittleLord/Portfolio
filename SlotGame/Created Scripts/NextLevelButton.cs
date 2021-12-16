using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Button Style Handler Class to load next Level
/// Set levelType to state to direct what level to load.
/// Calls to LevelLoader
/// </summary>
public class NextLevelButton : MonoBehaviour
{
    public enum NextLevelType {fight, rest};
    public NextLevelType levelType;


    void OnMouseDown()
    {
        switch(levelType)
        {
            case NextLevelType.fight:
                Fight();
                break;
            case NextLevelType.rest:
                Rest();
                break;
            default:
                break;
        }
    }

    private void Rest()
    {
        FindObjectOfType<LevelLoader>().LoadRest();
        FindObjectOfType<PlayerTurn>().EndTurn();
        Destroy(transform.parent.gameObject);
    }

    private void Fight()
    {
        FindObjectOfType<LevelLoader>().LoadFight();
        FindObjectOfType<PlayerTurn>().EndTurn();
        Destroy(transform.parent.gameObject);
    }
}
