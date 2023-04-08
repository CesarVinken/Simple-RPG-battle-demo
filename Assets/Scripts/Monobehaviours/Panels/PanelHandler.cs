using System.Collections.Generic;
using UnityEngine;

public class PanelHandler
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
}
