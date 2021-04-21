
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MovePhone : MonoBehaviour
{
   
    private Rigidbody2D rb;
    private float dirX;
    private float moveSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
        rb.velocity = new Vector2(dirX,0f );
    }
}
