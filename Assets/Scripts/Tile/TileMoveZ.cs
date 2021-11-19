using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoveZ : MonoBehaviour
{
    [SerializeField] private float _offsetZ;
    [SerializeField] private float _speed;

    private Transform _pursued;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(_pursued.position.z + _offsetZ != _pursued.position.z)
        {
            Vector3 direction = new Vector3(_transform.position.x, _transform.position.y, _pursued.position.z + _offsetZ);
            _transform.position = Vector3.MoveTowards(_transform.position, direction, _speed * Time.deltaTime);
        }
    }

    public void Init(Transform pursued)
    {
        _pursued = pursued;
        transform.position = pursued.position + new Vector3(0, 0, _offsetZ);

        enabled = true;
    }
}
