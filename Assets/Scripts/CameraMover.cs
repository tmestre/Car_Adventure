using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{



// VARIABLES

    [Header("Camera Properties")]
    [SerializeField] Transform target;                  // reference to player
    string targetName;                                  // string for target
    Transform thirdCameraPos;                           // position of "Camera Position" game object
    [SerializeField] Vector3 topCameraPos = new Vector3 (150, 150, -150);              // adjustable position of top down camera
    [SerializeField] byte followFactor = 10;            // speed of camera position adjustment
    [SerializeField] byte rotationFactor = 10;          // speed of camera rotation onto player
    [SerializeField] byte cameraFocusHeight = 3;        // where the camera focuses
    [SerializeField] byte cameraFOV = 22;               // orthographic size of camera
                             

    [Header("Camera Mode")]
    [SerializeField] bool thirdPersonCamera;            // 3rd person camera
    [SerializeField] bool topDownCamera;                // top down camera




// REFERENCES

    Camera mainCamera;                  // reference to camera
    Vector3 distanceToTarget;           // calculates line between camera to player



// GAME SETUP

    private void Start() {
        mainCamera = GetComponent<Camera>();
        targetName = target.name;
        thirdCameraPos = GameObject.Find($"/{targetName}/CamPos").transform;
    }



// LATE GAME LOOP

    private void FixedUpdate() {

        // switching between cameras
        if (thirdPersonCamera) {
            ThirdPersonMode();

        } else if (topDownCamera) {
            TopDownMode();
        }
    }



// BEHAVIOR

    // camera smoothly follows from behind
    private void ThirdPersonMode() {

        Vector3 adjustedTargetPos = new Vector3(0, cameraFocusHeight, 0) + target.position;
        distanceToTarget = adjustedTargetPos - transform.position;                                                            // calculates distance to player
        Quaternion targetRotation = Quaternion.LookRotation(distanceToTarget);                                              // uses that distance to determine point and look at it
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationFactor);         // smoothly move current rotation
        transform.position = Vector3.Lerp(transform.position, thirdCameraPos.position, Time.deltaTime * followFactor);      // moves position according to player's position

        mainCamera.orthographic = false;
    }

    // camera fixed in position, looks down
    private void TopDownMode() {

        transform.position = target.position + topCameraPos;                                                                // locks camera at a set distance to player
        distanceToTarget = target.position - transform.position;                                                            // calculates distance to player from set position
        Quaternion targetRotation = Quaternion.LookRotation(distanceToTarget);                                              // smoothly move current rotation 
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationFactor);         // moves position according to player's position

        mainCamera.orthographic = true;
        mainCamera.orthographicSize = cameraFOV;
    }

    private void UpdateTarget() {
        thirdCameraPos = GameObject.Find($"/{targetName}/CamPos").transform;
    }
}
