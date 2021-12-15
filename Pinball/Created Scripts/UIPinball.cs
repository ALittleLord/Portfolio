using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the pinball objects used by UI
/// </summary>
public class UIPinball : MonoBehaviour
{
    UIBallSlot slot;
    public PinballData pinballData;
    public float spinSpeed = 1f;
    public bool spinning = false;

    /// <summary>
    /// On start grabs slot info, sets data from a PinballData file, and checks if it is the currently active ball in UI
    /// </summary>
    void Start()
    {
        slot = transform.parent.parent.GetComponent<UIBallSlot>();
        pinballData = FindObjectOfType<SceneData>().CurrentPinballs[slot.RowIndex];
        slot.pinball = this;
        SetFromData();
        if (slot.RowIndex == slot.ballMenu.activeBallIndex)
        {
            slot.ballMenu.SetActiveBall(slot.RowIndex);
        }
    }

    /// <summary>
    /// Update call to rotate ball if spinning == true
    /// Ball spinning is current method to show selectedf ball in UI
    /// </summary>
    void Update()
    {
        if (spinning)
        {
           transform.Rotate(0, spinSpeed, 0); 
        }
    }

    /// <summary>
    /// Sets the UI ball material to that from its data sheet
    /// </summary>
    void SetFromData()
    {
        GetComponent<Renderer>().material = pinballData.baseMaterial;
    }

    /// <summary>
    /// public call to set this pinball as active
    /// </summary>
    public void ToggleActive(bool enabled)
    {
        spinning = enabled;
    }
}
