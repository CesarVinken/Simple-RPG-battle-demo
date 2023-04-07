using System;

public class HeroDefeatedEvent : EventArgs
{
    public IHero Hero { get; private set; }

    public HeroDefeatedEvent(IHero hero)
    {
        Hero = hero;
    }
}
