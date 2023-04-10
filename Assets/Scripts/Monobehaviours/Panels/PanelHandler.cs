using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : IGameService
{
    private List<IPanel> _openPanels = new List<IPanel>();

    public void RegisterPanel(IPanel panel)
    {
        _openPanels.Add(panel);
    }

    public void DeregisterPanel(IPanel panel)
    {
        _openPanels.Remove(panel);
    }

    public List<IPanel> GetOpenPanels()
    {
        return _openPanels;
    }

    public void ClearOpenPanels()
    {
        for (int i = 0; i < _openPanels.Count; i++)
        {
            _openPanels[i].Deregister();
        }
    }
}
