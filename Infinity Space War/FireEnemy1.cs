using UnityEngine;

public class FireEnemy1 : MonoBehaviour
{
    private BoundsCheck bndCheck;

    // Start is called before the first frame update
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }
}
