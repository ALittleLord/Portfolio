using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Watcher class on Tiles to handle inputs
/// </summary>
public class TileWatcher : MonoBehaviour
{
    public Tile tile;
    public GameObject toolTipPrefab;
    TileToolTip toolTipActive;

    [SerializeField] float hoverLength;
    float timeHovered = 0;

    Control controller;

    /// <summary>
    /// Check if tiles tooltip creating a delay so it doesnt happen instantly
    /// Delay set by hover length
    /// </summary>
    public void CheckLoadingToolTip()
    {
        timeHovered += Time.deltaTime;
        if (timeHovered >= hoverLength && toolTipActive == null)
        {
            toolTipActive = Instantiate(toolTipPrefab).GetComponent<TileToolTip>();
            toolTipActive.transform.SetParent(GameObject.Find("Canvas").transform);
            toolTipActive.transform.position = gameObject.transform.position;
            toolTipActive.tile = tile;
        }
        if (toolTipActive != null && !toolTipActive.mouseOverTile)
        {
            toolTipActive.mouseOverTile = true;
        }
    }

    /// <summary>
    /// Call to set tile as no longer moused over
    /// </summary>
    public void ExitTile()
    {
        if (toolTipActive != null)
        {
            toolTipActive.mouseOverTile = false;
        }
    }

    /// <summary>
    /// Start initialization
    /// </summary>
    void Start()
    {
        controller = FindObjectOfType<Control>();
    }

    /// <summary>
    /// Select Tile on click if usable
    /// </summary>
    void OnMouseDown()
    {

        if (controller.tilesSelectable)
        {
            FindObjectOfType<InfoDisplay>().SetTileInfo(tile);
            if (tile.isEffect)
            {
                if (tile.usable)
                {
                    Debug.Log("TileWatcher Click Event");

                    controller.SelectTile(tile);
                }
            }
            else
            {
                Debug.Log("non usable tile clicked");
            }
        }
    }

    /// <summary>
    /// Run adjacency check on tile
    /// </summary>
    void OnMouseEnter()
    {
        if (controller.tilesSelectable) controller.adjCheck.RunCheck(tile);
    }

    /// <summary>
    /// Checkloading tool tip when moused over
    /// </summary>
    void OnMouseOver()
    {
        if (controller.tilesSelectable) CheckLoadingToolTip();
    }

    /// <summary>
    /// Remove tooltip
    /// </summary>
    void OnMouseExit()
    {
        ExitTile();
    }
}
