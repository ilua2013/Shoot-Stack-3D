using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoveX : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _pursued;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_pursued.position.x != _transform.position.x)
        {
            Vector3 direction = new Vector3(_pursued.position.x, _transform.position.y, _transform.position.z);
            _transform.position = Vector3.MoveTowards(_transform.position, direction, _speed * Time.deltaTime);
        } else
        {
            enabled = false;
        }
    }

    public void Init(Transform pursued)
    {
        _pursued = pursued;

        enabled = true;
    }
}
