using System.Collections.Generic;
using UnityEngine;

public class HeroBattleResultsContainer : MonoBehaviour
{
    [SerializeField] private HeroBattleResults _hero1Results;
    [SerializeField] private HeroBattleResults _hero2Results;
    [SerializeField] private HeroBattleResults _hero3Results;

    private List<HeroBattleResults> _heroBattleResults = new List<HeroBattleResults>();

    public async void Setup()
    {
        _heroBattleResults.Add(_hero1Results);
        _heroBattleResults.Add(_hero2Results);
        _heroBattleResults.Add(_hero3Results);

        SelectedHeroesAsset selectedHeroesAsset = await ServiceLocator.Instance.Get<ScriptableObjectHandler>().GetSelectedHeroesAsset();
        List<IHero> selectedHeroes = selectedHeroesAsset.SelectedHeroes;

        if (selectedHeroes.Count != _heroBattleResults.Count)
        {
            ConsoleLog.Error(LogCategory.General, $"We expected {_heroBattleResults.Count} heroes but found {selectedHeroes.Count}");
        }

        for (int i = 0; i < _heroBattleResults.Count; i++)
        {
            if(_heroBattleResults[i] == null)
            {
                ConsoleLog.Error(LogCategory.General, $"Could not find {_heroBattleResults[i]}");
            }

            _heroBattleResults[i].Setup(selectedHeroes[i]);
        }
    }

    public void Initialise()
    {
        for (int i = 0; i < _heroBattleResults.Count; i++)
        {
            _heroBattleResults[i].Initialise();
        }
    }
}
