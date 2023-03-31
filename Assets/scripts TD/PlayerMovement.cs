using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float move_speed = 10f;
    public Rigidbody2D body;
    Vector2 movement; //can store an x and y

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    //called on a fixed interval
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * move_speed * Time.fixedDeltaTime);
    }
}
