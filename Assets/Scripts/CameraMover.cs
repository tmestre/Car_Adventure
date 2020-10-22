using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{



// VARIABLES

    [Header("Turn Reaction")]
    [SerializeField] Vector3 turnPos;           // amount of movement on x-axis
    float turnFactor;                           // factor based on user input
    Vector3 startPos;                           // idle position of camera

    [Header("Reverse Reaction")]
    [SerializeField] Vector3 reversePos;
    Quaternion reverseRot;
    Rigidbody _rigidbody;



// GAME SETUP

    void Start() {
        startPos = transform.localPosition;         // gets position of camera
        reverseRot = transform.rotation;
        _rigidbody = GetComponentInParent<Rigidbody>();
    }



// GAME LOOP

    void Update() {
        TurnMovement();
        ReverseView();
    }



// BEHAVIOR

    // moves camera to the side when player turns the car
    private void TurnMovement() {
        turnFactor = Input.GetAxis("Horizontal");           // -1 to 1 based on horizontal
        Vector3 offset = turnPos * Mathf.Clamp (turnFactor, -1, 1);              // creates offset based on factor
        transform.localPosition = startPos + offset;        // applies offset to position
    }

    // turns camera backward when player goes into reverse
    private void ReverseView() {
        // if (_rigidbody.velocity.magnitude <= -0.5) {


        // UNFINISHED
        if (Input.GetKey(KeyCode.Q)) { 
            reverseRot = Quaternion.Euler(0, 180, 0);
            transform.localRotation = reverseRot;
            transform.localPosition = reversePos;
        }
    }
}
