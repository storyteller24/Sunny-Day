using UnityEngine;

public class FindPath : MonoBehaviour
{
    private void Awake()
    {
        GameObject children = GameObject.FindGameObjectWithTag("Moving Platform");
        if (children != null)
        {
            transform.SetParent(children.transform);
            transform.localPosition = Vector3.zero;
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
}
