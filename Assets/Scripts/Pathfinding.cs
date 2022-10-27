using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinding {
    public static List<Tile> findPath(Tile startNode, Tile targetNode) {
        var tilesToSearch = new List<Tile>() { startNode };
        var processedTiles = new List<Tile>();

        while (tilesToSearch.Any()) {
            var currentTile = tilesToSearch[0];

            foreach (Tile tile in tilesToSearch) {
                if (tile.F < currentTile.F || tile.F == currentTile.F && tile.H < currentTile.H) {
                    currentTile = tile;
                }
            }

            processedTiles.Add(currentTile);
            tilesToSearch.Remove(currentTile);

            currentTile.setColorForPath(currentTile.closedColor);

            if (currentTile == targetNode) {
                Tile currentPathTile = targetNode;
                var backtrackedPath = new List<Tile>();
                var count = 100;

                while (currentPathTile != startNode) {
                    backtrackedPath.Add(currentPathTile);
                    currentPathTile = currentPathTile.pathConnection;
                    count--;

                    if (count < 0) {
                        throw new Exception();
                    }
                }

                foreach (Tile tile in backtrackedPath) {
                    tile.setColorForPath(Color.red);
                }
                startNode.setColorForPath(Color.red);

                return backtrackedPath;
            }

            foreach (Tile neighboor in currentTile.tileNeighboors.Where(tile => tile.isWalkable && !processedTiles.Contains(tile))) {
                bool isInSearch = tilesToSearch.Contains(neighboor);
                var costToNeighboor = currentTile.G + neighboor.getTravelDistance(neighboor);
                
                if(!isInSearch || costToNeighboor < neighboor.G) {
                    neighboor.SetG(costToNeighboor);
                    neighboor.setPathConnection(currentTile);

                    if(!isInSearch) {
                        neighboor.SetH(neighboor.getTravelDistance(targetNode));
                        tilesToSearch.Add(neighboor);
                        neighboor.setColorForPath(neighboor.openedColor);
                    }
                }
            }
        }

        return null;
    }
}
