using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarrelText : MonoBehaviour
{
    private Barrel _barrel;
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _barrel = GetComponent<Barrel>();

        _barrel.HealthChanged += SetActualValue;
        SetActualValue();
    }

    private void OnDisable()
    {
        _text.enabled = false;
    }

    private void SetActualValue()
    {
        _text.text = _barrel.Health.ToString();
    }
}
