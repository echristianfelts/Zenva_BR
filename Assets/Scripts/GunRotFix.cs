using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotFix : MonoBehaviour
{
    public Transform target;
    public float speed;
    //  public GameObject self;

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;

        // Rotate our transform a step closer to the target's.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
    }
}
