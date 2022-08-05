#if (UNITY_EDITOR) 
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridMapCreator : EditorWindow
{
    Vector2 offset;
    Vector2 drag;
    List<List<Node>> nodes;
    GUIStyle empty;
    Vector2 nodePos;
    [MenuItem("Window/Grid Level Creator")]
    private static void OpenWindow()
    {
        GridMapCreator window = GetWindow<GridMapCreator>();
        window.titleContent = new GUIContent("Grid Level Creator");
        
    }

    private void OnEnable()
    {
        empty = new GUIStyle();
        Texture2D icon = Resources.Load("IconTex/Empty") as Texture2D;
        empty.normal.background = icon;
        SetUpNodes();
    }

    private void SetUpNodes()
    {
        nodes = new List<List<Node>>();
        for (int i = 0; i< 20; i++)
        {
            nodes.Add(new List<Node>());
            for (int j = 0; j < 10; j++)
            {
                nodePos.Set(i * 30, j * 30);
                nodes[i].Add(new Node(nodePos, 30, 30, empty));
            }
        }
    }

    private void OnGUI()
    {
        DrawGrid();
        DrawNodes();
        ProcessGrid(Event.current);
        if (GUI.changed)
        {
            Repaint();
        }
    }

    private void DrawNodes()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {

                nodes[i][j].Draw();
            }
        }
    }

    private void ProcessGrid(Event e)
    {
        drag = Vector2.zero; 
        switch(e.type)
        {
            case EventType.MouseDrag:
                if(e.button == 0)
                {
                    OnMouseDrag(e.delta);
                }
                break;
        }
    }

    private void OnMouseDrag(Vector2 delta)
    {
        drag = delta;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {

                nodes[i][j].Drag(delta);
            }
        }

        GUI.changed = true;
    }

    private void DrawGrid()
    {
        int widthDivider = Mathf.CeilToInt(position.width/20);
        int heightDivider = Mathf.CeilToInt(position.height/20);
        Handles.BeginGUI();
        Handles.color = Color.gray;
        offset += drag;
        Vector3 newOffset = new Vector3(offset.x % 20, offset.y % 20, 0);
        for (int i = 0; i < widthDivider; i++)
        {
            Handles.DrawLine(new Vector3(20 * i, -20, 0) + newOffset, new Vector3(20 * i, position.height, 0)+newOffset);
        }
        for (int i = 0; i < heightDivider; i++)
        {
            Handles.DrawLine(new Vector3 (-20, 20*i, 0)+newOffset , new Vector3(position.width, 20*i,0 ) + newOffset);
        }
        Handles.color= Color.white;
        Handles.EndGUI();
    }
}

#endif
