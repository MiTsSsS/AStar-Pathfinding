using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour {
    [SerializeField]
    int width, height = 0;

    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Transform cam;

    private void Start() {
        generateGrid();
    }

    void generateGrid() {
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                var spawnedTile = Instantiate(tile, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i}, {j}";
                spawnedTile.setTileColor((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0) ? true : false);
            }
        }
    }
}
