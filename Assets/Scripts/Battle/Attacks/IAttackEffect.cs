using UnityEngine;

public interface IAttackEffect
{
    void Setup(IAttack attack);
    void Initialise();
    void OnAttackFinished();
}