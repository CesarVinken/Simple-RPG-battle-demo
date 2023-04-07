using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler
{
    private static readonly GameEventHandler _instance = new GameEventHandler();

    public event EventHandler<HasTakenDamageEvent> HasTakenDamageEvent;
    public event EventHandler<HasCompletedBattleEvent> HasCompletedBattleEvent;

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

    public void ExecuteHasCompletedBattleEvent(bool hasWon)
    {
        ConsoleLog.Log(LogCategory.Events, $"Execute HasCompletedBattleEvent");
        HasCompletedBattleEvent?.Invoke(this, new HasCompletedBattleEvent(hasWon));
    }
}
