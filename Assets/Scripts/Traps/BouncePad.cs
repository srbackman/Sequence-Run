using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 5f;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
        if (!playerMovement) return;

        playerMovement.AddBounceForce(transform.TransformDirection(transform.up) * _bounceForce);
    }
}
