using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Picker"))
        {
            ScoreCalculator.Instance.NextScore();
        }
    }
}
