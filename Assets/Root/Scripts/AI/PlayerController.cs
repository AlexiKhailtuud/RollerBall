using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        Vector3 v = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            v += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            v += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            v += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            v += new Vector3(1, 0, 0);
        }

        transform.position += v.normalized * Time.deltaTime * 6.5f;
    }
}
