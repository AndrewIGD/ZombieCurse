using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBlades : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject targetPos;

    Vector2 pos1;
    Vector2 pos2;

    bool forward = true;

    private void Start()
    {
        pos1 = targetPos.transform.position;
        pos2 = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));

        if(forward)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos1, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pos1) < 1)
                forward = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pos2, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pos2) < 1)
                forward = true;
        }
    }
}
