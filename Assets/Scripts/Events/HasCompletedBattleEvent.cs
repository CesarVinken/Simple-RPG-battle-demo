using System;

public class HasCompletedBattleEvent : EventArgs
{
    public bool HasWon { get; private set; }

    public HasCompletedBattleEvent(bool hasWon)
    {
        HasWon = hasWon;
    }
}
