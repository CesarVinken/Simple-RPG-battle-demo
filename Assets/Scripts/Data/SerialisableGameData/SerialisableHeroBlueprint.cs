using System;
using UnityEngine;

[Serializable]
public class SerialisableHeroBlueprint
{
    public int Id;
    public string Name;

    public HeroBlueprint Deserialise()
    {
        HeroBlueprint heroBlueprint = new HeroBlueprint();
        heroBlueprint.Id = Id;
        heroBlueprint.Name = Name;

        return heroBlueprint;
    }
}
