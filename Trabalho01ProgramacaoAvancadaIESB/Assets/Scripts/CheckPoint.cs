using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    int checkPointID;
    private void OnTriggerEnter(Collider other) {
        var runner = other.GetComponent<IRunner>();
        if(runner != null)
        {
            if (runner.nextCheckPoint == checkPointID)
            {              
                runner.nextCheckPoint++;
            }
        }

    }
}
