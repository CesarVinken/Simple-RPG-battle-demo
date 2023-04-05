using UnityEngine;

public class EnemyBlueprint
{
    public int Id;
    public string Name;

    public SerialisableEnemyBlueprint Serialise()
    {
        SerialisableEnemyBlueprint serialisableEnemyBlueprint = new SerialisableEnemyBlueprint();
        serialisableEnemyBlueprint.Id = Id;
        serialisableEnemyBlueprint.Name = Name;

        return serialisableEnemyBlueprint;
    }
}
