using UnityEngine;

[CreateAssetMenu(fileName = "NewTimeConfig", menuName = "ConfigAsset/TimeConfig")]
public class TimeConfigSO : ScriptableObject
{
    public string[] periodNames = new string[] { "Morning", "Afternoon", "Evening" };
    public EWeekDay startWeekday = EWeekDay.Monday;
}