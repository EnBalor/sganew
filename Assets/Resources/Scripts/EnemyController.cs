using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 Direction;
    private int TargetCount;

    private void Start()
    {
        TargetCount = 0;
        GetDirection();
    }

    private void Update()
    {
        transform.position += Direction * 5.0f * Time.deltaTime;
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        GetDirection();
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        GetDirection();
    }

    void GetDirection()
    {
        /*
        Direction = (WayPointManager.NodeList[TargetCount].transform.position - this.transform.position).normalized;

        if (WayPointManager.NodeList.Count <= TargetCount)
            TargetCount = 0;
        else
            TargetCount++;

        transform.LookAt(Direction);
        */
    }
}
