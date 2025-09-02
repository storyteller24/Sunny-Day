using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPath : MonoBehaviour
{
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    [SerializeField] private List<GameObject> pathPoints; // List of path GameObjects (P1, P2, etc.)
    int currentPathIndex = 0; // Index to track the current path                        
    GameObject currentPath; // Reference to the current path GameObject
    public float moveSpeed = 2f; // Speed at which the object moves towards the path
    public float coyoteTime = 1.5f; // Time to wait before moving to the next path

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component

        // Go up to Moving Platform, then up to Parent, then find "Paths"
        

        
            // Clear any existing paths and populate from hierarchy
       pathPoints = GetSiblings();


            // Sort paths by name to ensure correct order (P1, P2, etc.)
       pathPoints.Sort((a, b) => a.name.CompareTo(b.name));

            // Set the initial target path
       if (pathPoints.Count > 0)
       {
             currentPath = pathPoints[currentPathIndex];
       }
        
      
    }

    void Start()
    {
        if (currentPath != null)
        {
            MoveTo();
        }
  
    }

    private List<GameObject> GetSiblings()
    {
        List<GameObject> siblings = new List<GameObject>();

        if (transform.parent == null) return siblings; // no parent → no siblings

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform child = transform.parent.GetChild(i);

            // Skip if it's this object itself
            if (child == transform) continue;

            siblings.Add(child.gameObject);
        }

        return siblings;
    }


    private void MoveTo()
    {
        if (currentPath == null) return;

        var directionToPath = GetDirectionToPath(transform.position, currentPath.transform.position);
        rb.linearVelocity = directionToPath * moveSpeed; // Set velocity towards current path
    }

    Vector2 GetDirectionToPath(Vector2 point1, Vector2 point2)
    {
        var direction = point2 - point1; // Calculate direction vector
        return direction.normalized; // Return normalized direction
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == currentPath)
        {
            Vector2 stopPosition = currentPath.transform.position;
            StartCoroutine(WaitAndMoveToNextPath(stopPosition));            
        }
    }

    private IEnumerator WaitAndMoveToNextPath(Vector2 stopPosition)
    {
        yield return new WaitUntil(() => Vector2.Distance(transform.position, stopPosition) < 0.1f);
        rb.linearVelocity = Vector2.zero; // Stop movement
        yield return new WaitForSeconds(coyoteTime); // Wait at current path

        // Move to next path in sequence
        currentPathIndex++;
        if (currentPathIndex >= pathPoints.Count)
        {
            currentPathIndex = 0; // Loop back to first path
        }

        currentPath = pathPoints[currentPathIndex];
        MoveTo(); // Start moving to next path
    }
}