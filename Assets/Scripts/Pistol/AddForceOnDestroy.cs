using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnDestroy : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _rotationZ;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void AddForce()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-_rotationZ, _rotationZ));
        _rigidbody.AddForceAtPosition(
            Vector3.up * _force, transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)));
        _rigidbody.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ground>(out Ground ground))
            _collider.isTrigger = false;
    }
}
