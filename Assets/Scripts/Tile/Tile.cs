using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TileMoveX))]
[RequireComponent(typeof(TileMoveZ))]
[RequireComponent(typeof(TileText))]
[RequireComponent(typeof(Collider))]
public  class Tile : MonoBehaviour
{
    [SerializeField] private int _countBullets;

    public event UnityAction<Tile> Destroyed;
    public event UnityAction<Tile> CollidedTile;

    public int CountBullets => _countBullets;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Tile>(out Tile tile))
            CollidedTile?.Invoke(tile);

        if (other.gameObject.TryGetComponent<Trap>(out Trap trap))
            Destroy();

    }

    private void Destroy()
    {
        Destroyed?.Invoke(this);

        GetComponent<TileMoveX>().enabled = false;
        GetComponent<TileMoveZ>().enabled = false;
        GetComponent<TileText>().enabled = false;
        GetComponent<Collider>().enabled = false;

        TileAddForceOnDestroy[] tiles = GetComponentsInChildren<TileAddForceOnDestroy>();

        foreach (var tile in tiles)
            tile.AddForce();
    }

    public void SetPursued(Transform pursued)
    {
        GetComponent<TileMoveX>().Init(pursued);
        GetComponent<TileMoveZ>().Init(pursued);
    }
}
