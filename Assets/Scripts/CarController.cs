using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class CarController : MonoBehaviour
{

// REFERENCES

    [Header("Wheel Colliders")]
    [SerializeField] WheelCollider[] wheelCollider = new WheelCollider[4];

    [Header("Wheel Transforms")]
    [SerializeField] Transform[] wheel = new Transform[4];

    [Header("Car Utilities")]
    [SerializeField] Transform hatch;
    

    // component references
    Rigidbody _rigidbody;
    AudioSource _audiosource;



// VARIABLES

    [Header("Handling Properties")]
    [SerializeField] float engineTorque = 35000f;
    [SerializeField] float brakePower = 8000f;
    [SerializeField] float maxSteer = 35f;
    [SerializeField] Transform centerOfMass;
    float steerFactor;

    

    // constants
    string horizontal = "Horizontal";
    string vertical = "Vertical";

// GAME SETUP

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
        _audiosource = GetComponent<AudioSource>();
    }



// PHYSICS LOOP

    void FixedUpdate() {
        SteerUpdate();
        Throttle();         // gain speed
        Steer();            // go left and right
        Brake();            // stop fully
    }



// GAME LOOP

    void Update() {
        WheelUpdate();          // wheel mesh gets updated
        EngineSound();          // car engine varies according to speed
    }



// PLAYER INPUT

    // left and right to steer
    private void Steer() {
        wheelCollider[0].steerAngle = Input.GetAxis(horizontal) * steerFactor;
        wheelCollider[1].steerAngle = Input.GetAxis(horizontal) * steerFactor;
    }

    // forward and backward to accelerate
    private void Throttle() {
        wheelCollider[2].motorTorque = Input.GetAxis(vertical) * engineTorque;
        wheelCollider[3].motorTorque = Input.GetAxis(vertical) * engineTorque;
    }

    // space to brake
    private void Brake() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            wheelCollider[2].brakeTorque = brakePower;
            wheelCollider[3].brakeTorque = brakePower;
        } else {
            wheelCollider[2].brakeTorque = 0f;
            wheelCollider[3].brakeTorque = 0f;
        }
    }



// BEHAVIOR

    private void SteerUpdate() {
        steerFactor = maxSteer - (0.2f * _rigidbody.velocity.magnitude);
    }



// VISUAL & AUDIO

    // updates wheel mesh according to wheel colliders
    private void WheelUpdate() {

        // cycles through all 4 wheels
        int[] index = { 0, 1, 2, 3 };
        foreach (int i in index) {

            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;

            wheelCollider[i].GetWorldPose(out pos, out rot);
            wheel[i].position = pos;
            wheel[i].rotation = rot;
        }
    }

    // highers engine pitch depending on speed
    private void EngineSound() {
        float engineSpeed = _rigidbody.velocity.magnitude;
        float enginePitch = _audiosource.pitch;

        enginePitch = Mathf.Clamp(1 + engineSpeed / 30, 1, 4);
        enginePitch = GearSoundChange(engineSpeed, enginePitch);
        _audiosource.pitch = enginePitch;
    }

    private static float GearSoundChange(float engineSpeed, float enginePitch) {

        // speed of car until next gear
        float firstGear = 20;
        float secondGear = 32;
        float thirdGear = 46;
        float fourthGear = 60;

        // amount of change in pitch per gear
        float nextGear = 0.4f;

        // to second gear
        if (engineSpeed >= firstGear && engineSpeed <= secondGear) {
            enginePitch = enginePitch - nextGear;
        } 

        // to third gear
        else if (engineSpeed >= secondGear && engineSpeed <= thirdGear) {
            enginePitch = enginePitch - (2 * nextGear);
        } 

        // to fourth gear
        else if (engineSpeed >= thirdGear && engineSpeed <= fourthGear) {
            enginePitch = enginePitch - (3 * nextGear);
        } 

        // to fifth gear
        else if (engineSpeed >= fourthGear) {
            enginePitch = enginePitch - (4 * nextGear);
        }

        return enginePitch;
    }
}
