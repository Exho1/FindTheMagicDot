using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class Movement : MonoBehaviour
{

    float movement_speed = 500f;
    float rotation_speed = 40f;
    Rigidbody rb; //Holds the seeker object
    float current_angle; //The current angle the Seeker is facing from the negative x axis
    public GameObject guide_view; //Holds the guide camera
    GameObject scanner;
    GameObject magic_dot;
    bool scanner_light_on;
    int scanner_blink_rate;
    int time_elapsed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        current_angle = 0;
        guide_view = GameObject.FindWithTag("Guide");
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        scanner_light_on = false;
        time_elapsed = 0;
        magic_dot = GameObject.FindWithTag("Finish");
        magic_dot.GetComponent<Renderer>().material.color = Color.red;
        scanner = GameObject.FindWithTag("Scanner");

    }

    // Update is called once per frame
    void Update()
    {

        scanner_blink_rate = (int)get_distance_between(gameObject, magic_dot);

        //Multiplier for the speed in the x direciton
        float xDir = (float)Math.Cos(current_angle * Math.PI / 180);
        //Multiplier for the speed in the z direction
        float zDir = (float)Math.Sin(current_angle * Math.PI / 180);

        //If the W key is held down the seeker travels forward
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(movement_speed * xDir, rb.velocity.y, movement_speed * zDir) * Time.deltaTime * - 1;
        }

        //If the S key is held down the seeker travels backwards
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(movement_speed * xDir, rb.velocity.y, movement_speed * zDir) * Time.deltaTime;
        }

        //If the A key is held the seeker turns to their left
        if (Input.GetKey(KeyCode.A))
        { 
            transform.Rotate(0, - rotation_speed * Time.deltaTime, 0, Space.Self);
            current_angle += rotation_speed * Time.deltaTime;
        }

        //If the D key is held the seeker turns to their right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotation_speed * Time.deltaTime, 0, Space.Self);
            current_angle -= rotation_speed * Time.deltaTime;
        }

        time_elapsed++;
        if (time_elapsed >= scanner_blink_rate)
        {
            scanner_light_on = !scanner_light_on;
            time_elapsed = 0;
        }
        
        if(scanner_light_on)
        {
            scanner.GetComponent<Renderer>().material.color = Color.green;
         
        }
        else
        {
            scanner.GetComponent<Renderer>().material.color = Color.black;
        }
        
        //Updates the guide's point of view in the x and z directions to be directly over the seeker
        guide_view.transform.position = new Vector3(transform.position.x, guide_view.transform.position.y, transform.position.z);
    }

    //When the seeker comes into contact with the magic dot the magic dot is destroyed.
    //TODO: trigger the game end scene
    void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(collision.gameObject);
        }
    }

    float get_distance_between(GameObject ob1, GameObject ob2)
    {
        return (float)Math.Sqrt(Math.Pow(ob1.transform.position.x - ob2.transform.position.x, 2) + Math.Pow(ob2.transform.position.z - ob1.transform.position.z, 2));
    }

    float get_angle_between(GameObject ob1, GameObject ob2)
    {
        return (float)Math.Atan((ob1.transform.position.z - ob2.transform.position.z) / (ob1.transform.position.x - ob2.transform.position.x));
    }
}
