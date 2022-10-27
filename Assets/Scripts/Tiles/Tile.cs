using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour {
    [SerializeField] private Color color, offsetColor, defaultColor, obstacleColor;
    public Color openedColor, closedColor, pathColor;
    [SerializeField] Gradient walkableColor;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private GameObject tileHighlight;

    public bool isWalkable;
    public bool isSelected;
    
    public BaseUnit occupyingUnit;

    public Vector2 tilePosition;

    //Pathfinding

    public static event Action<Tile> OnHoverTile;

    //End Pathfinding

    private void Update() {
        if(!isWalkable) {
            setColorForPath(Color.black);
        }
    }

    public void setTileColor(bool isOffset) {
        rend.color = isOffset ? offsetColor : color;
    }

    public void setColorForPath(Color color) {
        rend.color = color;
    }

    private void OnMouseEnter() {
        tileHighlight.SetActive(true);

        if (!isWalkable)
            return;
        OnHoverTile?.Invoke(this);
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
                List<Tile> path = GridManager.instance.path;
                path.Reverse();

                StartCoroutine(unitFollowPath(UnitManager.instance.selectedUnit, path));

                UnitManager.instance.setSelectedUnit(null);
            }
        }
    }

    IEnumerator unitFollowPath(BaseUnit unit, List<Tile> path) {
        foreach (Tile tile in path) {
            tile.setUnitOnTile(unit);
            yield return new WaitForSeconds(.2f);
        }
    }

    public void setUnitOnTile(BaseUnit unit) {
        if (unit.occupiedTile != null)
            unit.occupiedTile.occupyingUnit = null;

        unit.transform.position = transform.position;
        occupyingUnit = unit;
        unit.occupiedTile = this;
    }

    public void RevertTile() {
        setTileColor(((tilePosition.x + tilePosition.y) % 2 == 1));
    }

    //Pathfinding

    private static readonly List<Vector2> tileNeighboorDirections = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };

    public List<Tile> tileNeighboors { get; protected set; }
    public Tile pathConnection { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;

    public void SetG(float g) {
        G = g;
    }

    public void SetH(float h) {
        H = h;
    }

    public void setPathConnection(Tile connectedTile) {
        pathConnection = connectedTile;
    }

    public void cacheNeighboors() {
        tileNeighboors  = new List<Tile>();

        foreach (Tile tile in tileNeighboorDirections.Select(neighboorDirection => GridManager.instance.getTileAtPosition(tilePosition + neighboorDirection)).Where(tile => tile != null)) {
            tileNeighboors.Add(tile);
        }
    }

    public float getTravelDistance(Tile otherTile) {
        Vector2 distance = new Vector2(Mathf.Abs(tilePosition.x - otherTile.tilePosition.x), Mathf.Abs(tilePosition.y - otherTile.tilePosition.y));

        var highestValue = Mathf.Max(distance.x, distance.y);
        var lowestValue = Mathf.Min(distance.x, distance.y);

        var requiredHorizontalMoves = highestValue - lowestValue;

        return lowestValue * 14 + requiredHorizontalMoves * 10;
    }
}