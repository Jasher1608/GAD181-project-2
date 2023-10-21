using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private PlayerState state = PlayerState.Idle;
    [SerializeField] private float moveSpeed;

    float horizontal;
    float vertical;

    [SerializeField] private Animator animator;

    private Rigidbody2D rb;

    public InventoryManger inventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject craftingPanel;
    [SerializeField] private GameObject hotbarPanel;
    [SerializeField] private GameObject selector;
    [SerializeField] private Slider miningProgress;

    [SerializeField] private Tilemap ironOreTilemap;
    [SerializeField] private ItemClass ironOre;

    [SerializeField] private Tilemap stoneTilemap;
    [SerializeField] private ItemClass stone;

    public static BuildingDirection direction = BuildingDirection.South;
    [SerializeField] private TextMeshProUGUI directionText;

    private float miningTimer = 0f;
    [SerializeField] private float miningTimerThreshold;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        miningProgress.maxValue = miningTimerThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector2 movementInput = new Vector2(horizontal, vertical);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if ((horizontal != 0 || vertical != 0) && state != PlayerState.Mining)
        {
            animator.SetBool("IsMoving", true);
            int direction = DetermineDirectionAsInteger(movementInput);
            animator.SetInteger("Direction", direction);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }


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
        if (Input.GetMouseButton(1) && !inventoryPanel.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                Tilemap tilemap = hit.collider.GetComponent<Tilemap>();
                if (tilemap != null && tilemap == ironOreTilemap)
                {
                    state = PlayerState.Mining;
                    miningTimer += Time.deltaTime;
                    miningProgress.gameObject.SetActive(true);
                    miningProgress.value = miningTimer;
                    if (miningTimer >= miningTimerThreshold)
                    {
                        Mine(ironOre);
                        miningTimer = 0f;
                    }
                }
                else if (tilemap != null && tilemap == stoneTilemap)
                {
                    state = PlayerState.Mining;
                    miningTimer += Time.deltaTime;
                    miningProgress.gameObject.SetActive(true);
                    miningProgress.value = miningTimer;
                    if (miningTimer >= miningTimerThreshold)
                    {
                        Mine(stone);
                        miningTimer = 0f;
                    }
                }
            }
            else
            {
                state = PlayerState.Idle;
                miningTimer = 0f;
                miningProgress.gameObject.SetActive(false);
                miningProgress.value = 0f;
            }
        }
        else
        {
            state = PlayerState.Idle;
            miningTimer = 0f;
            miningProgress.gameObject.SetActive(false);
            miningProgress.value = 0f;
        }

        if (Input.GetMouseButtonDown(0) && !inventoryPanel.activeSelf && inventory.selectedItem)
        {
            if (!BuildingSystem.IsObjectHere() && inventory.selectedItem.GetBuildable())
            {
                Build();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (direction == BuildingDirection.West)
            {
                direction = BuildingDirection.North;
            }
            else
            {
                direction += 1;
            }
            directionText.text = direction.ToString();
        }
    }

    private void FixedUpdate()
    {
        if (!inventoryPanel.activeSelf && state != PlayerState.Mining)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }
    }

    private void Mine(ItemClass ore)
    {
        inventory.Add(ore, 1);
    }

    private void Build()
    {
        inventory.selectedItem.Use(this);
        inventory.Remove(inventory.selectedItem, 1);
    }

    private int DetermineDirectionAsInteger(Vector2 input)
    {
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;

        if (angle >= -22.5f && angle < 22.5f)
        {
            return 0; // Right
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            return 1; // Up-Right
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            return 2; // Up
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            return 3; // Up-Left
        }
        else if (angle >= 157.5f || angle < -157.5f)
        {
            return 4; // Left
        }
        else if (angle >= -157.5f && angle < -112.5f)
        {
            return 5; // Down-Left
        }
        else if (angle >= -112.5f && angle < -67.5f)
        {
            return 6; // Down
        }
        else if (angle >= -67.5f && angle < -22.5f)
        {
            return 7; // Down-Right
        }
        else
        {
            return 6; // Default to Down if the angle is not within any range
        }
    }
}

public enum PlayerState
{
    Idle,
    Mining
}
