
using UnityEngine;

public class Lightnings : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private BoxCollider2D boxCollider;

    public void Activator ( int angle)
    {
        transform.rotation = Quaternion.Euler(0,0,angle);
        particleSystem.Play();
        boxCollider.enabled = true;
    }
    public void Offer()
    {
        boxCollider.enabled = false;
    }
}
