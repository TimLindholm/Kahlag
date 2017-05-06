using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    private InputScript _ir;

	void Start ()
    {
        _ir = (InputScript)FindObjectOfType(typeof(InputScript));
	}
	
	
	void Update ()
    {
		if(_ir.RestartLevel == true)
        {
            SceneManager.LoadScene(0);
        }
	}
}
