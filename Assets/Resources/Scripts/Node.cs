using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node Next;

    private void Update()
    {
        Debug.DrawLine(transform.position, Next.transform.position);
    }
    
}
