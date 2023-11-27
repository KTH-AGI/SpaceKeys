using UnityEngine;

public class SpotlightTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject note = other.gameObject;
        // todo do score logic
        note.SetActive(false);
        var audioSource = note.GetComponent<AudioSource>();
        
        AudioSource.PlayClipAtPoint(audioSource.clip, Camera.main.transform.position);          
        Debug.Log("Music object audio played");
        
    }
}
