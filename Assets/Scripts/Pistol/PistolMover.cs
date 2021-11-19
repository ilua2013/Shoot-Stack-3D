using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PistolMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minXBorder;
    [SerializeField] private float _maxXBorder;
    [SerializeField] private Slider _slider;

    private Transform _transform;

    public event UnityAction Moved;

    private void Start()
    {
        _slider.onValueChanged.AddListener(Move);
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(Move);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(Move);
    }

    private void Move(float value)
    {
        _transform.position = new Vector3(Mathf.Lerp(_minXBorder, _maxXBorder, value), transform.position.y, transform.position.z);
        Moved?.Invoke();
    }
}
