using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private float beltSpeed;
    public BeltDirection beltDirection;
    Rigidbody2D rb;

    private void FixedUpdate()
    {
        if (rb != null)
        {
            switch (beltDirection)
            {
                case BeltDirection.Right:
                    rb.velocity = new Vector2(rb.velocity.x + beltSpeed, rb.velocity.y);

                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        rb = collision.gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        rb = null;
    }
}

public enum BeltDirection
{
    Up,
    Down,
    Left,
    Right
}
