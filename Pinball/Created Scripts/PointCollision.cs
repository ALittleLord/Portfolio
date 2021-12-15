using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

/// <summary>
/// Collision handler to be placed on scene objects that are designed to earn points
/// On Hit Runs MMFeedback attached, and adds to any pinball that hit it a force pushing it away in the vector direction between the center of the two objects
/// </summary>
public class PointCollision : MonoBehaviour
{
    public float pointMagnifier = 1;
    public bool cashIn = true;  
    public MMFeedbacks HitFeedback;
    public float reboundForce = 0;
    public float yReboundDividend = 3;

    /// <summary>
    /// On collision runs feedback and applies force to colliding pinball
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        HitFeedback?.PlayFeedbacks();
        Transform collisionTransform = collision.transform;
        if (collisionTransform.GetComponent<Pinball>() != null)
        {
            Vector3 collisionDirection = Vector3.Normalize(collisionTransform.position - transform.position);
            Vector3 returnForce = new Vector3(collisionDirection.x, collisionDirection.y*(1+(collisionDirection.y/yReboundDividend)), 0);
            collisionTransform.GetComponent<Rigidbody>().AddForce(returnForce * reboundForce);
        }
    }
}
