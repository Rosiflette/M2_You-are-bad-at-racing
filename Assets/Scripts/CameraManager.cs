using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float offsetX; 
    [SerializeField] private Transform car;
    [SerializeField] private float step;

    private Vector3 basePosition = new Vector3(0,18,-22);

    Vector3 offset;
    Vector3 roadDirection = Vector3.left;

    void Start()
    {
        basePosition = transform.localPosition;
        offset = roadDirection;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 offset = gameObject.GetComponent<PositionConstraint>().translationOffset;
        // if(gameObject.GetComponent<Camera>().WorldToScreenPoint(car.position).y > gameObject.GetComponent<Camera>().pixelWidth/2){
        //     gameObject.GetComponent<PositionConstraint>().translationOffset = new Vector3(offsetX, offset.y, offset.z);
        // }
        // else{
        //     gameObject.GetComponent<PositionConstraint>().translationOffset = new Vector3(-offsetX, offset.y, offset.z);
        // }
        //print(car.position);
        //Vector3 newPosition = car.position + basePosition + (car.forward *5);
        //gameObject.transform.position = newPosition;
        transform.localPosition = car.localPosition + basePosition;
        // if(Vector3.Dot(roadDirection, car.forward) > 0){
        //     offset = offset.magnitude < roadDirection.magnitude ? offset+car.forward*step : offset;
        // }
        // else{
        //     offset = offset.magnitude < roadDirection.magnitude ? offset-car.forward*step : offset;
        // }
        // gameObject.transform.position = car.position + basePosition + offset * 5;
    }
}












