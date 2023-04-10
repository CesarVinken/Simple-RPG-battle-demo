using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionHandler : IGameService
{
    private List<HeroSelectionTile> _selectedTiles = new List<HeroSelectionTile>();

    public void HandleTileSelection(HeroSelectionTile tile)
    {
        if (_selectedTiles.Contains(tile))
        {
            tile.Deselect();
            return;
        }

        if (_selectedTiles.Count >= 3)
        {
            return;
        }

        tile.Select();
    }

    public void HandleToBattleButton(ToBattleButton toBattleButton)
    {
        if (_selectedTiles.Count == 3)
        {
            toBattleButton.Enable();
        }
        else if (toBattleButton.IsEnabled)
        {
            toBattleButton.Disable();
        }
    }

    public void AddToSelectedTiles(HeroSelectionTile tile)
    {
        _selectedTiles.Add(tile);
    }

    public void RemoveFromSelectedTiles(HeroSelectionTile tile)
    {
        _selectedTiles.Remove(tile);
    }

    public List<IHero> GetSelectedHeros()
    {
        List<IHero> selectedHeroes = new List<IHero>();

        for (int i = 0; i < _selectedTiles.Count; i++)
        {
            selectedHeroes.Add(_selectedTiles[i].Hero);
        }
        return selectedHeroes;
    }
}