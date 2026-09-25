using UnityEngine;

public class TimeHopTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.Hop();
        }
    }
}
