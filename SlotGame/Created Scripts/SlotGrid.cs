using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slot Grid Class
/// Holds Columns and handles spinning states of each
/// </summary>
public class SlotGrid : MonoBehaviour
{
    public int columns = 4;
    public SlotArm slotArm;
    public SlotColumn slotColumn;

    public List<SlotColumn> columnList = new List<SlotColumn>();
    Control controller;

    /// <summary> 
    /// public call to run slot machine/start columns spinning
    /// </summary>
    public void RunSlot()
    {
        controller.tilesSelectable = true;
        foreach(SlotColumn column in columnList)
        {
            column.SetSpinning();
        }
    }

    /// <summary>
    /// public call to fill grid with tiles and prime arm to be pulled
    /// </summary>
    public void SetUp()
    {
        Debug.Log("Setting Up Grid");
        InitializeColumnList();
        foreach (SlotColumn column in columnList)
        {
            column.ClearColumn();
            column.InitializeColumn();
        }
        slotArm.PrimeArm();
    }

    void Start()
    {
        controller = FindObjectOfType<Control>();
        InitializeColumnList();
    }

    void Update()
    {
        CheckSpinning();
    }

    /// <summary>
    /// Called Every Update
    /// Checks each column for if it is spinning. If so runs move column.
    /// </summary>
    void CheckSpinning()
    {
        foreach (SlotColumn column in columnList)
        {
            if (column.spinning)
            {
                column.RunColumn();
            }
        }
    }


    /// <summary>
    /// WARNING CLEARS CURRENT COLUMNS
    /// creates columns for 
    /// </summary>
    void InitializeColumnList()
    {
        ClearColumnList();
        for (int i=0; i<columns; i++)
        {
            AddColumn();
        }
    }

    /// <summary>
    /// empties column list, first deleting all tiles
    /// </summary>
    void ClearColumnList()
    {
        foreach(SlotColumn column in columnList)
        {
            column.ClearColumn();
        }
        columnList = new List<SlotColumn>();
    }

    /// <summary>
    /// Adds a column to the end of the list
    /// runs initialization on new column
    /// </summary>
    void AddColumn()
    {
        int index = columnList.Count;
        columnList.Add(Instantiate(slotColumn));
        columnList[index].index = index;
        columnList[index].basePos = new Vector3(index - (columns / 2f) + 0.5f, 0f, 0f);
        columnList[index].gridObject = this.gameObject;
        columnList[index].InitializeColumn();
    }
}
