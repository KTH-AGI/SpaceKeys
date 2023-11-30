using UnityEngine;
using UnityEngine.VFX;

public class SpawnShieldRipples : MonoBehaviour
{

    [SerializeField] private GameObject shieldRipples;

    private VisualEffect _shieldRipplesVFX;

    
    // Create Ripple Effect on collision
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Note")
        {
            var ripples = Instantiate(shieldRipples, transform) as GameObject;
            _shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
            _shieldRipplesVFX.SetVector3("SphereCenter", col.contacts[0].point);
            
            Destroy(ripples, 2);
        }
    }
}
