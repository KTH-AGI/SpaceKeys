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
        if (other.gameObject.tag == "Note")
        {
            GameObject note = other.gameObject;
            // todo do score logic
            note.SetActive(false);
            var audioSource = note.GetComponent<AudioSource>();
            
            AudioSource.PlayClipAtPoint(audioSource.clip, Camera.main.transform.position);          
            Debug.Log("Music object audio played");
        }
        
    }
}
