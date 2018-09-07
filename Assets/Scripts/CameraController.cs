using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{

    public AudioSource mouseOver;
    public Transform blocks;
    int i, j;
    string currentName;
    public GameObject[] intros;



    // Use this for initialization
    void Start()
    {
        float playerX = GM.PlayerBlock.x;
        float playerY = GM.PlayerBlock.y;

        i = j = 0;

        foreach (Transform blockline in blocks)
        {
            foreach (Transform block in blockline)
            {
                if (i == playerX && j == playerY)
                {
                    block.gameObject.SetActive(false);
                    GM.currentPos = block.position;
                    currentName = block.name;
                    GM.graph[i, j] = 1;
                }
                else if (i == GM.gemBlock.x && j == GM.gemBlock.y)
                {
                    GM.gemPos = block.position;
                    GM.graph[i, j] = 2;
                }
                else
                {
                    GM.graph[i, j] = 0;
                }
                GM.blockToPos[i, j] = block.position;
                j++;
            }
            i++;
            j = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GM.end) { 
        if (Input.GetMouseButton(0))
        {
            GM.mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GM.mouseDown = false;
        }
        if (GM.mouseDown)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider)
                {
                    foreach (Transform blockline in blocks.transform)
                    {
                        foreach (Transform block in blockline)
                        {
                            if (hit.collider.gameObject == block.gameObject)
                            {
                                j = block.name[block.name.Length - 2] - '0';
                                if (block.name[6] == ' ')
                                {
                                    i = block.name[5] - '0';
                                }
                                else
                                {
                                    i = 10 * (block.name[5] - '0') + (block.name[6] - '0');
                                }

                                string upBlock = "block" + i.ToString() + " (" + (j - 1).ToString() + ')';
                                string downBlock = "block" + i.ToString() + " (" + (j + 1).ToString() + ')';
                                string leftBlock = "block" + (i - 1).ToString() + " (" + j.ToString() + ')';
                                string rightBlock = "block" + (i + 1).ToString() + " (" + j.ToString() + ')';


                                if (upBlock == currentName || downBlock == currentName || leftBlock == currentName || rightBlock == currentName)
                                {
                                    block.gameObject.SetActive(false);
                                    mouseOver.Play();
                                    currentName = block.name;
                                    GM.currentPos = block.position;
                                    if (PlayerPrefs.GetString("played", "no") == "no")
                                    {
                                        PlayerPrefs.SetString("played", "yes");
                                        foreach (GameObject intro in intros)
                                        {
                                            intro.SetActive(false);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

    }

}