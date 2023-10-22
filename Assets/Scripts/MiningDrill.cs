using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningDrill : MonoBehaviour
{
    public MinerDirection minerDirection;

    private Tilemap ironOreTilemap;
    [SerializeField] private GameObject ironOre;
    private Tilemap stoneTilemap;
    [SerializeField] private GameObject stone;
    [SerializeField] private float oreMiningTime;

    [SerializeField] private float miningSpeed;

    private bool isMining = false;
    private bool miningStone = false;
    private bool miningIronOre = false;

    private void Start()
    {
        ironOreTilemap = GameObject.FindGameObjectWithTag("IronOreTilemap").GetComponent<Tilemap>();
        stoneTilemap = GameObject.FindGameObjectWithTag("StoneTilemap").GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (miningStone && !isMining)
        {
            StartCoroutine(Mine(stone));
        }
        else if (miningIronOre && !isMining)
        {
            StartCoroutine(Mine(ironOre));
        }
    }

    private IEnumerator Mine(GameObject item)
    {
        isMining = true;
        float time = miningSpeed / oreMiningTime;

        yield return new WaitForSeconds(time);
        switch (minerDirection)
        {
            case MinerDirection.North:
                Instantiate(item, new Vector3(this.transform.position.x, this.transform.position.y + 0.55f, this.transform.position.z), Quaternion.identity);
                break;
            case MinerDirection.East:
                Instantiate(item, new Vector3(this.transform.position.x + 0.55f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                break;
            case MinerDirection.South:
                Instantiate(item, new Vector3(this.transform.position.x, this.transform.position.y - 0.975f, this.transform.position.z), Quaternion.identity);
                break;
            case MinerDirection.West:
                Instantiate(item, new Vector3(this.transform.position.x - 0.925f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                break;
        }
        isMining = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tilemap>() != null && collision.gameObject.GetComponent<Tilemap>() == stoneTilemap)
        {
            miningStone = true;
        }
        else if (collision.gameObject.GetComponent<Tilemap>() != null && collision.gameObject.GetComponent<Tilemap>() == ironOreTilemap)
        {
            miningIronOre = true;
        }
    }
}

public enum MinerDirection
{
    North,
    East,
    South,
    West
}
