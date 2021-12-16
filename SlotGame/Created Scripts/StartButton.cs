using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Start Button Handler
/// </summary>
public class StartButton : MonoBehaviour
{ 
    public void RunStart()
    {
        SceneManager.LoadScene("CombatMainScene");
    }
}
