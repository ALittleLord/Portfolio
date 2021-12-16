using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Class for Tile Pool Scriptable obects holding predefined sets of tiles
/// </summary>
[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/TilePool", order = 1)]
public class TilePool : ScriptableObject
{
    [Searchable]
    [AssetsOnly]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    public List<Tile> deck = new List<Tile>();
}
