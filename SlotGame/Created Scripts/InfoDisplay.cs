using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class to set tile info in UI
/// </summary>
public class InfoDisplay : MonoBehaviour
{
    TextMeshPro InfoText;
    Tile tile;
    TileDescriptionCreator textMaker;

    /// <summary>
    /// Public call to set info
    /// </summary>
    /// <param name="setTile">Tile to set from</param>
    public void SetTileInfo(Tile setTile)
    {
        textMaker = FindObjectOfType<TileDescriptionCreator>();
        tile = setTile;

        InfoText.text = textMaker.CreateText(tile);
    }

    void Start()
    {
        InfoText = transform.GetComponentInChildren<TextMeshPro>();
    }


}
