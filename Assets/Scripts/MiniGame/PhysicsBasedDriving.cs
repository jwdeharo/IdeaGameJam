using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBasedDriving : MonoBehaviour {

    public static readonly float MAX_SPEED = 10;
    public static readonly float TIME_BETWEEN_SPEED_CHANGES = 0.1f;

    public float speedOfRotation;
    public float acceleration;

    private float currentSpeed;

    private float deviation;

    private Rigidbody2D body;

    private float timeTillNextChange;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.drag = 2;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDeviation();

        timeTillNextChange -= Time.deltaTime;

        if (timeTillNextChange < 0)
        {
            if (Input.GetMouseButton(0))
                ChangeSpeed(1);
            else if (Input.GetMouseButton(1))
                ChangeSpeed(-1);
            else
                LoseSpeed();

            timeTillNextChange = TIME_BETWEEN_SPEED_CHANGES;
        }

        transform.Translate(transform.up * currentSpeed / 100, Space.World);
    }

    private void LoseSpeed()
    {
        if (currentSpeed != 0)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= acceleration / 2;
                if (currentSpeed < 0)
                    currentSpeed = 0;
            }
            else
            {
                currentSpeed += acceleration / 2;
                if (currentSpeed > 0)
                    currentSpeed = 0;
            }

        }
    }

    private void ChangeSpeed(int direction)
    {
        //1 accelarates, -1 decelerates

        if(direction>0)
            currentSpeed += direction * acceleration;
        else
            currentSpeed += direction * acceleration;

        if (currentSpeed > MAX_SPEED)
            currentSpeed = MAX_SPEED;
        if (currentSpeed < -MAX_SPEED/2)
            currentSpeed = -MAX_SPEED/2;

        Debug.Log(body.velocity);
    }

    private void CalculateDeviation()
    {
        deviation = PlayerChosenDeviation();

        transform.Rotate(transform.forward * deviation * Time.deltaTime * speedOfRotation);
        //Vector2 mouseMovement = new Vector2(Controls.MouseMovementX(), Controls.MouseMovementY());
        //Vector2 right = transform.right;

        //float deviationInputed = right.x * mouseMovement.y + right.y * mouseMovement.x;

        //deviation = deviation - Controls.MouseMovementX();

        //if (deviation > MAX_DEVIATION)
        //    deviation = MAX_DEVIATION;
        //else if (deviation < -MAX_DEVIATION)
        //    deviation = -MAX_DEVIATION;
    }

    internal static float PlayerChosenDeviation()
    {
        return -Input.GetAxis("Horizontal");
    }

    internal static bool FlutterControls()
    {
        return Input.GetMouseButtonUp(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentSpeed = -currentSpeed/5;
    }
}
