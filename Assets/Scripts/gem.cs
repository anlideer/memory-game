using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour {

    public AudioSource gainGemAudio;

	// Use this for initialization
	void Start () {
        transform.position = GM.gemPos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GM.end = true;
            GM.IsSuccess = true;
            gainGemAudio.Play();
        }
    }
}
