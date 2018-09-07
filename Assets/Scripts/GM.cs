using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour {

    public static Vector2Int PlayerBlock = new Vector2Int();
    public static bool mouseDown = false;
    public static Vector3 currentPos = new Vector3();
    public static Vector2Int gemBlock = new Vector2Int();
    public static Vector3 gemPos = new Vector3();
    public static bool end = false;
    public static bool animPlayed = false;
    public static bool IsSuccess = false;
    

    public static Vector3[,] blockToPos = new Vector3[15, 6];
    public static int[,] graph = new int[15, 6];    // 0-nothing, 1-player, 2-gem, 3-hole
    

    public GameObject UIgem;
    public AudioSource deadAudio;
    public GameObject[] intros;
    public Text win;


    // Use this for initialization
    void Start() {
        end = false;
        animPlayed = false;
        IsSuccess = false;
        mouseDown = false;

        UIgem.SetActive(false);
        win.gameObject.SetActive(false);
        Screen.SetResolution(Screen.width, Screen.height, true);

        if (PlayerPrefs.GetString("over", "success") == "success")
        {
            System.Random ran = new System.Random();
            PlayerBlock.x = ran.Next(15);
            PlayerBlock.y = ran.Next(6);
            gemBlock.x = ran.Next(15);
            gemBlock.y = ran.Next(6);
            while (PlayerBlock == gemBlock)
            {
                gemBlock.x = ran.Next(15);
                gemBlock.y = ran.Next(6);
            }
            PlayerPrefs.SetInt("PlayerBlockX", PlayerBlock.x);
            PlayerPrefs.SetInt("PlayerBlockY", PlayerBlock.y);
            PlayerPrefs.SetInt("gemBlockX", gemBlock.x);
            PlayerPrefs.SetInt("gemBlockY", gemBlock.y);
        }
        else
        {
            PlayerBlock.x = PlayerPrefs.GetInt("PlayerBlockX");
            PlayerBlock.y = PlayerPrefs.GetInt("PlayerBlockY");
            gemBlock.x = PlayerPrefs.GetInt("gemBlockX");
            gemBlock.y = PlayerPrefs.GetInt("gemBlockY");
        }

        if (PlayerPrefs.GetString("played", "no") == "no")
        {
            foreach(GameObject intro in intros)
            {
                intro.SetActive(true);
            }
        }


    }

    // Update is called once per frame
    void Update () {

        if (end && IsSuccess)
        {
            UIgem.SetActive(true);
            PlayerPrefs.SetString("over", "success");
        }
        else if (end && !IsSuccess)
        {
            deadAudio.Play();
        }

        if (animPlayed && IsSuccess)
        {
            UIgem.SetActive(false);
            win.gameObject.SetActive(true);
        }
        
	}
}
