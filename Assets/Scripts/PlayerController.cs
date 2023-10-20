using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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

    [SerializeField] private Tilemap ironOreTilemap;
    [SerializeField] private ItemClass ironOre;

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

        Vector3 mousePos = Input.mousePosition;
        

        // Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryPanel.activeSelf)
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

        // Mining
        if (Input.GetMouseButtonDown(1) && !inventoryPanel.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                Tilemap tilemap = hit.collider.GetComponent<Tilemap>();
                if (tilemap != null && tilemap == ironOreTilemap)
                {
                    MineIron();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && inventory.selectedItem && !inventoryPanel.activeSelf)
        {
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

            if (!BuildingSystem.IsObjectHere(worldPos2D))
            {
                Build();
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

    private void MineIron()
    {
        inventory.Add(ironOre, 1);
    }

    private void Build()
    {
        inventory.selectedItem.Use(this);
        inventory.Remove(inventory.selectedItem, 1);
    }
}

public enum PlayerState
{
    Idle,
    Mining,
    Running
}
