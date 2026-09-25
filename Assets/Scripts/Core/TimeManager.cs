using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public delegate void TimeChange(int Day, int Period, EWeekDay Week);
    public event TimeChange OnTimeChanged;

    public static TimeManager Instance;

    private int _step;

    [SerializeField] private TimeConfigSO _timeConfig;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnValidate()
    {
        if (_timeConfig == null)
        {
            Debug.LogWarning("You didn't attach time config yet", this);
        }
    }

    //private void Start()
    //{
    //    int count = Enum.GetValues(typeof(EPeriodsPerDay)).Length;
    //    Debug.Log(count);

    //    EWeekDay day = (EWeekDay)6;

    //    Debug.Log(day);
    //    Debug.Log((int)day);
    //}

    public int Day()
    {
        return _step / GetEPeriodPerDayLength();
    }

    public int Period()
    {
        return _step % GetEPeriodPerDayLength();
    }

    private int GetEPeriodPerDayLength()
    {
        return Enum.GetValues(typeof(EPeriodsPerDay)).Length;
    }

    public EWeekDay WeekDay()
    {
        return (EWeekDay)((Day() + (int)_timeConfig.startWeekday) % 7);
    }

    public void Hop()
    {
        _step++;
        OnTimeChanged?.Invoke(Day(), Period(), WeekDay());
    }

    #region Test
    [ContextMenu("Hop")]
    public void HopTest()
    {
        Hop();
        Debug.Log(_step);
    }
    #endregion
}
