using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 12.5f;
    private float startingRotation;

    void Start()
    {
        startingRotation = GameObject.Find("Player").transform.rotation.z;
    }

    void Update()
    {
        
    }
}
