using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    Vector2 oldPos;
    Vector2 newPos;
    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            oldPos = newPos;
            newPos = transform.position;
            if (oldPos != null)
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 45);
            }
        }
    }
}
