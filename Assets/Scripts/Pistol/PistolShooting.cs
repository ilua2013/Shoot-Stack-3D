using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PistolTiles))]
public class PistolShooting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _delayBetweenBullets;
    [SerializeField] private float _delayBetweenShots;
    [SerializeField] private Transform _pointShot;
    [SerializeField] private float _maxOffsetX;
    [SerializeField] private float _minOffsetX;
    [SerializeField] private float _maxOffsetY;
    [SerializeField] private float _minOffsetY;
    [SerializeField] private int _maxBulletsOneShot;

    private PistolTiles _pistolTiles;
    private float _elapsedTime;
    private Vector3 _offset;
    private Animator _animator;
    private const string PistolShoot = "PistolShoot";

    private void Awake()
    {
        _pistolTiles = GetComponent<PistolTiles>();
        _animator = GetComponent<Animator>();

        _elapsedTime = _delayBetweenShots;
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _delayBetweenShots)
        {
            StartCoroutine(Shoot());
            _elapsedTime = 0;
        }
    }

    private IEnumerator Shoot()
    {
        var wait = new WaitForSeconds(_delayBetweenBullets);
        int currentCountBulletsShot = 0;
        int totalIssuedBullets = 0;
        Vector3 pathSpawn = Vector3.zero;

        while (_pistolTiles.CountBullets > totalIssuedBullets)
        {
            _animator.Play(PistolShoot);

            while (_maxBulletsOneShot >= currentCountBulletsShot && _pistolTiles.CountBullets > currentCountBulletsShot)
            {
                SetActualOffset();
                pathSpawn = new Vector3(Random.Range(_minOffsetX, _maxOffsetX), Random.Range(_minOffsetY, _maxOffsetY)) + _offset;

                Instantiate(_bullet, pathSpawn, Quaternion.identity);
                
                totalIssuedBullets++;
                currentCountBulletsShot++;
            }
            currentCountBulletsShot = 0;

            yield return wait;
        }
    }

    private void SetActualOffset()
    {
        if(_pistolTiles.CountTiles > 0)
        {
            _offset = _pistolTiles.GetPositionLastTile();
            _offset.y = _pointShot.position.y;
        } else
        {
            _offset = _pointShot.position;
        }
    }
}
