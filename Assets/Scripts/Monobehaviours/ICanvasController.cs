using System.Collections.Generic;
using UnityEngine;

public interface ICanvasController
{
    PanelHandler PanelHandler { get;}

    void Unload();
    void RegisterTile(ITile tile);

    InfoPanelContainer GetInfoPanelContainer();

    void OnClickHero(IHeroTile tile);
}