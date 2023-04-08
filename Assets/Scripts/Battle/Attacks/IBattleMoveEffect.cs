using UnityEngine;

public interface IBattleMoveEffect
{
    void Setup(IAttack attack);
    void Initialise();
    void OnEffectFinished();
}