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
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x-450, gameObject.transform.position.y -300, -1000);
        gameObject.transform.LookAt(mousePosition);
    }
}
