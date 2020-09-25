﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCheck : MonoBehaviour
{
    [SerializeField] GameObject toActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Box>() != null)
        {
            toActivate.SetActive(false);
        }
    }
}
