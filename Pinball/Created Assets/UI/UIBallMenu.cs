using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Ball Upgrade Menu UI
/// </summary>
public class UIBallMenu : MonoBehaviour
{
    public SceneData sceneData;
    public UIUpgradeWindow upgradeWindow;
    public GameObject BallSlotPrefab;
    public List<Transform> BallSlots = new List<Transform>();
    Transform ballRow;
    UIPinball activeBall;
    public int activeBallIndex = 0;
    [SerializeField] float ballRowWidth;
    [SerializeField] int ballSlotCount = 1;

    /// <summary>
    /// When enabled makes sure grabbed scene objects are up to date and refreshes slots
    /// </summary>
    void OnEnable()
    {
        sceneData = FindObjectOfType<SceneData>();
        ballRow = transform.Find("Ball Row");
        RefreshBallSlots();
    }

    /// <summary>
    /// Spawn and place n ballSlots as defined by ballSlotCount
    /// </summary>
    void SpawnBallSlots()
    {
        ballSlotCount = sceneData.CurrentPinballs.Count;
        Transform newBallSlot;
        float ballX;
        for (int i = 0; i < ballSlotCount; i++)
        {
            newBallSlot = Instantiate(BallSlotPrefab).transform;
            newBallSlot.SetParent(ballRow);
            newBallSlot.GetComponent<UIBallSlot>().RowIndex = i;
            ballX = (-ballRowWidth/2) + ((i+1)*ballRowWidth/(ballSlotCount+1));
            newBallSlot.localPosition = new Vector3(ballX,0,0);
            BallSlots.Add(newBallSlot);
        }
    }

    /// <summary>
    /// Public call to toggle menu active state
    /// </summary>
    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = gameObject.activeSelf ? 0 : 1;
    }

    /// <summary>
    /// Deletes all slots in BallSlots list from scene
    /// Sets BallSlots as a new list
    /// </summary>
    void ClearBallSlots()
    {
        foreach(Transform slot in BallSlots)
        {
            Destroy(slot.gameObject);
        }
        BallSlots = new List<Transform>();
    }

    /// <summary>
    /// Set active ball from its index in BallSlots
    /// Sets the previous active ball to inactive
    /// </summary>
    public void SetActiveBall(int index)
    {
        PinballData data;
        //if (index != activeBallIndex)
        {
            activeBallIndex = index;
            if (activeBall != null) activeBall.ToggleActive(false);
            activeBall = BallSlots[index].GetComponent<UIBallSlot>().pinball;
            activeBall.ToggleActive(true);
            data = sceneData.CurrentPinballs[activeBallIndex];
            upgradeWindow.SetUpgrades(data);
        }
    }

    
    /// <summary>
    /// Clears and refills BallSlots
    /// </summary>
    void RefreshBallSlots()
    {
        ClearBallSlots();
        SpawnBallSlots();
    }
}
