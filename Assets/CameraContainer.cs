using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContainer : MonoBehaviour
{
    private PlayerController dog;

    private void Awake()
    {
        dog = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (dog.transform.position - transform.position) / 10;
    }
}
