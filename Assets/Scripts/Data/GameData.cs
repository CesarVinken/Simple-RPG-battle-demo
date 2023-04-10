using System.Collections.Generic;

/// <summary>
/// Game data is all the data that never changes during the game. It includes blueprints of all heroes and enemies.
/// </summary>
public class GameData
{
    public Dictionary<int, HeroBlueprint> Heroes = new Dictionary<int, HeroBlueprint>();
    public Dictionary<int, EnemyBlueprint> Enemies = new Dictionary<int, EnemyBlueprint>();

    public GameData WithHeroes(Dictionary<int, HeroBlueprint> heroes)
    {
        Heroes = heroes;

        return this;
    }

    public GameData WithEnemies(Dictionary<int, EnemyBlueprint> enemies)
    {
        Enemies = enemies;

        return this;
    }
}
