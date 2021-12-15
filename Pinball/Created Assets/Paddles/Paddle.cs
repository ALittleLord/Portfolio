using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

/// <summary>
/// Paddle class placed on paddle game objects
/// </summary>
public class Paddle : MonoBehaviour
{
    public MMFeedbacks SwingFeedback;
    public bool isRight = true;

    public float speed = 1;

    public float baseAngle;
    
    public float cockAmount = 6;

    public float swingAmount = 20;

    /// <summary>
    /// Set to angle on awake
    /// </summary>
    void Awake()
    {
        SetBaseAngle();
    }

    /// <summary>
    /// Set paddle rotation to baseAngle as defined in editor
    /// </summary>
    void SetBaseAngle()
    {
        baseAngle = transform.localRotation.eulerAngles.z;
    }

    /// <summary>
    /// Public call to swinf paddle
    /// Runs swing through use of an MMFeedback
    /// </summary>
    public void Swing()
    {
        SwingFeedback?.PlayFeedbacks();
    }
}
