using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CameraStyle {Fixed, Free}

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public CameraStyle cameraStyle;
    public Transform pivot;
    public float rotationSpeed = 1f;

    private Vector3 offset;
    private Vector3 pivotOffset;
    
    void Start()
    {
        //set the offset of the camera based on the player
        offset = transform.position - player.transform.position;
        //The offset of the pivot from the player
        pivotOffset = pivot.position - player.transform.position;
        //The offset from the player
        offset = transform.position - player.transform.position;
    }

    
    void LateUpdate()
    {
        //get the cameras transform position to be that of the players transform position 
        transform.position = player.transform.position + offset; 

        //If we are using the fixed camera mode
        if (cameraStyle == CameraStyle.Fixed)
        {
            //Set the cameras position to be the players position plus the offset
            transform.position = player.transform.position + offset;
        }

        //If we are using the free camera mode
        if (cameraStyle == CameraStyle.Free)
        {
            //Make the pivot position follow the player
            pivot.transform.position = player.transform.position + pivotOffset;
            //Work out the angle from the mouse input as a queternion
            Quaternion turnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            //Modify the offset by the turn angle
            offset = turnAngle * offset;
            //Set the camera position to that of the pivot plus the offset
            transform.position = pivot.transform.position + offset;
            //Make the camera look at the pivot
            transform.LookAt(pivot);
        }
    }

    public enum CameraStyle { Fixed, Free }

    public class cameraController : MonoBehaviour
    {
        public GameObject player;
        public CameraStyle cameraStyle;
        public Transform pivot;
        public float rotationSpeed = 1f;

        private Vector3 offset;
        private Vector3 pivotOffset;
    }
}
