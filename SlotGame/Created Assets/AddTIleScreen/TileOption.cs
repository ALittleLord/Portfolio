using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle tile option in tile select screen
/// </summary>
public class TileOption : MonoBehaviour
{
    public TilePool tileOptions;
    Tile currentTile;

    Control controller;

    /// <summary>
    /// On Awake choose and set random tile
    /// </summary>
    void Awake()
    {
        controller = FindObjectOfType<Control>();
        currentTile = tileOptions.deck[Random.Range(0, tileOptions.deck.Count)];
        currentTile.Initialize(new Vector3(0, 0, 0), gameObject, "Option", currentTile, 0);
    }
    
    /// <summary>
    /// On Click Release select this tile
    /// </summary>
    private void OnMouseUp()
    {
        SelectTile();
        controller.LoadRound();
        foreach(TileToolTip toolTip in FindObjectsOfType<TileToolTip>())
        {
            Destroy(toolTip.gameObject);
        }

        Destroy(this.transform.parent.gameObject);
    }

    /// <summary>
    /// On mouse over create tile preview ui
    /// </summary>
    private void OnMouseOver()
    {
        Debug.Log("MouseOverTileOption");
        currentTile.tileObject.GetComponent<TileWatcher>().CheckLoadingToolTip();
    }

    /// <summary>
    /// Remove tile stat preview ui when mouse no longer over
    /// </summary>
    private void OnMouseExit()
    {
        currentTile.tileObject.GetComponent<TileWatcher>().ExitTile();
    }

    /// <summary>
    /// Select tile
    /// Through OnMouseUp()
    /// </summary>
    private void SelectTile()
    {
        FindObjectOfType<Deck>().baseDeck.Add(Instantiate(currentTile));
    }
}
