using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameData GameData { get; private set; }

    private Dictionary<int, IHero> _playerHeroes = new Dictionary<int, IHero>();

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        GameData = DataHandler.GetInstance().LoadGameData();

        IHero hero1 = HeroFactory.CreateRandomHero();
        _playerHeroes.Add(hero1.Id, hero1);
        IHero hero2 = HeroFactory.CreateRandomHero();
        _playerHeroes.Add(hero2.Id, hero1);
        IHero hero3 = HeroFactory.CreateRandomHero();
        _playerHeroes.Add(hero3.Id, hero1);
    }

    public Dictionary<int, IHero> GetHeroes()
    {
        return _playerHeroes;
    }

    public IHero GetHero(int id)
    {
        if(_playerHeroes.TryGetValue(id, out IHero hero))
        {
            return hero;
        }
        return null;
    }
}
