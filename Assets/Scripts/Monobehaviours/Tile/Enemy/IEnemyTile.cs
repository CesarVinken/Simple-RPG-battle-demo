public interface IEnemyTile : ITile
{
    IEnemy Enemy { get; }

    void Setup(IEnemy enemy);
    void Initialise();
}
