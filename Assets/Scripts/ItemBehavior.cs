using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

// REFERENCES

    [SerializeField] AudioClip getItem;
    [SerializeField] AudioClip giveItem;
    AudioSource _audiosource;
    MeshRenderer _meshrenderer;



// VARIABLES

    bool usedItem = false;



// GAME SETUP

    void Start() {
        _audiosource = GetComponent<AudioSource>();
        _meshrenderer = GetComponent<MeshRenderer>();
    }



// COLLISIONS

    private void OnTriggerEnter() {
        if (!usedItem) {
            UsingItem();
        }
    }

    private void PlayGetItem() {
        _audiosource.PlayOneShot(getItem);
    }




    // GAME LOOP

    void Update() {
        
    }



// BEHAVIOR

    private void UsingItem() {
        if (!usedItem) {
            usedItem = true;
        }
    }

    private void DeliveredItem() {
        _meshrenderer.enabled = false;
        _audiosource.PlayOneShot(giveItem);
        Invoke("RemoveItem", 0.5f);
    }

    private void RemoveItem() {
        Destroy(this.gameObject);
    }

}
