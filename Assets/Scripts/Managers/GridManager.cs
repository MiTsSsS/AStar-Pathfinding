using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour {
    [SerializeField] int width, height = 0;
    [SerializeField] private Tile tile;
    [SerializeField] private Transform cam;
    private Dictionary<Vector2, Tile> tiles;
    public static GridManager instance;

    //Pathfinding
    private Tile goalTile;
    public List<Tile> path;

    private void Awake() {
        instance = this;
    }

    public void generateGrid() {
        tiles = new Dictionary<Vector2, Tile>();

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                var spawnedTile = Instantiate(tile, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i}, {j}";
                spawnedTile.setTileColor(((i + j) % 2 == 1));
                spawnedTile.tilePosition = new Vector2Int(i, j);

                tiles[new Vector2(i, j)] = spawnedTile;
            }
        }

        foreach (Tile tile in tiles.Values) {
            tile.cacheNeighboors();
        }

        Tile.OnHoverTile += OnTileHover;

        GameManager.instance.changeState(GameManager.GameState.SpawnCharacters);
    }

    public Tile getTileAtPosition(Vector2 position) {
        if (tiles.TryGetValue(position, out _)) {
            return tiles[position];
        }

        return null;
    }

    public Tile getRandomTile() {
        Tile randomTile = tiles[new Vector2(Random.Range(0, 16), Random.Range(0, 9))];
        Tile finalTile = null;

        if (randomTile.occupyingUnit == null) {
            finalTile = randomTile;
        } else {
            Debug.Log("Chosen same tile");
            getRandomTile();
        }

        return finalTile;
    }

    //Attack range implementation
    public void getTilesInUnitRange(BaseUnit unit) {
        int minHorizontalRange = unit.unitStats.attackRange.leftRange;
        int maxHorizontalRange = unit.unitStats.attackRange.rightRange;
        int minVerticalRange = unit.unitStats.attackRange.downwardRange;
        int maxVerticalRange = unit.unitStats.attackRange.upwardRange;

        for (int mhr = minHorizontalRange; mhr <= maxHorizontalRange; mhr++) {
            for (int mvr = minVerticalRange; mvr <= maxVerticalRange; mvr++) {
                //Iterating tiles in unit range to look for enemy character and other things

            }
        }
    }

    //Pathfinding

    private void OnTileHover(Tile tile) {
        goalTile = tile;

        foreach (var t in tiles.Values) 
            t.RevertTile();

        if (UnitManager.instance.selectedUnit != null) {
            path = Pathfinding.findPath(UnitManager.instance.selectedUnit.occupiedTile, goalTile);
        }
    }

    private void OnDrawGizmos() {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.yellow;

        if(tiles.Count > 0) {
            foreach (var tile in tiles) {
                if (tile.Value.pathConnection == null) {
                    continue;
                }
                Gizmos.DrawLine((Vector3)tile.Key + new Vector3(0, 0, -1), (Vector3)tile.Value.pathConnection.tilePosition + new Vector3(0, 0, -1));
            }
        }
    }
}