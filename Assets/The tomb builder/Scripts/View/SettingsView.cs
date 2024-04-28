using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _close;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _close.onClick.AddListener(() =>
        {
            SetActive(false);
            AudioManager.Instance.PlayClickButton();
        });
    }
}
