using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class closeInfo : MonoBehaviour {

    public GameObject closeAbout;
    public GameObject aboutBG;

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
            closeAbout.SetActive(false);
            aboutBG.SetActive(false);
        }
    }

}
