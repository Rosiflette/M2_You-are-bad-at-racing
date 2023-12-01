using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float offsetX; 
    [SerializeField] private Transform car;

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = gameObject.GetComponent<PositionConstraint>().translationOffset;
        if(gameObject.GetComponent<Camera>().WorldToScreenPoint(car.position).y < gameObject.GetComponent<Camera>().pixelWidth){
            gameObject.GetComponent<PositionConstraint>().translationOffset = new Vector3(offsetX, offset.y, offset.z);
        }
        else{
            gameObject.GetComponent<PositionConstraint>().translationOffset = new Vector3(-offsetX, offset.y, offset.z);
        }
    }
}
