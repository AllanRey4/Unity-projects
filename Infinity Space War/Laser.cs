
using UnityEngine;

public class Laser : MonoBehaviour
{
   

    private void Awake()
    {
      
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
        }
       
    }
}
