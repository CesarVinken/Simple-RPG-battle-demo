public interface IHero : IActor
{
    int Experience { get; }

    void ResetHealth();
    void UpdateStats(int experience);
}
