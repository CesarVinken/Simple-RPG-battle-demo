public interface IHeroTile : ITile
{
    IHero Hero { get; }
    void Setup(IHero hero);
    void Initialise();
    void OnClick();
}
