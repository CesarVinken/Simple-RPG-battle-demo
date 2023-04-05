using System.Collections.Generic;
using UnityEngine;
public interface ICanvasController
{
    void AddTile(IHeroTile tile);
    void OnClickHero(IHeroTile tile);
}

public class BattleCanvasController : MonoBehaviour, ICanvasController
{
    public static BattleCanvasController Instance;

    [SerializeField] private ToHeroSelectionButton _toHeroSelectionButton;

    private List<IHeroTile> _tiles = new List<IHeroTile>();


    public void Awake()
    {
        if (_toHeroSelectionButton == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find _toHeroSelectionButton");
        }

        Instance = this;
    }

    private void Start()
    {
        // We came back from the hero selection scene
        if (GameManager.Instance.PreviousScene != SceneType.None)
        {
            Setup();
            Initialise();
        }
    }
    public void Setup()
    {
        _toHeroSelectionButton.Setup();
    }

    // We want initialisation to take place after we have loaded in game data
    public void Initialise()
    {
        _toHeroSelectionButton.Initialise();
    }

    public void AddTile(IHeroTile tile)
    {
        _tiles.Add(tile);
    }

    public void OnClickHero(IHeroTile tile)
    {
        ConsoleLog.Log(LogCategory.General, $"attack");
    }
}
