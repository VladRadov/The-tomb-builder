using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _viewsScore;
    [SerializeField] private int _increaseScore;

    public void AddScore()
    {
        AudioManager.Instance.PlayGetCoin();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CoinsGame += _increaseScore;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins += _increaseScore;
        UpdateViewScore();
    }

    private void UpdateViewScore()
    {
        foreach (var viewScore in _viewsScore)
            viewScore.text = "Score: " + ContainerSaveerPlayerPrefs.Instance.SaveerData.CoinsGame.ToString();
    }

    private void Start()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CoinsGame = 0;
        UpdateViewScore();
    }
}
