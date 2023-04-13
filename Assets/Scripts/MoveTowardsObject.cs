using UnityEngine;

public class MoveTowardsObject : MonoBehaviour
{
    public Transform targetObject;
    private float speed = 0.6f;
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetObject.position, speed * Time.deltaTime);
    }
}
