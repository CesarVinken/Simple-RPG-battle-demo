using UnityEngine;

public interface ICanvasController
{
    void Unload();
    void RegisterTile(ITile tile);
    void OnClickHero(IHeroTile tile);
}