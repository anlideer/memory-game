using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 player3;
    public Animator deadAnimator;
    bool deadPlayed = false;
    float timer;

	// Use this for initialization
	void Start () {
        deadPlayed = false;
        player3.x = GM.currentPos.x;
        player3.y = GM.currentPos.y;
        player3.z = 0;
        transform.position = player3;
        deadAnimator.speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GM.end)
        {
            if (transform.position.x != GM.currentPos.x)
            {
                player3.x = GM.currentPos.x;
                
            }
            else if (transform.position.y != GM.currentPos.y)
            {
                player3.y = GM.currentPos.y;
               
            }

            if (transform.position != player3)
            {
                transform.Translate(player3 - transform.position);
            }
        }
        else if (GM.end && !GM.IsSuccess)
        {
            
            if (!deadPlayed)
            {
                deadAnimator.Play("dead");
                deadPlayed = true;
                deadAnimator.speed = 1f;
                timer = Time.time;
            }
            else if (deadPlayed && timer + 0.17f <= Time.time)
            {
                deadAnimator.speed = 0f;
            }
            
        }
        

	}
}
