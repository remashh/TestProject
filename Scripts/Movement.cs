using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Instantiate
{
    [SerializeField] private float _speed;
    private int rotateSpeed = 1;
    private Vector3 maxTransform = new Vector3(0,0,-10);
    private System.Random rand = new System.Random();
    private void Update()
    {

        //gameObject.transform.Translate(Vector3.forward * Time.deltaTime * _speed * -1, Space.World);
        
        if (gameObject.transform.position.z < maxTransform.z)
        {
            DestroyObjects();
        }
    }

    private void DestroyObjects()
    {
        Destroy(gameObject);
    }
}
