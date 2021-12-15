using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Core Pinball Class
/// Placed on pinball GameObject
/// Handles pinball collisions and active value
/// Stores reference to the pinballs dataSheet
/// </summary>
public class Pinball : MonoBehaviour
{
    public MMFeedbacks PointCollisionFeedback;

    public float ballValueCurrent = 1;

    [InlineEditor(InlineEditorModes.FullEditor)]
    public PinballData pinballData;

    /// <summary>
    /// Set paddle rotation to baseAngle as defined in editor
    /// </summary>
    void Start()
    {
        SetFromData();
    }

    /// <summary>
    /// Each time ball is enabled resets its values to its DataSheet
    /// </summary>
    void OnEnable()
    {
        SetFromData();
    }

    /// <summary>
    /// On entering 'Finish' Trigger disables pinball and cycles into inactive pool
    /// </summary>
    void OnTriggerEnter(Collider zone)
    {
        if (zone.tag == "Finish")
        {
            ClearBall();
        }
    }

    /// <summary>
    /// On Colliding with an object checks if it is a point collision
    /// If it is a point collision cashes in current value, then increases value for the next collision
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        PointCollision pointCollision = collision.transform.GetComponent<PointCollision>();
        if (pointCollision != null)
        {
            PointCollisionFeedback?.PlayFeedbacks();
            CashIn(pointCollision.pointMagnifier);
            ballValueCurrent++;
        }
    }

    /// <summary>
    /// Call when ball removed from scene (eg. upon reaching finish)
    /// Cashes in ball value and sets to inactive
    /// </summary>
    void ClearBall()
    {
        CashIn();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Cash in function for ball
    /// Optional input of magnifier on cash in
    /// Deposits value to bank and activates a point indicator of said value
    /// </summary>
    void CashIn(float magnifier = 1f)
    {
        float cashInValue = ballValueCurrent * magnifier;
        FindObjectOfType<Bank>().Deposit(cashInValue);
        GameObject indicator = FindObjectOfType<SceneData>().GetAPointIndicator();
        indicator.GetComponent<PointIndicator>().Initialize(cashInValue, transform.position.x, transform.position.y);
    }

    /// <summary>
    /// Sets pinball data to that from its data sheet
    /// Enables swapping sheets between spawns and makes sure that stats modified during ball life (collision count increasing value) does not carry through spawns
    /// </summary>
    void SetFromData()
    {
        Stat valueStat = pinballData.GetValueStat();
        ballValueCurrent = valueStat.GetValue();

        GetComponent<Rigidbody>().mass = pinballData.baseWeight;

        GetComponent<Renderer>().material = pinballData.baseMaterial;
    }
}

