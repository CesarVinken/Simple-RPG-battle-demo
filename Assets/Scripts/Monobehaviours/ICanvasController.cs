using UnityEngine;

public interface ICanvasController
{
    void AddTile(IHeroTile tile);
    void OnClickHero(IHeroTile tile);
}