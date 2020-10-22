using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchController : MonoBehaviour
{

// VARIABLES

    [SerializeField] AudioClip hatchOpenSound;
    [SerializeField] AudioClip hatchCloseSound;

    public bool hatchOpen = false;
    float hatchCycle;

    AudioSource _audiosource;




    // GAME SETUP

    void Start() {
        _audiosource = GetComponent<AudioSource>();
    }



// GAME LOOP
    
    void Update() {
        HatchControl();
    }



// BEHAVIOUR

    // space to open hatch
    private void HatchControl() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            HatchToggle();
        }

        // when shift is pressed, time gets added to hatchCycle, clamped at 1
        // when shift is released, time gets removed, clamped at 0
        // depending on hatchCycle, hatch rotates between the two positions
        // lerp = linear interpolation

        if (hatchOpen) {
            hatchCycle = Mathf.Clamp(hatchCycle + Time.deltaTime, 0, 1.5f);
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(70, 0, 0), hatchCycle);

        } else if (!hatchOpen) {
            hatchCycle = Mathf.Clamp(hatchCycle - Time.deltaTime / 1.5f, 0, 1);
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(70, 0, 0), hatchCycle);
        }
    }

    private void HatchToggle() {
        hatchOpen = !hatchOpen;
        if (hatchOpen) {
            _audiosource.PlayOneShot(hatchOpenSound);
        } else if (!hatchOpen) {
            _audiosource.PlayOneShot(hatchCloseSound);
        }
    }
}
