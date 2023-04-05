public interface IHero : IActor
{
    int XP { get; }

    void UpdateStats(int xp);
}
