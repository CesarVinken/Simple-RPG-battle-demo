public interface IHero : IActor
{
    int XP { get; }

    void ResetHealth();
    void UpdateStats(int xp);
}
