using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPanel : MonoBehaviour
{
    public List<ToolProperies> tools = new List<ToolProperies> { };
    public List<ToolButton> buttons = new List<ToolButton> { };
    private void Start()
    {
        InitButtons();
    }

    void InitButtons()
    {
        RandomizeToolsList();
        for (int i = 0; i < tools.Count; i++)
        {
            buttons[i].Init(tools[i]);
        }
    }

    void RandomizeToolsList()
    {
        List<ToolProperies> newToolsList = new List<ToolProperies> { };

        int toolsListCount = tools.Count;
        for (int i = 0; i < toolsListCount; i++)
        {
            int index = Random.Range(0, tools.Count);
            newToolsList.Add(tools[index]);
            tools.RemoveAt(index);
        }

        tools = newToolsList;
    }
}
