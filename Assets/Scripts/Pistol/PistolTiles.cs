using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PistolTiles : MonoBehaviour
{
    [SerializeField] private float _offsetTile;
    [SerializeField] private int _countBullets;
    [SerializeField] private Transform _pathAddTile;

    private List<Tile> _tiles = new List<Tile>();

    public event UnityAction<Tile> AddedTile;
    public event UnityAction<Tile> RemovedTile;

    public int CountTiles => _tiles.Count;
    public int CountBullets => _countBullets;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Tile>(out Tile tile))
            AddTile(tile);
    }

    public Vector3 GetPositionLastTile()
    {
        if (_tiles.Count >= 1)
            return _tiles[_tiles.Count - 1].transform.position;
        return Vector3.zero;
    }

    private void CountUpBullets()
    {
        _countBullets = 0;

        foreach (var tile in _tiles)
            _countBullets += tile.CountBullets;

        _countBullets = 0 >= _countBullets ? 1 : _countBullets;
    }

    private void AddTile(Tile tile)
    {
        if (_tiles.Contains(tile))
            return;

        _tiles.Add(tile);
        AddedTile?.Invoke(tile);

        tile.CollidedTile += AddTile;
        tile.Destroyed += RemoveTile;

        CountUpBullets();
        InitTile(tile);
    }

    private void RemoveTile(Tile tile)
    {
        _tiles.Remove(tile);
        RemovedTile?.Invoke(tile);
        
        tile.CollidedTile -= AddTile;
        tile.Destroyed -= RemoveTile;

        for (int i =0 ; i < _tiles.Count; i++)
            InitTile(_tiles[i]);

        CountUpBullets();
    }

    private void InitTile(Tile tile)
    {
        int index = _tiles.IndexOf(tile) == 0 ? 1 : _tiles.IndexOf(tile);

        tile.SetPursued(_tiles[index - 1].transform);
        _tiles[0].SetPursued(_pathAddTile);
    }
}
