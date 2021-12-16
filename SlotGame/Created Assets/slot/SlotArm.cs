using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles slot machine arm
/// Runs arm movement, state, and running slot on pull
/// </summary>
public class SlotArm : MonoBehaviour
{
    public SlotGrid slot;

    [SerializeField] bool armPrimed = false;
    [SerializeField] Material materialActive;
    [SerializeField] Material materialInActive;

    [Range(-1, 1)] int rotating = 0; //I/O + direction
    [SerializeField] float rotationSpeed = 1;
    float distanceRotated = 0;
    float maxRotation = 90;

    /// <summary>
    /// If rotating move on update
    /// </summary>
    void Update()
    {
        if (rotating != 0)
        {
            RotateArm(rotating * rotationSpeed);
        }
    }

    /// <summary>
    /// On click pull if primed
    /// </summary>
    void OnMouseUp()
    {
        if (armPrimed)
        {
            DePrimeArm();
            PullArm();
        }
    }

    /// <summary>
    /// Public call to mark arm as ready to be pulled
    /// </summary>
    public void PrimeArm()
    {
        armPrimed = true;
        GetComponentInChildren<MeshRenderer>().material = materialActive;
    }


   /// <summary>
   /// Public call to mark arm as no longer ready to be pulled
   /// </summary>
    public void DePrimeArm()
    {
        armPrimed = false;
        GetComponentInChildren<MeshRenderer>().material = materialInActive;
    }

    /// <summary>
    /// Queue arm moving
    /// </summary>
    void PullArm()
    {
        rotating = -1;
    }

    /// <summary>
    /// When arm hits bottom change movement direction and run slot
    /// </summary>
    void BottomArmOut()
    {
        rotating = 1;
        RunSlot();
    }

    /// <summary>
    /// Sends call to run slot machine
    /// </summary>
    void RunSlot()
    {
        slot.RunSlot();
    }

    /// <summary>
    /// Stop arm movement
    /// </summary>
    void StopArm()
    {
        rotating = 0;
    }

    /// <summary>
    /// Handles rotation animation
    /// </summary>
    /// <param name="amount">Amount to rotate arm by</param>
    void RotateArm(float amount)
    {
        distanceRotated -= amount;
        if(distanceRotated >= maxRotation)
        {
            amount += distanceRotated-maxRotation;
            distanceRotated = maxRotation;
            BottomArmOut();
        }
        else if(distanceRotated <= 0)
        {
            amount -= distanceRotated;
            distanceRotated = 0;
            StopArm();
        }
        transform.Rotate(amount, 0f, 0f);
    }
}