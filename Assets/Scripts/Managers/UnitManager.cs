using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    public ScriptableUnit swordsman;
    public BaseUnit selectedUnit;

    private void Awake() {
        instance = this;
    }

    public void spawnCharacter(Faction faction) {
        var spawnedUnit = Instantiate(swordsman.unitPrefab);
        spawnedUnit.GetComponent<SpriteRenderer>().color = faction == Faction.Player ? Color.yellow : Color.red;
        spawnedUnit.faction = faction;

        Tile randomTile = GridManager.instance.getRandomTile();

        if (randomTile != null) {
            randomTile.setUnitOnTile(spawnedUnit);
        }
    }

    public void setSelectedUnit(BaseUnit unit) {
        selectedUnit = unit;
    }
}
