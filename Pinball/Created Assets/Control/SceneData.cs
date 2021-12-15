using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Handles miscelanious data related to the scene
/// Core handler for the players list of pinballs (data prefabs not spawned scene objects)
/// Handles collision point indicator objects
/// </summary>
public class SceneData : MonoBehaviour
{
    public GameObject PointIndicatorPrefab;
    public List<GameObject> InactivePointIndicators = new List<GameObject>();
    [InlineEditor(InlineEditorModes.FullEditor)]
    public List<PinballData> CurrentPinballs = new List<PinballData>();

    /// <summary>
    /// On Awake run call to check if ball levels need to be reset
    /// </summary>
    void Awake()
    {
        ResetBallLevels();
    }

    /// <summary>
    /// Public call to get a point indicator
    /// Tries to pull from pool of inactive indicators removing it from pool if it does
    /// If inactive pool is empty only then will create a new indicator
    /// Designed to minimize instantioation of indicators, and to store only as many indicators as have been required at scene peak
    /// Returns indicator gameObject
    /// </summary>
    public GameObject GetAPointIndicator()
    {
        GameObject returnIndicator;
        if (InactivePointIndicators.Count != 0)
        {
            returnIndicator = InactivePointIndicators[0];
            InactivePointIndicators.RemoveAt(0);
        }
        else
        {
            returnIndicator = CreateNewIndicator();
        }

        return returnIndicator;
    }

    /// <summary>
    /// Public call to add an indicator to the inactive pool inputed as gameObject
    /// Called from the indicator itself in its OnDisable
    /// </summary>
    public void AddDisabledIndicatorToPool(GameObject indicator)
    {
        InactivePointIndicators.Add(indicator);
    }
    
    /// <summary>
    /// Call to create a new indicator
    /// Returns GameObject of instantiated indicator
    /// </summary>
    private GameObject CreateNewIndicator()
    {
        return Instantiate(PointIndicatorPrefab);
    }

    /// <summary>
    /// Call to reset ball levels if ball data is set to not store data, checked per ball
    /// Important in testing as ball data being stored as scriptableObjects leads to level ups lasting through tests
    /// </summary>
    void ResetBallLevels()
    {
        foreach (PinballData data in CurrentPinballs)
        {
            if (!data.storeData)
            {
                data.ResetData();
            }
        }
    }
}
