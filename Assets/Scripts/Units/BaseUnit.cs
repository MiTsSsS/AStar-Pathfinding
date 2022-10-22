using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour {
    public Faction faction;
    [SerializeField] private int hp;
    [SerializeField] private float swiftness;

    public Tile occupiedTile;
}

public enum Faction {
    Player = 0,
    Enemy = 1
}