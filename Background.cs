using UnityEngine;

public class Background : MonoBehaviour
{
  public float speed = 4f;
  private Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
    }

    
    void FixedUpdate()
    {
        transform.Translate(translation:Vector3.down*speed*Time.deltaTime);
        if(transform.position.y < -127.8f)
        {
            transform.position = StartPosition;
        }
    }

}
