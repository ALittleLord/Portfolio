using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle deck
/// Stores three Lists. 
///     - The Base deck which stands outside of current round
///     - The Draw pile which is loaded from base deck at the start of the round, and reshuffled into from the discard pile when empty
///     - The Discard pile which is used as the secondary holder in round to cycle discarding and reshuffling
/// </summary>
public class Deck : MonoBehaviour
{
    //Decks
    public List<Tile> baseDeck = new List<Tile>();
    public List<Tile> drawPile = new List<Tile>();
    public List<Tile> discardPile = new List<Tile>();


    public Tile blankTile;
    public int minDeckSize = 25;

    /// <summary>
    /// Public call to initialize deck
    /// Resets baseDeck from controller Deck
    /// Sets Up Slot Machine
    /// </summary>
    public void Initialize()
    {
        baseDeck = new List<Tile>();
        Control controller = GetComponent<Control>();
        foreach (Tile tile in controller.tilePoolPrefab.deck)
        {
            baseDeck.Add(tile);
        }
        SetUp();
    }

    /// <summary>
    /// Call to draw a tile from the deck
    /// </summary>
    /// <returns>The tile on top of deck, null if deck is empty</returns>
    public Tile Draw()
    {
        if (drawPile.Count == 0) DiscardToDraw();

        if (drawPile.Count > 0)
        {
            Tile returnTile = drawPile[0];
            drawPile.RemoveAt(0);

            return returnTile;
        }

        return null;
    }

    /// <summary>
    /// Call to discard input tile
    /// Destroys Tile object through Tile and adds to discard pile
    /// </summary>
    /// <param name="tile">Tile to discard</param>
    public void Discard(Tile tile)
    {
        tile.DestroyObject();
        discardPile.Add(tile);
    }

    /// <summary>
    /// Call to copy tiles from base deck into draw pile and fill draw pile with blanks up to minimum tile count
    /// </summary>
    public void SetUp()
    {
        foreach (SlotColumn col in FindObjectOfType<SlotGrid>().columnList)
        {
            col.spinning = false;
            col.ClearColumn();
        }

        drawPile = new List<Tile>();
        discardPile = new List<Tile>();

        foreach (Tile tile in baseDeck)
        {
            drawPile.Add(tile);
        }
        while (drawPile.Count < minDeckSize)
        {
            drawPile.Add(Instantiate(blankTile));
        }

        Shuffle();
    }

    /// <summary>
    /// Shuffle discard pile back into draw pile
    /// </summary>
    void DiscardToDraw()
    {
        foreach (Tile tile in discardPile)
        {
            drawPile.Add(tile);
        }
        discardPile = new List<Tile>();
        Shuffle();
    }

    /// <summary>
    /// Shuffles the drawPile
    /// </summary>
    void Shuffle()
    {
        for (int i = 0; i < drawPile.Count; i++)
        {
            Tile temp = drawPile[i];
            int randomIndex = Random.Range(i, drawPile.Count);
            drawPile[i] = drawPile[randomIndex];
            drawPile[randomIndex] = temp;
        }
    }
}
