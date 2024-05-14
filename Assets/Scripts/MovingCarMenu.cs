using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCarMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookPosition = new Vector3(mousePosition.x - 50, mousePosition.y - 10, 0);
        gameObject.transform.LookAt(lookPosition);
    }
}
