using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic control handling for pinball
/// Runs swinging of paddles of buttons for touch or 'A' and 'D' for keyboard
/// </summary>
public class ControlHandler : MonoBehaviour
{

    int leftCount = 0;
    int rightCount = 0;
    public List<Paddle> paddlesL;
    public List<Paddle> paddlesR;
    bool leftSwinging = false;
    bool rightSwinging = false;

    /// <summary>
    /// Collects all paddles in scene as lists of left paddles and right paddles
    /// </summary>
    void Awake()
    {
        paddlesL = GetPaddles(false);
        paddlesR = GetPaddles(true);
    }

    /// <summary>
    /// Checks for input in time with phsics system
    /// </summary>
    void FixedUpdate()
    {
        RegisterInputs();
    }

    /// <summary>
    /// Checks to see if hardcoded keys are pressed, or if touch input is held based on swinging state
    /// Called in FixedUpdate
    /// </summary>
    void RegisterInputs()
    {
        if (Input.GetKey("a"))
        {
            //Debug.Log("a");
            SwingLeft();
            leftCount++;
        }
        else leftCount = 0;

        if (Input.GetKey("d"))
        {
            //Debug.Log("d");
            SwingRight();
            rightCount++;
        }
        else rightCount = 0;

        if (rightSwinging) SwingRight();
        if (leftSwinging) SwingLeft();
    }

    /// <summary>
    /// Swings all left paddles as collected in paddlesL list
    /// </summary>
    public void SwingLeft()
    {
        //Debug.Log("Swing Left " + Time.time);
        foreach(Paddle paddle in paddlesL)
        {
            paddle.Swing();
        }
    }

    /// <summary>
    /// Swings all right paddles as collected in paddlesR list
    /// </summary>
    public void SwingRight()
    {
        //Debug.Log("Swing Right " + Time.time);
        foreach(Paddle paddle in paddlesR)
        {
            paddle.Swing();
        }
    }

    /// <summary>
    /// Collects all paddles either left or right adding them to a new list
    /// Call is independent to one side as defined by isRight bool
    /// Collects right if isRight == true; Collects left if isRight == false
    /// Returns a created List<Paddle> of paddles on side
    /// </summary>
    List<Paddle> GetPaddles(bool isRight)
    {
        List<Paddle> paddleReturn = new List<Paddle>();
        foreach (Paddle paddle in FindObjectsOfType<Paddle>())
        {
            if (isRight == paddle.isRight)
            {
                paddleReturn.Add(paddle);
            }
        }
        return paddleReturn;
    }


    /// <summary>
    /// Updated touch input set up
    /// On touch sets swinging to true per side
    /// Sets back to false when released
    /// Called from event trigger buttons
    /// </summary>

    /// <summary>
    /// Sets left paddles to swing
    /// </summary>
    public void StartSwingLeft()
    {
        leftSwinging = true;
    }

/// <summary>
    /// Sets left paddles to stop swinging
    /// </summary>
    public void StopSwingLeft()
    {
        leftSwinging = false;
    }

    /// <summary>
    /// Sets right paddles to swing
    /// </summary>
    public void StartSwingRight()
    {
        rightSwinging = true;
    }

    /// <summary>
    /// Sets right paddles to stop swinging
    /// </summary>
    public void StopSwingRight()
    {
        rightSwinging = false;
    }
}
