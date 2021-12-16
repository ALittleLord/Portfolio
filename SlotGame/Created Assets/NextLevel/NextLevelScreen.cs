using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class to handle next level screen setup
/// Call SetLevels() to refresh, currently private
/// </summary>
public class NextLevelScreen : MonoBehaviour
{
    EnemyLoader loader;
    /// <summary>
    /// Run Setup functions at start
    /// </summary>
    private void Start()
    {
        loader = FindObjectOfType<EnemyLoader>();
        SetLevels();
    }

    /// <summary>
    /// Primary class function
    /// Sets up two buttons to fight and rest states
    /// </summary>
    //  Note: Move button input into seperate function and make a for each on buttons to expand when needed
    private void SetLevels()
    {
        NextLevelButton[] buttons = GetComponentsInChildren<NextLevelButton>();
        buttons[0].levelType = NextLevelButton.NextLevelType.fight;
        buttons[0].GetComponentInChildren<TextMeshPro>().text = "FIGHT";

        buttons[1].levelType = NextLevelButton.NextLevelType.rest;
        buttons[1].GetComponentInChildren<TextMeshPro>().text = "REST";
    }

    /// <summary>
    /// Get the text of enemies in a level
    /// </summary>
    /// <param name="level">Level to check for enemies in</param>
    /// <returns>Level Title based on enemies as string</returns>
    private string SetText(EnemyPool level)
    {
        string newText = "";
        foreach (GameObject enemy in level.pool)
        {
            newText = newText + enemy.name + "\n";
        }
        return newText;
    }

}
