using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefecture : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0.01f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
