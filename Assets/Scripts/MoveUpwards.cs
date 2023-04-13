using UnityEngine;

public class MoveUpwards : MonoBehaviour
{
    private float speed = 0.1f;
    
    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
