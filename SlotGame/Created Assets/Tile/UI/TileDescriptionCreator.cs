using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to create tile description
/// </summary>
public class TileDescriptionCreator : MonoBehaviour
{
    Tile tile;
    
    /// <summary>
    /// Public call to create text
    /// </summary>
    /// <param name="_tile">Tlie to create text for</param>
    /// <returns></returns>
    public string CreateText(Tile _tile)
    {
        tile = _tile;

        string text = tile.tileName + "\n";
        if (tile.isEffect) text += AddEffect();
        if (tile.isAura) text += AddAura();

        return text;
    }


    /*
    * Effect Tip
    */
    
    /// <summary>
    /// Create effect text
    /// </summary>
    /// <returns>Effect Text</returns>
    string AddEffect()
    {
        string returnString = "";
        if (tile.isAttack) returnString += AddAttack();
        if (tile.isDefend) returnString += AddDefense();
        returnString += AddCost();


        return returnString;
    }

    /// <summary>
    /// Create text for how many times the effect is performed
    /// </summary>
    /// <returns>Multiplier text</returns>
    string AddMultiplier()
    {
        if (tile.xCost)
        {
            return " X times";
        }
        else if (tile.multiplier > 1)
        {
            return " " + tile.multiplier + " times";
        }
        return "";
    }

    /// <summary>
    /// Create Attack Text
    /// </summary>
    /// <returns>Attack text</returns>
    string AddAttack()
    {
        string returnString = "Deal " + tile.attackAmount;
        if (tile.bonusDamage > 0) returnString = returnString + " + " + tile.bonusDamage;
        returnString = returnString + " damage" + AddMultiplier();
        return returnString + "\n";
    }

    /// <summary>
    /// Create Defend Text
    /// </summary>
    /// <returns>Defend Text</returns>
    string AddDefense()
    {
        return "Add " + tile.defendAmount + " block" + AddMultiplier() + "\n";
    }

    /// <summary>
    /// Create cost text
    /// </summary>
    /// <returns>Cost text</returns>
    string AddCost()
    {
        if (tile.xCost)
        {
            return "Cost: X Energy";
        }
        return "Cost: " + tile.energyCost + " Energy \n";
    }

    /*
     * Aura Tip
     */

    /// <summary>
    /// Create Aura text
    /// </summary>
    /// <returns>Aura Text</returns>
    string AddAura()
    {
        string returnString = "";
        if (tile.boostsAttack) returnString += tile.boostsAttackAmount + " Aura to Attack \n";
        if (tile.boostsDefend) returnString += tile.boostsDefendAmount + " Aura to Defend \n";

        return returnString;
    }
}
