using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Tile))]
public class TileText : MonoBehaviour
{
    [SerializeField] private string _text;

    private TMP_Text _tmpText;

    private void Start()
    {
        _tmpText = GetComponentInChildren<TMP_Text>();
        _tmpText.text = _text;
    }

    private void OnDisable()
    {
        _tmpText.enabled = false;
    }
}
