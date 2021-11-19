using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTilesMover : MonoBehaviour
{
    [SerializeField] private float _delayEnable;

    private PistolTiles _pistolTiles;
    private PistolMover _pistolMover;
    private List<TileMoveX> _tiles = new List<TileMoveX>();

    private void Awake()
    {
        _pistolTiles = GetComponent<PistolTiles>();
        _pistolMover = GetComponent<PistolMover>();
    }

    private void OnEnable()
    {
        _pistolTiles.AddedTile += AddTile;
        _pistolTiles.RemovedTile += RemoveTile;
        _pistolMover.Moved += StartCoroutineEnableTiles;
    }

    private void OnDisable()
    {
        _pistolTiles.AddedTile -= AddTile;
        _pistolTiles.RemovedTile -= RemoveTile;
        _pistolMover.Moved -= StartCoroutineEnableTiles;
    }

    private void StartCoroutineEnableTiles()
    {
        StartCoroutine(EnableTilesMoveX());
    }

    private IEnumerator EnableTilesMoveX()
    {
        var wait = new WaitForSeconds(_delayEnable);

        for (int i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].enabled = true;
            yield return wait;
        }
    }

    private void AddTile(Tile tile)
    {
        _tiles.Add(tile.GetComponent<TileMoveX>());
    }

    private void RemoveTile(Tile tile)
    {
        _tiles.Remove(tile.GetComponent<TileMoveX>());
    }
}
