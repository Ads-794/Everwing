using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waves : MonoBehaviour
{
    void OnTransformChildrenChanged()
    {
        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
