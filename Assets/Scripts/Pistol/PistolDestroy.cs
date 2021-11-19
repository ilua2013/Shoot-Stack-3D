using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PistolDestroy : MonoBehaviour
{
    private Animator _animator;
    private bool _destroyed = false;

    public event UnityAction Destroyed;

    private const string DestroyPistol = "DestroyPistol";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_destroyed)
            return;

        if (other.TryGetComponent<Trap>(out Trap trap))
            Destroy();
    }

    private void Destroy()
    {
        Destroyed?.Invoke();

        GetComponent<PistolMover>().enabled = false;
        GetComponent<PistolShooting>().enabled = false;
        GetComponent<PistolTiles>().enabled = false;
        _animator.enabled = false;

        AddForceOnDestroy[] addForces = GetComponentsInChildren<AddForceOnDestroy>();

        foreach (var item in addForces)
            item.AddForce();

        _destroyed = true;
    }
}
