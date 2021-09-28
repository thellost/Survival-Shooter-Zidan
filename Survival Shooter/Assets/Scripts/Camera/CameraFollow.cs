using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     public Transform target;
    public float smoothing = 5f;
    private Camera cam;
    [SerializeField] float XThreshold;
    [SerializeField] float ZThreshold;


    Vector3 offset;

    private void Start()
    {
        //Mendapatkan offset antara target dan camera
        offset = transform.position - target.position;
        cam = this.gameObject.GetComponent<Camera>();
        Debug.Log(offset);
    }
    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position;
        //Menapatkan posisi untuk camera
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetCamPos = target.position + offset;


        //ini digunakan agar camera juga mem follow cursor namun di batasi oleh threshold   
        targetCamPos = (targetCamPos + mousePos) / 2f;

        targetCamPos.x = Mathf.Clamp(targetCamPos.x, -XThreshold + targetPosition.x, XThreshold + targetPosition.x);
        targetCamPos.z = Mathf.Clamp(targetCamPos.z, -ZThreshold, ZThreshold);
        
        targetCamPos.y = 15;
        //set posisi camera dengan smoothing
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
