using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]
    int spinSpeed = 1000;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spinSpeed);
    }
}
