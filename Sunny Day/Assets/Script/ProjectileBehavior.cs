using UnityEngine;

public class EProjectileBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false); // Deactivate the projectile
        }
        else if (collision.CompareTag("Obstacles"))
        {
            gameObject.SetActive(false); // Deactivate the projectile
        }
    }
    void OnBecameInvisible()
    {
        gameObject.SetActive(false); // return to pool instead if needed
    }
}
