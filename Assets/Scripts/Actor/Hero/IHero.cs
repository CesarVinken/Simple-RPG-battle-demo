public interface IHero : IActor
{
    int XP { get; }

    void Initialise(int xp);
}
