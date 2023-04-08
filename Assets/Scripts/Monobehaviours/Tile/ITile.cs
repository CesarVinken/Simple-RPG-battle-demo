using UnityEngine;

public interface ITile
{
    IActor GetActor();
    Transform GetEffectContainer();
    void Unload();
}
