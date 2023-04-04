using UnityEngine;

public class BattleCanvasController : MonoBehaviour
{
    public static BattleCanvasController Instance;

    [SerializeField] private ToHeroSelectionButton _toHeroSelectionButton;

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
        // We came back from the battle scene
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

}
