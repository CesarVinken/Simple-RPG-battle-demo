using System.Collections.Generic;
using System.Linq;

public static class HeroFactory
{
    private static readonly System.Random _random = new System.Random();

    public static IHero CreateHero(int id)
    {
        DataHandler dataHandler = ServiceLocator.Instance.Get<DataHandler>();
        Dictionary<int, HeroBlueprint> heroData = dataHandler.GameData.Heroes;

        HeroBlueprint heroBlueprint = heroData[id];
        IHero hero = new Hero(heroBlueprint);
        return hero;
    }

    public static IHero CreateRandomHero(Dictionary<int, HeroBlueprint> heroData, List<int> excludedIds)
    {
        List<int> heroIds = heroData.Keys.Where(k => !excludedIds.Contains(k)).ToList();

        int randomId = heroIds[_random.Next(heroIds.Count)];

        HeroBlueprint heroBlueprint = heroData[randomId];
        IHero hero = new Hero(heroBlueprint);

        ConsoleLog.Log(LogCategory.General, $"Created new hero {hero.Id}. {hero.Name}");
        return hero;
    }   
}
