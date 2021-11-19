using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDie : MonoBehaviour
{
    [SerializeField] private PistolDestroy _pistolDestroy;
    [SerializeField] private float _delayPanel;

    private CanvasGroup _canvasGroup;
    private Button[] _buttons;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;

        _buttons = GetComponentsInChildren<Button>();
        foreach (var button in _buttons)
            button.enabled = false;
    }

    private void OnEnable()
    {
        _pistolDestroy.Destroyed += StartOpenPanel;
    }

    private void OnDisable()
    {
        _pistolDestroy.Destroyed -= StartOpenPanel;
    }

    private void StartOpenPanel()
    {
        StartCoroutine(OpenPanel());
    }

    private IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(_delayPanel);

        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

        foreach (var button in _buttons)
            button.enabled = true;
    }
}
