using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject minPos;
    [SerializeField] GameObject maxPos;

    Rigidbody2D targetRb;

    private void Start()
    {
        targetRb = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + (targetRb.gravityScale > 0 ? 2 : -2), -10);

        if(transform.position.x < minPos.transform.position.x)
        {
            transform.position = new Vector3(minPos.transform.position.x, transform.position.y, -10);
        }
        if (transform.position.x > maxPos.transform.position.x)
        {
            transform.position = new Vector3(maxPos.transform.position.x, transform.position.y, -10);
        }
    }
}
