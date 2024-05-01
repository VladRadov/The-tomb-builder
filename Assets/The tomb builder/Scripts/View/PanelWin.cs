using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelWin : MonoBehaviour
{
    [SerializeField] private Button _shop;
    [SerializeField] private Button _menu;
    [SerializeField] private TextMeshProUGUI _coinsOfGame;
    [SerializeField] private TextMeshProUGUI _coinsAllGame;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void OnShop()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu", () =>
        {
            var menuController = Object.FindObjectOfType<MenuController>();
            menuController.ActivePanelShop();
        });

    private void OnMenu()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");

    private void Start()
    {
        AudioManager.Instance.PlayWinner();
        _shop.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            OnShop();
        });
        _menu.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            OnMenu();
        });
    }

    private void OnEnable()
    {
        _coinsOfGame.text = "Score: " + ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();
        _coinsAllGame.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Money.ToString();
    }
}
