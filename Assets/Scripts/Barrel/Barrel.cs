using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RayFire;

[RequireComponent(typeof(BarrelText))]
public class Barrel : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _forceExploission;

    private RayfireRigid _rayfireRigid;
    private ParticleSystem _particle;
    private List<Transform> _details = new List<Transform>();
    private bool _startCoroutine;
    private bool _exploission;

    [SerializeField]private bool i;

    public event UnityAction HealthChanged;
    public event UnityAction IsDestroyed;

    public int Health => _health;

    private void Start()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        _rayfireRigid = GetComponentInChildren<RayfireRigid>();

        _rayfireRigid.NeedExploision += Exploission;
    }

    private void Update()
    {
        if (i)
        {
            i = false;
            Exploission();
        }
    }

    private void OnDisable()
    {
        _rayfireRigid.NeedExploision -= Exploission;
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
        _rayfireRigid.Demolish();

        _particle.Play();

        yield return new WaitForSeconds(_particle.startLifetime);

        Destroy(gameObject);
    }

    private void Exploission()
    {
        foreach (var detail in _details)
            detail.GetComponent<Rigidbody>().AddForce((detail.position - transform.position).normalized * _forceExploission);
    }

    private void ApplyDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke();

        if (_health <= 0)
            IsDestroyed?.Invoke();
    }
}
