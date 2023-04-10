using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The HeroInfoPanel is instantiated as child of this container. 
/// While the HeroInfoPanel or another panel with priority is open, the InfoPanelContainer blocks the rest of the UI
/// </summary>
public class InfoPanelContainer : MonoBehaviour, IPointerDownHandler
{
    private IInfoPanel _infoPanel;

    public void ActivateInfoPanelContainer(IInfoPanel infoPanel)
    {
        if (_infoPanel != null)
        {
            _infoPanel.Deregister();
        }

        _infoPanel = infoPanel;
        gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ConsoleLog.Log(LogCategory.General, $"clicked on canvas");
        _infoPanel.Deregister();
        _infoPanel = null;
        gameObject.SetActive(false);
    }
}

