using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Collider))]
public class NoteTrigger : MonoBehaviour
{
    private void Start()
    {
        Assert.IsTrue(gameObject.GetComponent<Collider>().isTrigger);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*GameObject musicObject = other.gameObject;
        // todo do score logic
        musicObject.SetActive(false);
        Debug.Log("Music object deactivated");*/
        // var audioSource = note.GetComponent<AudioSource>();
        // AudioSource.PlayClipAtPoint(audioSource.clip, Camera.main.transform.position);          
        // Debug.Log("Music object audio played");
        
    }
}
