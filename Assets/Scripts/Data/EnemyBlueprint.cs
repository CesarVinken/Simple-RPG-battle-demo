using UnityEngine;

public class EnemyBlueprint
{
    public int Id;
    public string Name;
    public float Health;
    public float AttackPower;

    public SerialisableEnemyBlueprint Serialise()
    {
        SerialisableEnemyBlueprint serialisableEnemyBlueprint = new SerialisableEnemyBlueprint();
        serialisableEnemyBlueprint.Id = Id;
        serialisableEnemyBlueprint.Name = Name;
        serialisableEnemyBlueprint.Health = Health;
        serialisableEnemyBlueprint.AttackPower = AttackPower;

        return serialisableEnemyBlueprint;
    }
}
