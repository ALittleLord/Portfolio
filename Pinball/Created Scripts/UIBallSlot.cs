using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles individual BallSlots in BallMenu
/// </summary>
public class UIBallSlot : MonoBehaviour
{
    public UIBallMenu ballMenu;
    public UIPinball pinball;
    public RenderTexture rt;
    public Camera renderCamera;
    public int RowIndex;
    
    /// <summary>
    /// On Awake grab relevent scene objects, and sets up the renderer for the slot
    /// </summary>
    void Awake()
    {
        ballMenu = FindObjectOfType<UIBallMenu>();
        SetUpRender();
    }

    /// <summary>
    /// Creates a renderer to allow displaying a 3D Pinball in the UI
    /// </summary>
    void SetUpRender()
    {
        rt = new RenderTexture(256, 256, 16);
        rt.Create();
        renderCamera.targetTexture = rt;
        GetComponent<RawImage>().texture = rt;
    }

    /// <summary>
    /// Call from button on slot to set slot as active when pressed
    /// </summary>
    public void HandleClick()
    {
        ballMenu.SetActiveBall(RowIndex);
    }
}
