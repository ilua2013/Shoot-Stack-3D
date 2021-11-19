using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBarrelDestroy : MonoBehaviour
{
    private Barrel[] _barrels;

    private void Awake()
    {
        _barrels = GetComponentsInChildren<Barrel>();

        foreach (var barrel in _barrels)
            barrel.IsDestroyed += DestroyBarrels;
    }

    private void DestroyBarrels()
    {
        foreach (var barrel in _barrels)
                barrel.StartDestroy();
    }
}
