using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTeleporter : MonoBehaviour
{
    [SerializeField] byte selectLevel;

    void Start() {
        
    }

    private void OnTriggerEnter(Collider other) {
        SceneManager.LoadScene(selectLevel);
    }
}
