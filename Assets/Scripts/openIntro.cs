using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openIntro : MonoBehaviour {

    public GameObject[] intros;

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
            PlayerPrefs.SetString("played", "no");
            foreach(GameObject intro in intros)
            {
                intro.SetActive(true);
            }
        }
    }
}
