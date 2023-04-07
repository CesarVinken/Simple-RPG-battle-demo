using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler
{
    private static readonly GameEventHandler _instance = new GameEventHandler();

    public event EventHandler<EnemyDefeatedEvent> EnemyDefeatedEvent;
    public event EventHandler<HasTakenDamageEvent> HasTakenDamageEvent;
    public event EventHandler<HeroDefeatedEvent> HeroDefeatedEvent;

    private GameEventHandler()
    {

    }

    public static GameEventHandler GetInstance()
    {
        return _instance;
    }

    public void ExecuteHasTakenDamageEvent(IActor hitActor)
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute HasTakenDamageEvent");
        HasTakenDamageEvent?.Invoke(this, new HasTakenDamageEvent(hitActor));
    }

    public void ExecuteHeroDefeatedEvent(IHero hero)
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute HeroDefeatedEvent");
        HeroDefeatedEvent?.Invoke(this, new HeroDefeatedEvent(hero));
    }

    public void ExecuteEnemyDefeatedEvent()
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute EnemyDefeatedEvent");
        EnemyDefeatedEvent?.Invoke(this, new EnemyDefeatedEvent());
    }
}
