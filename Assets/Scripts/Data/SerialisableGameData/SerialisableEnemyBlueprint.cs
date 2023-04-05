using System;

[Serializable]
public class SerialisableEnemyBlueprint
{
    public int Id;
    public string Name;

    public EnemyBlueprint Deserialise()
    {
        EnemyBlueprint enemyBlueprint = new EnemyBlueprint();
        enemyBlueprint.Id = Id;
        enemyBlueprint.Name = Name;

        return enemyBlueprint;
    }
}
