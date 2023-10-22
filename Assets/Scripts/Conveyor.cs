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
                case BeltDirection.North:
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + beltSpeed);
                    break;
                case BeltDirection.East:
                    rb.velocity = new Vector2(rb.velocity.x + beltSpeed, rb.velocity.y);
                    break;
                case BeltDirection.South:
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - beltSpeed);
                    break;
                case BeltDirection.West:
                    rb.velocity = new Vector2(rb.velocity.x - beltSpeed, rb.velocity.y);
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
        collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        rb = null;
    }
}

public enum BeltDirection
{
    North,
    East,
    South,
    West
}
