using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = GetNextIndex(i);
            Gizmos.DrawSphere(GetWaypoint(i), 0.3f);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
        }
    }
    public int GetNextIndex(int i)
    {
        int index = i + 1;
        if (index >= transform.childCount)
            index = 0;
        return index;
    }

    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }

}
