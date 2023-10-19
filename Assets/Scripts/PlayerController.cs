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
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject hotbarPanel;
    [SerializeField] private GameObject selector;

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

        // Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryPanel.activeInHierarchy)
            {
                inventoryPanel.SetActive(false);
                craftingPanel.SetActive(false);
                hotbarPanel.SetActive(true);
                selector.SetActive(true);
            }
            else
            {
                inventoryPanel.SetActive(true);
                craftingPanel.SetActive(true);
                hotbarPanel.SetActive(false);
                selector.SetActive(false);
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!inventoryPanel.activeSelf)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }
    }
}

public enum PlayerState
{
    Idle,
    Mining,
    Running
}
