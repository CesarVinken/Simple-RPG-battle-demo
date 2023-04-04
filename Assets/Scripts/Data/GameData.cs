using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public Dictionary<int, HeroBlueprint> Heroes = new Dictionary<int, HeroBlueprint>()
    {
        { 0, new HeroBlueprint(0, "Hans") },
        { 1, new HeroBlueprint(1, "Alice") },
        { 2, new HeroBlueprint(2, "David") },
    };
}

public class HeroBlueprint
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public HeroBlueprint(int id, string name)
    {
        Id = id;
        Name = name;
    }
}