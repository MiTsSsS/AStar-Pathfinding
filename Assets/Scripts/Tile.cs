using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color color, offsetColor;

    [SerializeField] private SpriteRenderer rend;

    [SerializeField] private GameObject tileHighlight;

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
    }
}
