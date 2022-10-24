using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color color, offsetColor;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private GameObject tileHighlight;

    public BaseUnit occupyingUnit;

    public int xGridPosition, yGridPosition;

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
        Debug.Log(xGridPosition + ", " + yGridPosition);

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
}