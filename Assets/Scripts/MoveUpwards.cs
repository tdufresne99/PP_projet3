using UnityEngine;

public class MoveUpwards : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    
    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
