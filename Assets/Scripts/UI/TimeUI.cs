using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayLabel;
    [SerializeField] private TextMeshProUGUI _peroidLabel;
    [SerializeField] private TextMeshProUGUI _weekdayLabel;

    private void OnValidate()
    {
        if (_dayLabel == null)
        {
            Debug.LogWarning($"You didn't reference the {nameof(_dayLabel)} yet!", this);
        }

        if (_dayLabel == null)
        {
            Debug.LogWarning($"You didn't reference the {nameof(_peroidLabel)} yet!", this);
        }

        if (_dayLabel == null)
        {
            Debug.LogWarning($"You didn't reference the {nameof(_weekdayLabel)} yet!", this);
        }
    }

    private void OnEnable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged += UpdateTimeUI;
        }
    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged -= UpdateTimeUI;
        }
    }

    private void Start()
    {
        UpdateTimeUI(TimeManager.Instance.Day(), TimeManager.Instance.Period(), TimeManager.Instance.WeekDay());
    }

    private void UpdateTimeUI(int day, int period, EWeekDay weekday)
    {
        //Debug.Log($"{day} days");
        //Debug.Log($"The period is: {period}");
        //Debug.Log($"The weekday is:{(EWeekDay)weekday}");

        _dayLabel.text = day + "Days";
        _peroidLabel.text = ((EPeriodsPerDay)period).ToString();
        _weekdayLabel.text = ((EWeekDay)weekday).ToString();
    }
}
    