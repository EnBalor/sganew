using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WayPointManager : MonoBehaviour
{
    /*
    [HideInInspector] static public List<Node> NodeList = new List<Node>();
    private int Count = 0;

    private void Awake()
    {
        for (int i = 0; i < 4; ++i)
            CreateWayPoint(new Vector3(Random.Range(-20.0f, 20.0f), 0.5f, Random.Range(-20.0f, 20.0f)));
    }

    private void Start()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            if(i > 0)
            {
                Node node = transform.GetChild(i - 1).GetComponent<Node>();
                node.Next = transform.GetChild(i).GetComponent<Node>().gameObject;
            }
        }

        if (NodeList.Count > 0)
            NodeList[NodeList.Count - 1].GetComponent<Node>().Next = NodeList[0].GetComponent<Node>().gameObject;
    }

    void CreateWayPoint(Vector3 _Position)
    {
        GameObject obj = new GameObject("Node_" + Count.ToString());

        obj.transform.SetParent(GameObject.Find("WayPoint").transform);
        obj.transform.position = _Position;

        SphereCollider Collider = obj.AddComponent<SphereCollider>();
        Collider.radius = 0.2f;
        Collider.isTrigger = true;

        Node _Node = obj.AddComponent<Node>();

        NodeList.Add(_Node);
        Count++;
    }
*/
}