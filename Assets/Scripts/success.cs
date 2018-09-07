using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class success : MonoBehaviour {

    public Animation anim;
    float timer;
    bool timerSet = false;
    public GameObject gem;

	// Use this for initialization
	void Start () {
        anim.wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update () {
		if (GM.end && GM.IsSuccess)
        {
            anim.Play();
            
            if (!timerSet)
            {
                timerSet = true;
                timer = Time.time;
            }
            if (timer + 1 <= Time.time)
            {
                GM.animPlayed = true;
                gem.SetActive(false);
            }
            

        }
    }
}
