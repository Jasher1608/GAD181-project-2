using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    float horizontal;
    float vertical;
    private Vector2 movement;
    private Rigidbody2D rb;

    public InventoryManger inventory;
    [SerializeField] private GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
}

public enum PlayerState
{
    Idle,
    Mining,
    Running
}
