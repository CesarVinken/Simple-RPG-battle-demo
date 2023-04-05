using UnityEditor;
using UnityEngine;

public class EditorExtensions : Editor
{
    [MenuItem("Data/Reset Player Data")]
    public static void ResetPlayerData()
    {
        PlayerDataResetter.Reset();
    }

    [MenuItem("Data/Set Player Heroes")]
    public static void SetPlayerHeroes()
    {
        PlayerHeroHandler.Open();
    }
}
