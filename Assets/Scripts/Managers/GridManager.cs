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

    private void Awake() {
        instance = this;
    }

    public void generateGrid() {
        tiles = new Dictionary<Vector2, Tile>();

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                var spawnedTile = Instantiate(tile, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i}, {j}";
                spawnedTile.setTileColor(((i + j) % 2 == 1) ? true : false);
                spawnedTile.xGridPosition = i;
                spawnedTile.yGridPosition = j;

                tiles[new Vector2(i, j)] = spawnedTile;
            }
        }

        GameManager.instance.changeState(GameState.SpawnCharacters);
    }

    public Tile getTileAtPosition(Vector2 position) {
        if (tiles.TryGetValue(position, out var tile)) {
            return tiles[position];
        }
        
        return null;
    }

    public Tile getRandomTile() {
        Tile randomTile = tiles[new Vector2(Random.Range(0, 17), Random.Range(0, 10))];
        Tile finalTile = null;

        if (randomTile.occupyingUnit == null) {
            finalTile = randomTile;
        } else {
            Debug.Log("Chosen same tile");
            getRandomTile();
        }

        return finalTile;
    }
}