using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles individual column in slot machine
/// Handles spinning and tile cycling
/// </summary>
[CreateAssetMenu(fileName = "Column", menuName = "ScriptableObjects/Column", order = 1)]
public class SlotColumn : ScriptableObject
{
    public int index;
    int tileIndex;
    public GameObject gridObject;
    public bool spinning = false;
    public float baseSpinSpeed = 5;
    public float spinSpeed = 1;
    public List<Tile> tiles = new List<Tile>();
    public Deck deck;
    public Vector3 basePos = new Vector3(0,0,0);
    public int maxTiles = 5;

    [SerializeField] float slowMin = 1f;
    [SerializeField] float slowMax = 5f;
    [SerializeField] float minSpeed = 0.3f;
    [SerializeField] float speedDividend = 1000f;
    /// <summary>
    /// Public call to set initial tile positions filling column
    /// </summary>
    public void InitializeColumn()
    {
        deck = FindObjectOfType<Deck>();

        tileIndex = 0;
        string name;
        for(int i = 0; i < maxTiles; i++)
        {
            name = ("tile: " + index + ", " + tileIndex);
            CreateTile(name);
        }
        AlignColumn();
    }

    /// <summary>
    /// public call to clear column/ discard tiles
    /// to be run prior to column being deleted
    /// </summary>
    public void ClearColumn()
    {
        foreach (Tile tile in tiles)
        {
            deck.Discard(tile);
        }
        tiles = new List<Tile>();
    }

    /// <summary>
    /// public call to start spinning column
    /// sets initialization
    /// </summary>
    public void SetSpinning()
    {
        spinning = true;
        spinSpeed = baseSpinSpeed;
    }

    /// <summary>
    /// public call to run column to be called when slot is spinning
    /// runs movement and adjustment on speed
    /// </summary>
    public void RunColumn()
    {
        MoveTiles();
        SlowColumn();
        StoppedCheck();
    }

    /// <summary>
    /// Creates a new Tile, retrieved from tilePool, at the start of the list
    /// </summary>
    /// <param name="name">Tile name as string</param>
    void CreateTile(string name)
    {
        Tile rootObject = deck.Draw();
        tiles.Insert(0, Instantiate(rootObject));
        tiles[0].Initialize(basePos, gridObject, name, rootObject, index);
        tiles[0].ToggleUsable(true);
    }    

    /// <summary>
    /// removes the tile at index from list and runs call to disable it properly
    /// </summary>
    /// <param name="index">Index of tile to be removed</param>
    void RemoveTile(int index)
    {
        deck.Discard(tiles[index]);
        tiles.RemoveAt(index);
    }

    /// <summary>
    /// aligns all of the tiles to their int position in the column
    /// </summary>
    void AlignColumn()
    {
        //Debug.Log("Aligning index: " + index);
        for(int i=0; i < maxTiles; i++)
        {
            
            Vector3 newPos = basePos - new Vector3(0f, i, 0f);
            //Debug.Log(i + ": moving to " + newPos);
            tiles[i].tileObject.transform.localPosition = newPos;
            //Debug.Log(i + ": is now at " + tiles[i].tileObject.transform.localPosition);
            tiles[i].row = i;
        }
    }

    /// <summary>
    /// Removes the last tile and adds a new one to start, aligns all tiles to the new positions
    /// </summary>
    void CycleTiles()
    {
        RemoveTile(tiles.Count - 1);
        string name = "tile: " + index + ", " + tileIndex;
        CreateTile(name);
    }

    /// <summary>
    /// moves all tiles based on delta time and movement 
    /// </summary>
    void MoveTiles()
    {
        float distance = Time.deltaTime * spinSpeed;
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].tileObject.transform.Translate(0, -distance, 0);
            i = EndCollisionCheck(i);
        }
    }

    /// <summary>
    /// Checks to see if tiles[i] has reached the end
    /// If it has, runs cycle procedure
    /// returns i to counter, reduced to -1 if looped
    /// </summary>
    /// <param name="i">Tile index to check</param>
    /// <returns></returns>
    int EndCollisionCheck(int i)
    {
        if (tiles[i].tileObject.transform.localPosition.y <= basePos.y - maxTiles)
        {
            //Debug.Log("Cycling Tiles in column: " + index);
            CycleTiles();
            AlignColumn();
            //Makes the loop restart after being aligned
            i = -1;
        }
        return i;
    }

    /// <summary>
    /// Slow column prior to it stopping
    /// </summary>
    void SlowColumn()
    {
        spinSpeed -= (Random.Range(slowMin, slowMax))/((index+1)*(speedDividend));
    }

    /// <summary>
    /// Stops column when speed drops below minimum
    /// </summary>
    void StoppedCheck()
    {
        if (spinSpeed <= minSpeed)
        {
            StopColumn();
        }
    }

    /// <summary>
    /// Stops and aligns column
    /// </summary>
    void StopColumn()
    {
        //spinning = false;
        AlignColumn();
    }
}
