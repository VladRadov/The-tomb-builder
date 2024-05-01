using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _viewsScore;
    [SerializeField] private TextMeshProUGUI _viewMoney;
    [SerializeField] private int _increaseScore;

    public void AddScore()
    {
        AudioManager.Instance.PlayGetScore();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins += _increaseScore;
        UpdateViewScore();
    }

    public void AddMoney()
    {
        AudioManager.Instance.PlayGetMoney();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Money += _increaseScore;
        _viewMoney.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Money.ToString();
    }

    private void UpdateViewScore()
    {
        foreach (var viewScore in _viewsScore)
            viewScore.text = "Score: " + ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();
    }

    private void Start()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins = 0;
        UpdateViewScore();
    }
}
