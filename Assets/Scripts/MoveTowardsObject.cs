using UnityEngine;

public class MoveTowardsObject : MonoBehaviour
{
    public Transform targetObject;
    [SerializeField] private float speed = 1f;
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetObject.position, speed * Time.deltaTime);
    }
}
