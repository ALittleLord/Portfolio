using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileToolTip : MonoBehaviour
{
    public bool mouseOverTile = false;
    public bool mouseOverToolTip = false;
    public Tile tile;
    TileDescriptionCreator textMaker;

    void Start()
    {
        textMaker = FindObjectOfType<TileDescriptionCreator>();
        SetTip();
    }

    void Update()
    {
        if (!mouseOverTile && !mouseOverToolTip)
        {
            Destroy(this.gameObject);
        }
    }

    void OnMouseEnter()
    {
        mouseOverToolTip = true;
    }

    void OnMouseExit()
    {
        mouseOverToolTip = false;
    }

    void SetTip()
    {
        TextMeshProUGUI textObj = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        textObj.text = textMaker.CreateText(tile);
    }
}
