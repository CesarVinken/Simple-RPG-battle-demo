using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAttack
{
    string Name { get; }
    IActor Attacker { get; }
    IActor Target { get; }
    IAttack WithAttacker(IActor attacker);
    IAttack WithTarget(IActor attacker);
}
