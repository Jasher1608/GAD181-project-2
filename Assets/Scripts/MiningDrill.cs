using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningDrill : MonoBehaviour
{
    public MinerDirection minerDirection;
}

public enum MinerDirection
{
    North,
    East,
    South,
    West
}
