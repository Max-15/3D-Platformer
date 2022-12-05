using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    InputManager im;
    private void Start() {
        im = InputManager.Instance;
    }
    void Update()
    {
        if(im.PauseThisFrame()){
            Trinitrotoluene.Functions.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
