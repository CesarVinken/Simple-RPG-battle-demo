using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerHeroHandler : EditorWindow
{
    private const int _minimumHeroes = 3;
    private const int _maximumHeroes = 10;
    private const int _scalingFactor = 100;

    private float _sliderValue = 3f;
    private Rect _sliderRect = new Rect(0, 40, 200, 30);

    public static void Open()
    {
        PlayerHeroHandler window = (PlayerHeroHandler)EditorWindow.GetWindow(typeof(PlayerHeroHandler));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Generate new heroes", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        DrawHeroSlider();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(_sliderRect.y + 10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Generate Heroes", GUILayout.Width(200)))
        {
            GenerateHeroes();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();  
    }

    private void DrawHeroSlider()
    {
        float panelWidth = position.width;
        float sliderWidth = _sliderRect.width;
        _sliderRect.x = (panelWidth - sliderWidth) / 2f;
        _sliderValue = Mathf.RoundToInt(GUI.HorizontalSlider(_sliderRect, _sliderValue, _minimumHeroes, _maximumHeroes));

        Rect labelPosition = new Rect(_sliderRect.x, _sliderRect.y - 20, 140, 20);
        GUI.Label(labelPosition, $"Heroes to spawn: {_sliderValue}");
    }

    private void GenerateHeroes()
    {
        int numberToGenerate = (int)_sliderValue;

        // remove old player data
        PlayerDataResetter.Reset();

        // load game data
        GameData gameData = DataHandler.GetInstance().LoadGameData();
        Dictionary<int, HeroBlueprint> allHeroes = gameData.Heroes;

        // generate new player data
        Dictionary<int, IHero> playerHeroes = new Dictionary<int, IHero>();

        for (int i = 0; i < numberToGenerate; i++)
        {
            IHero hero = HeroFactory.CreateRandomHero(allHeroes, playerHeroes.Keys.ToList());
            playerHeroes.Add(hero.Id, hero);
        }
        int numberOfBattles = 0;

        // save new player data
        DataHandler.GetInstance().SavePlayerData(playerHeroes, numberOfBattles);
    }
}