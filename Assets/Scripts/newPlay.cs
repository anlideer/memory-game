﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.SetString("over", "success");
            GM.end = true;

            SceneManager.LoadScene(0);
        }
    }
}
