using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic button handler for Skip tile button in tile select screen
/// </summary>
public class TileOption_SkipButton : MonoBehaviour
{
    void OnMouseUp()
    {
        Control controller = FindObjectOfType<Control>();
        controller.LoadRound();
        Destroy(this.transform.parent.gameObject);
    }
}
