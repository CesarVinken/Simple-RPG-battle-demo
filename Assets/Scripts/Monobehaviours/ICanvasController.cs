using UnityEngine;

public interface ICanvasController
{
    void RegisterTile(ITile tile);
    void OnClickHero(IHeroTile tile);
}