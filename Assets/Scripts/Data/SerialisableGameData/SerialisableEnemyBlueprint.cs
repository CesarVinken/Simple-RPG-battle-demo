using System;

[Serializable]
public class SerialisableEnemyBlueprint
{
    public int Id;
    public string Name;
    public float Health;
    public float AttackPower;

    public EnemyBlueprint Deserialise()
    {
        EnemyBlueprint enemyBlueprint = new EnemyBlueprint();
        enemyBlueprint.Id = Id;
        enemyBlueprint.Name = Name;
        enemyBlueprint.Health = Health;
        enemyBlueprint.AttackPower = AttackPower;

        return enemyBlueprint;
    }
}
