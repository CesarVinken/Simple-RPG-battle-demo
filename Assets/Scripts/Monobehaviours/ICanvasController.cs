using System.Collections.Generic;
using UnityEngine;

public interface ICanvasController
{
    void Unload();
    void RegisterTile(ITile tile);

    InfoPanelContainer GetInfoPanelContainer();

    void OnClickHero(IHeroTile tile);
}