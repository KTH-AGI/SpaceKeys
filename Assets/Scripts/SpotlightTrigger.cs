using UnityEngine;

public class SpotlightTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // todo do score logic
        other.gameObject.SetActive(false);
    }
}
