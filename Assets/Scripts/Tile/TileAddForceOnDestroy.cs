using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class TileAddForceOnDestroy : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _directionX;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _rigidbody.useGravity = false;
        _collider.enabled = false;
    }

    public void AddForce()
    {
        _collider.enabled = true;
        _rigidbody.useGravity = true;

        _rigidbody.AddForceAtPosition((new Vector3(_directionX, 0.8f, -1) * _force), new Vector3(Random.Range(-1, 1), 0, 0));
    }
}
