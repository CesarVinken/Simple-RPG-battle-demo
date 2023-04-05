using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedHeroes", menuName = "ScriptableObjects/SelectedHeroes")]
public class SelectedHeroes : ScriptableObject
{
    public List<IHero> selectedHeroes;
}