using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    public GameObject cam;

    private Transform target;

    private void Start()
    {
        target = cam.transform;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.LookAt(target);
    }
}
