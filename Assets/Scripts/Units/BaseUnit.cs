using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseUnit : MonoBehaviour {
    public Faction faction;
    
    [SerializeField] private int hp;
    
    [SerializeField] private float swiftness;

    [SerializeField] private List<Tile> tilesInRange;

    public RangesInDirection attackRange;

    public Tile occupiedTile;

    public void initializeAttackRange(int up, int down, int left, int right) {
        attackRange.upwardRange = up;
        attackRange.downwardRange = down;
        attackRange.leftRange = left;
        attackRange.rightRange = right;

        GridManager.instance.getTilesInUnitRange(this);
    }
}

public enum Faction {
    Player = 0,
    Enemy = 1
}

public struct RangesInDirection {
    public int upwardRange;
    public int downwardRange;
    public int leftRange;
    public int rightRange;
}