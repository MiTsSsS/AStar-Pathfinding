using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseUnit : MonoBehaviour {
    public ScriptableUnit unitStats;
    [SerializeField] private List<Tile> tilesInRange;
    public Tile occupiedTile;

    public void initializeAttackRange(int up, int down, int left, int right) {
        unitStats.attackRange.upwardRange = up;
        unitStats.attackRange.downwardRange = down;
        unitStats.attackRange.leftRange = left;
        unitStats.attackRange.rightRange = right;

        GridManager.instance.getTilesInUnitRange(this);
    }
}