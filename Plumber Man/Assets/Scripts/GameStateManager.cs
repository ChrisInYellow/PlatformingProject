using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameStateManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        SceneTransitions();
	}

    public void SceneTransitions()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            {
            Application.Quit();
        }
    }
}
