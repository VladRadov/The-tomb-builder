using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _openSettings;
    [SerializeField] private Button _openInformation;
    [SerializeField] private Button _openShop;
    [SerializeField] private Button _levels;
    [SerializeField] private Button _startGame;

    [SerializeField] private SettingsView _settingsView;

    private void Start()
    {
        _openSettings.onClick.AddListener(() =>
        {
            _settingsView.SetActive(true);
            AudioManager.Instance.PlayClickButton();
        });

        _openInformation.onClick.AddListener(() =>
        {

            AudioManager.Instance.PlayClickButton();
        });

        _openShop.onClick.AddListener(() =>
        {

            AudioManager.Instance.PlayClickButton();
        });

        _levels.onClick.AddListener(() =>
        {

            AudioManager.Instance.PlayClickButton();
        });

        _startGame.onClick.AddListener(() =>
        {

            AudioManager.Instance.PlayClickButton();
        });
    }
}
