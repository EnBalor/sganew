using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class WayPointEditor : EditorWindow
{
    [MenuItem("Editor Tools/Way Point Editor")]

    static public void Initialize()
    {
        GetWindow<WayPointEditor>().Show();
    }

    public GameObject ParentNode = null;

    private void OnGUI()
    {
        SerializedObject Obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(Obj.FindProperty("ParentNode"));

        if(ParentNode == null)
        {
            EditorGUILayout.HelpBox("root node ¾øÀ½", MessageType.Warning);
        }
        else
        {
            if(GUILayout.Button("Create Node"))
            {
                CreateNode();
            }
        }

        Obj.ApplyModifiedProperties();
    }

    void CreateNode()
    {
        GameObject NodeObj = new GameObject("Node_" + ParentNode.transform.childCount);
        NodeObj.transform.SetParent(ParentNode.transform);

        NodeObj.AddComponent<MyGizmo>();
        Node CurrentNode = NodeObj.AddComponent<Node>();

        float Distance = 1000.0f;

        while (true)
        {
            NodeObj.transform.position = new Vector3(Random.Range(-20.0f, 20.0f), 0.5f, Random.Range(-20.0f, 20.0f));

            if (ParentNode.transform.childCount > 1)
            {
                Node previosNode = ParentNode.transform.GetChild(ParentNode.transform.childCount - 2).GetComponent<Node>();

                previosNode.Next = CurrentNode;

                CurrentNode.Next = ParentNode.transform.GetChild(0).GetComponent<Node>();

                Distance = Vector3.Distance(previosNode.transform.position, CurrentNode.transform.position);
            }

            if (Distance > 1.5f)
                break;
        }
    }
}
