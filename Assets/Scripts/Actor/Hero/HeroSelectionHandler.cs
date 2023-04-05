using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionHandler
{
    private List<HeroSelectionTile> _selectedTiles = new List<HeroSelectionTile>();
    private ToBattleButton _toBattleButton;

    public HeroSelectionHandler(ToBattleButton toBattleButton)
    {
        _toBattleButton = toBattleButton;
    }

    public void HandleTileSelection(HeroSelectionTile tile)
    {
        if (_selectedTiles.Contains(tile))
        {
            tile.Deselect(this);
            return;
        }

        if (_selectedTiles.Count >= 3)
        {
            return;
        }

        tile.Select(this);
    }

    public void AddToSelectedTiles(HeroSelectionTile tile)
    {
        _selectedTiles.Add(tile);
        if (_selectedTiles.Count == 3)
        {
            _toBattleButton.Enable();
        }
    }

    public void RemoveFromSelectedTiles(HeroSelectionTile tile)
    {
        if (_selectedTiles.Count == 3)
        {
            _toBattleButton.Disable();
        }

        _selectedTiles.Remove(tile);
    }
}