using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Logic class for the slot machine to check tile adjacency based on an input tile
///  Currently still in early prototyping stages
///  Only Check Surrounding is running as an early way to make sure systems in place work
///  Adjacency/Aura system is being reworked heavily, new functions being put in are largely running line counts to do in a row bonuses
/// </summary>
//  TO DO:
//    -Make static class
//    -Remove currentTile and set as a passthrough tile to functions
//    -Finish setting up directional checks and run inner loops to exterior functions to make reworking models easier
public class AdjacencyCheck : MonoBehaviour
{
    Tile currentTile;
    List<SlotColumn> columnList;
    SlotGrid slotGrid;

    int inARow = 0;
    List<GameObject> linedUpTiles;

    /// <summary>
    /// Public call to use this class
    /// Currently only running CheckSurrounding, not orthogonal or diagonal
    /// Input: Tile to check adjacency around
    /// </summary>
    /// <param name="tile">Active Tile to check around</param>
    public void RunCheck(Tile tile)
    {
        if (currentTile != null) currentTile.ResetBonuses();
        currentTile = tile;
        currentTile.ResetBonuses();
        columnList = FindObjectOfType<SlotGrid>().columnList;

        CheckSurrounding();
    }

    /// <summary>
    /// Set up scene object references at Start
    /// </summary>
    void Start()
    {
        slotGrid = FindObjectOfType<SlotGrid>();
    }
    

    /// <summary>
    /// Prototype Adjacency call to check if any tiles surrounding currentTile are are Aura Tiles, and if so then applying their effects
    /// </summary>
    void CheckSurrounding()
    {
        for (int x = currentTile.column-1; x <= currentTile.column + 1; x++)
        {
           
            if (x >= 0 && x < slotGrid.columns)
            {

                for (int y = currentTile.row - 1; y <= currentTile.row + 1; y++)
                {
                    if (y > 0 && y < columnList[x].maxTiles)
                    {

                        Debug.Log("Attempting to apply aura to y = " + y + " | x = " + x);
                        //Start of inside of loop ---------> Probably move to own function
                        if (columnList[x].tiles[y].isAura)
                        {
                            currentTile.ApplyAura(columnList[x].tiles[y]);
                        }

                    }
                }
            }
        }
    }


    //                   //
    // Under Development
    //                   //

    /// <summary>
    /// Call Vertical and Horizontal checks
    /// </summary>
    void CheckOrthogonal()
    {
        CheckVertical();
        CheckHorizontal();
    }

    /// <summary>
    /// Check vertical relative to currentTile
    /// Sets how many tiles are in column
    /// </summary>
    void CheckVertical()
    {
        int column = currentTile.column;
        int inColumn = 0;
        for (int i = 1; i < columnList[column].maxTiles; i++)
        {
            if (i != currentTile.row)
            {
                if (columnList[column].tiles[currentTile.row].tileSprite == currentTile.tileSprite)
                {
                    inColumn++;
                }
            }
        }
     }

    void CheckHorizontal()
    {
        //TO DO
    }       

    void CheckDiagonal()
    {
        //TO DO
    }

}
