using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 2f;
    private bool created = false;

    void Awake() {
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    void LoadFirstLevel() {
        SceneManager.LoadScene(1);
    }


    // Use this for initialization
    void Start() {
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    // Update is called once per frame
    void Update() {

    }
}
