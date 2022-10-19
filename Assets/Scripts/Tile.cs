using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color color, offsetColor;
    [SerializeField] private SpriteRenderer rend;

    public void setTileColor(bool isOffset) {
        rend.color = isOffset ? offsetColor : color;
    }
}
