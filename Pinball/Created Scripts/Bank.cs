using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Basic bank setup to handle player wealth
/// </summary>
public class Bank : MonoBehaviour
{
    public TMP_Text BalanceDisplay;
    public float _CurrentValue = 0;

    /// <summary>
    /// On start sets UI
    /// </summary>
    void Start()
    {
        UpdateBalanceDisplay();
    }

    /// <summary>
    /// Public call to add float to balance
    /// Ensures positive value Deposited, otherwise runs as a subtract value
    /// returns true if transaction is succesful, false otherwise
    /// </summary>
    public bool Deposit(float amount)
    {
        if (amount >= 0)
        {
            return AddValue(amount);
        }
        else
        {
            return SubtractValue(-amount);
        }
    }

    /// <summary>
    /// Private call to add value to balance and update UI
    /// </summary>
    private bool AddValue(float amount)
    {
        _CurrentValue += amount;
        UpdateBalanceDisplay();
        return true;
    }

    /// <summary>
    /// Call to remove float from balance, with a check that the balance contains the amount wanting to be withdrawn
    /// If there is an adequate balance removes amount, updates UI, and returns true
    /// If the balance is not great enough does not run transaction and returns false
    /// </summary>
    public bool SubtractValue(float amount)
    {
        if (amount <= _CurrentValue)
        {
            _CurrentValue -= amount;
            UpdateBalanceDisplay();
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Call to update display of current Balance
    /// To be called whenever balance is modified
    /// </summary>
    void UpdateBalanceDisplay()
    {
        if(BalanceDisplay != null)
        {
            BalanceDisplay.text = "$"+ _CurrentValue;
        }
    }
}
