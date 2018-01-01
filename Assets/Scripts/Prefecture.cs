using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefecture : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public bool isMoving = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > 0.01f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
