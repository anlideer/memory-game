using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class about : MonoBehaviour {

    public GameObject aboutBG;
    public GameObject closeInfo;

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
            aboutBG.SetActive(true);
            closeInfo.SetActive(true);
        }
    }
}
