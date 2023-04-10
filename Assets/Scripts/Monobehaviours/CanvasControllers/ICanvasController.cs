
// The ICanvasController controls the canvas of the current scene
public interface ICanvasController : IGameService
{
    void Setup();
    void Initialise();
    void Unload();
    
    void RegisterTile(ITile tile);
    ITile GetTile(IActor actor);
    void ActivateTile(ITile tile);

    void ToScene(SceneType sceneType);
    InfoPanelContainer GetInfoPanelContainer();
}