using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ItemShopView : MonoBehaviour
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private int _price;
    [SerializeField] private TextMeshProUGUI _viewPrice;
    [SerializeField] private GameObject _close;
    [SerializeField] private Button _purchase;
    [SerializeField] private GameObject _purchased;

    public ReactiveCommand OnPurchasedCommand = new();

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(OnPurchasedCommand);
        _purchase.onClick.AddListener(() => { OnPurchaseLevel(); });
        _viewPrice.text = "Buy" + _price.ToString();
    }

    private void OnPurchaseLevel()
    {
        if (WalletManager.TryPurchase(_price))
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.PurchasedLevels += _numberLevel.ToString() + ";";
            SetActiveButtons(false, false, true);
            OnPurchasedCommand.Execute();
        }
    }

    private void OnEnable()
    {
        if (_numberLevel > ContainerSaveerPlayerPrefs.Instance.SaveerData.Level)
            SetActiveButtons(true, false, false);
        else if (_numberLevel == ContainerSaveerPlayerPrefs.Instance.SaveerData.Level)
            SetActiveButtons(false, false, true);
        else if (ContainerSaveerPlayerPrefs.Instance.SaveerData.EndLevels.Contains((_numberLevel - 1).ToString()))
            SetActiveButtons(false, true, false);
    }

    private void SetActiveButtons(bool isClose, bool isPurchase, bool isPurchased)
    {
        _close.SetActive(isClose);
        _purchase.gameObject.SetActive(isPurchase);
        _purchased.SetActive(isPurchased);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnPurchasedCommand);
    }
}
