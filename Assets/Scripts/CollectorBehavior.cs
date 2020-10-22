using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class CollectorBehavior : MonoBehaviour
{
    [SerializeField] Transform collector;
    [SerializeField] GameObject item;
    [SerializeField] GameObject target;
    bool collectorFull = false;
    


    void Start() {

    }

    void Update() {
        ItemIntoHatch();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Deliver Target" && collectorFull) {
            item.SendMessage("DeliveredItem");
            collectorFull = false;
        }

        if (other.gameObject.tag == "Collect" && !collectorFull) {
            item = other.gameObject;
            collectorFull = true;
            item.SendMessage("PlayGetItem");
        }
    }

    private void ItemIntoHatch() {
        if (collectorFull) {
            item.transform.position = collector.position;
        }
    }
}
