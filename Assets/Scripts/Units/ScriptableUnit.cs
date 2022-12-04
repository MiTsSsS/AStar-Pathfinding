using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject {
    public struct RangesInDirection {
        public int upwardRange;
        public int downwardRange;
        public int leftRange;
        public int rightRange;
    }

    public BaseUnit unitPrefab;
    public CustomUtility.Faction faction;
    public RangesInDirection attackRange;
    public bool hasPlayedTurn;
    public float hitpoints;
    public float mana;
    public float speed;
}