using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    public ScriptableUnit swordsman;

    private void Awake() {
        instance = this;
    }

    public void spawnCharacter(Faction faction) {
        var spawnedUnit = Instantiate(swordsman.unitPrefab);
        spawnedUnit.faction = faction;
        Tile randomTile = GridManager.instance.getRandomTile();

        randomTile.setUnitOnTile(spawnedUnit);
    }
}
