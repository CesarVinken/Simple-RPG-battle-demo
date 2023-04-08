public interface IHeroTile : ITile
{
    IHero Hero { get; }
    void Setup(IHero hero, ICanvasController canvasController);
    void Initialise();
}
