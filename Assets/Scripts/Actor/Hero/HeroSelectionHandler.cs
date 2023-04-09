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

    public void AddToSelectedTiles(HeroSelectionTile tile)
    {
        _selectedTiles.Add(tile);
        if (_selectedTiles.Count == 3)
        {
            ToBattleButton button = HeroSelectionCanvasController.Instance.GetToBattleButton();
            button.Enable();
        }
    }

    public void RemoveFromSelectedTiles(HeroSelectionTile tile)
    {
        if (_selectedTiles.Count == 3)
        {
            ToBattleButton button = HeroSelectionCanvasController.Instance.GetToBattleButton();
            button.Disable();
        }

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