using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private int moveDistance;
    [SerializeField] private LayerMask mask;
    [SerializeField] private int side; //0: move on x axis, 1: move on z axis
    private Camera mainCam;

    private Vector3 currentPoint;
    private bool isDragging;
    private void Awake()
    {
        this.mainCam = Camera.main;
        currentPoint = Vector3.one * 999;
    }

    private void Update()
    { 
        if(Input.GetMouseButtonDown(0)){
            var ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out var hit, 100, mask)){
                this.isDragging = true;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log(Input.mousePosition);
            if(this.isDragging){
                var ray = mainCam.ScreenPointToRay(Input.mousePosition);
                var pointOnPlane = GetPointOnPlane(ray);
                if(currentPoint != Vector3.one * 999){
                    var delta = pointOnPlane - currentPoint;
                    delta = delta.Set(y: 0);
                    transform.position += delta;
                }
                currentPoint = pointOnPlane;
            }
        }
        if(Input.GetMouseButtonUp(0)){
            this.currentPoint = Vector3.one * 999;
            isDragging = false;
        }
    }

    private Vector3 GetPointOnPlane(Ray ray){
        if(side == 0){
            var z = Mathf.Abs(ray.origin.z);
            var angle = Vector3.Angle(ray.direction, Vector3.forward);
            var cos = Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad));
            var rayLength = z / cos;
            return ray.origin + ray.direction.normalized * rayLength;
        }
        else{
            var x = Mathf.Abs(ray.origin.x);
            var angle = Vector3.Angle(ray.direction, Vector3.right);
            var cos = Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad));
            var rayLength = x / cos;
            //Debug.Log((x, angle, cos, rayLength, ray.origin));
            return ray.origin + ray.direction.normalized * rayLength;
        }
    }
}
