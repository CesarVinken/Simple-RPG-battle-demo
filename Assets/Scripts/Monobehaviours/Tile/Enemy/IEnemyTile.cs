public interface IEnemyTile : ITile
{
    IEnemy Enemy { get; }

    void Setup(IEnemy enemy, ICanvasController canvasController);
    void Initialise();
}
