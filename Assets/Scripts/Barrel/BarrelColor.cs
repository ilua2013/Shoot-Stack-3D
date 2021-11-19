using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelColor : MonoBehaviour
{
    [SerializeField] private float _durationColorApplyDamage;
    [SerializeField] private Color _applyDamage;
    [SerializeField] private Material _red;
    [SerializeField] private Material _yellow;
    [SerializeField] private Material _green;

    private MeshRenderer _renderer;
    private Barrel _barrel;

    private void Awake()
    {
        _barrel = GetComponentInParent<Barrel>();
        _renderer = GetComponent<MeshRenderer>();

        SetActualMaterial();
    }

    private void OnEnable()
    {
        _barrel.HealthChanged += StartAnimationApplyDamage;
    }

    private void OnDisable()
    {
        _barrel.HealthChanged -= StartAnimationApplyDamage;
    }

    private void StartAnimationApplyDamage()
    {
        StartCoroutine(AnimationApplyDamage());
    }

    private IEnumerator AnimationApplyDamage()
    {
        _renderer.material.color = _applyDamage;

        yield return new WaitForSeconds(_durationColorApplyDamage);

        SetActualMaterial();
    }

    private void SetActualMaterial()
    {
        if (_barrel.Health > 150)
            _renderer.material = _red;
        else if (_barrel.Health > 50)
            _renderer.material = _yellow;
        else if (_barrel.Health > 0)
            _renderer.material = _green;
    }
}
