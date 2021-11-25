using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RayFire;

[RequireComponent(typeof(BarrelText))]
[RequireComponent(typeof(Collider))]
public class Barrel : MonoBehaviour
{
    [SerializeField] private int _health;

    private RayfireRigid _rayfireRigid;
    private RayfireBomb _rayfireBomb;
    private ParticleSystem _particle;
    private List<Transform> _details = new List<Transform>();
    private Collider _collider;
    private bool _startCoroutine;
    private bool _exploission;

    public event UnityAction HealthChanged;
    public event UnityAction IsDestroyed;

    public int Health => _health;

    private void Start()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        _collider = GetComponent<Collider>();
        _rayfireBomb = GetComponent<RayfireBomb>();
        _rayfireRigid = GetComponentInChildren<RayfireRigid>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bullet>(out Bullet bullet))
            ApplyDamage(bullet.Damage);
        else if (_exploission)
            _details.Add(other.transform);
    }

    public void StartDestroy()
    {
        if (_startCoroutine)
            return;

        StartCoroutine(Destroy());
        _startCoroutine = true;

        GetComponent<BarrelText>().enabled = false;
    }

    private IEnumerator Destroy()
    {
        _exploission = true;
        _collider.enabled = false;

        _rayfireRigid.Demolish();
        _rayfireBomb.Explode(0);

        _particle.Play();

        yield return new WaitForSeconds(_particle.startLifetime);

        Destroy(gameObject);
    }

    private void ApplyDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke();

        if (_health <= 0)
            IsDestroyed?.Invoke();
    }
}
