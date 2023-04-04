using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class HeroFactory
{
    private static readonly System.Random _random = new System.Random();

    public static IHero CreateRandomHero()
    {
        Dictionary<int, HeroBlueprint> heroData = GameManager.Instance.GameData.Heroes;
        List<int> excludedIds = GameManager.Instance.GetHeroes().Keys.ToList();
        List<int> heroIds = heroData.Keys.Where(k => !excludedIds.Contains(k)).ToList();

        //_random = new System.Random();
        int randomId = heroIds[_random.Next(heroIds.Count)];

        HeroBlueprint heroBlueprint = heroData[randomId];
        IHero hero = new Hero(heroBlueprint);

        ConsoleLog.Log(LogCategory.General, $"Created new hero {hero.Id}. {hero.Name}");
        return hero;
    }   
}