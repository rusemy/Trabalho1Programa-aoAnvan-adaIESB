using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public int groundID;

    private void OnTriggerEnter(Collider other)
    {
        var runner = other.GetComponent<IRunner>();
        if (runner != null)
        {
            runner.onGroundID = groundID;
        }
    }
}