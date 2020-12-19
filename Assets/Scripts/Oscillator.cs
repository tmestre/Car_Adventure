using UnityEngine;

[DisallowMultipleComponent]         // component can't be added more than once
public class Oscillator : MonoBehaviour
{

// VALUES
    [SerializeField] Vector3 movePos;            // how much the position is going to move
    [Range(0, 1)] [SerializeField] float moveFactor;          // slider from 0 to 1
    Vector3 startPos;           // gameobject's initial position
    [SerializeField] float period = 0.2f;         // duration of movement

// GAME SETUP

    void Start() {
        startPos = transform.position;          // assigning starting position
    }

// GAME LOOP

    void Update() {
        float cycle = Time.time * period;           // seconds divided by period; grows continuously (1 cycle = 0.5 seconds)

        Vector3 offset = movePos * moveFactor;          // matches slider with end position
        transform.position = startPos + offset;         // changes gameobject's position

        const float tau = Mathf.PI * 2;         // around 6.28; one sin wave
        float sinWave = Mathf.Sin(cycle * tau);         // sin wave goes from 0, 1, 0, -1 in one second
        moveFactor = sinWave / 2f + 0.5f;           // shifting the sin wave to fit in the range from 0 to 1
    }
}