using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedHeroes", menuName = "ScriptableObjects/SelectedHeroesAsset")]
public class SelectedHeroesAsset : ScriptableObject
{
    [SerializeField]
    public List<IHero> SelectedHeroes = new List<IHero>();
}