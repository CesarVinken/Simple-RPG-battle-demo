public class HeroBlueprint
{
    public int Id;
    public string Name;

    public SerialisableHeroBlueprint Serialise()
    {
        SerialisableHeroBlueprint serialisableHeroBlueprint = new SerialisableHeroBlueprint();
        serialisableHeroBlueprint.Id = Id;
        serialisableHeroBlueprint.Name = Name;

        return serialisableHeroBlueprint;
    }
}
