using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Movement : MonoBehaviour
{

    float speed = 500f;
    float rotation_speed = 30f;
    Rigidbody rb;
    float current_angle;
    public GameObject guide_view;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        current_angle = 0;
        guide_view = GameObject.FindWithTag("Guide");
    }

    // Update is called once per frame
    void Update()
    {
        float xDir = (float)Math.Cos(current_angle * Math.PI / 180);
        float zDir = (float)Math.Sin(current_angle * Math.PI / 180);

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(speed * xDir, rb.velocity.y, speed * zDir) * Time.deltaTime * - 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(speed * xDir, rb.velocity.y, speed * zDir) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        { 
            transform.Rotate(0, - rotation_speed * Time.deltaTime, 0, Space.Self);
            current_angle += rotation_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotation_speed * Time.deltaTime, 0, Space.Self);
            current_angle -= rotation_speed * Time.deltaTime;
        }

        guide_view.transform.position = new Vector3(transform.position.x, guide_view.transform.position.y, transform.position.z);
    }
}
