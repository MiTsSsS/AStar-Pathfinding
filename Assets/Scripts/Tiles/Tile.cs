using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color color, offsetColor;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private GameObject tileHighlight;
    
    public BaseUnit occupyingUnit;

    public Vector2Int tilePosition;

    public void setTileColor(bool isOffset) {
        rend.color = isOffset ? offsetColor : color;
    }

    private void OnMouseEnter() {
        tileHighlight.SetActive(true);
    }

    private void OnMouseExit() {
        tileHighlight.SetActive(false);
    }

    private void OnMouseDown() {
        Debug.Log(tilePosition.x + ", " + tilePosition.y);

        if (GameManager.instance.gameState != GameState.PlayerTurn)
            return;

        if(occupyingUnit != null) {
            if(occupyingUnit.faction == Faction.Player) {
                UnitManager.instance.setSelectedUnit(occupyingUnit);
            } else {
                if(UnitManager.instance.selectedUnit != null) {
                    //Clicking on enemy while a friendly unit is selected
                }
            }
        }

        else {
            if(UnitManager.instance.selectedUnit != null) {
                setUnitOnTile(UnitManager.instance.selectedUnit);
                UnitManager.instance.setSelectedUnit(null);
            }
        }
    }

    public void setUnitOnTile(BaseUnit unit) {
        if (unit.occupiedTile != null)
            unit.occupiedTile.occupyingUnit = null;

        unit.transform.position = transform.position;
        occupyingUnit = unit;
        unit.occupiedTile = this;
    }

    //Pathfinding

    private static readonly List<Vector2> tileNeighboorDirections = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };

    public List<Tile> tileNeighboors;

    public void cacheNeighboors() {
        tileNeighboors  = new List<Tile>();

        foreach (Tile tile in tileNeighboorDirections.Select(neighboorDirection => GridManager.instance.getTileAtPosition(tilePosition + neighboorDirection)).Where(tile => tile != null)) {
            tileNeighboors.Add(tile);
        }
    }

    public float getTravelDistance(Tile otherTile) {
        Vector2Int distance = new Vector2Int(Mathf.Abs(tilePosition.x - otherTile.tilePosition.x), Mathf.Abs(tilePosition.y - otherTile.tilePosition.y));

        int highestValue = Mathf.Max(distance.x, distance.y);
        int lowestValue = Mathf.Min(distance.x, distance.y);

        int requiredHorizontalMoves = highestValue - lowestValue;

        return lowestValue * 14 + requiredHorizontalMoves * 10;
    }
}