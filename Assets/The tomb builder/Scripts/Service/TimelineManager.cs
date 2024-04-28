using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimelineManager : MonoBehaviour
{
    private float _currentTime;
    private bool _isTimerStart;

    [Header("Длина таймера(в сек.)")]
    [SerializeField] private float _longTimer;
    [SerializeField] private TimelineView _timelineView;

    public ReactiveCommand OnTimerEndCommand = new();

    public void RestartTimer()
    {
        _currentTime = 0;
    }

    public void UpdateTimer()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _longTimer)
            OnTimerEndCommand.Execute();

        _timelineView.UpdateTimeline(_currentTime / _longTimer);
    }

    private void Start()
    {
        _currentTime = 0;
        _isTimerStart = true;
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnTimerEndCommand);
    }
}
