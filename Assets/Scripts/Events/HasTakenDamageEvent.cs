using System;
using UnityEngine;

public class HasTakenDamageEvent : EventArgs
{
    public IActor HitActor { get; private set; }
    public HasTakenDamageEvent(IActor hitActor)
    {
        HitActor = hitActor;
    }
}
