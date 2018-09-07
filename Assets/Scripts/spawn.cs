using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spawn : MonoBehaviour {

    public GameObject hole;

    int holeNum;
    int i, j;
    Vector2Int tmp2;
    Vector3 tmp3;
    Dictionary<string, Vector2Int> holeBlock = new Dictionary<string, Vector2Int>();
    Dictionary<string, Vector3> holePos = new Dictionary<string, Vector3>();
    bool flag;
    bool locationRight = false;
    public static bool[,] reached = new bool[15, 6];
      

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetString("over", "success") == "success")
        {

            /************************************** SPAWN ************************************/
            flag = true;
            System.Random ran = new System.Random();

            // find possible solution
            while (flag)
            {
                flag = false;
                // a solution
                holeNum = ran.Next(4, 8);  // 4-7 holes

                for (i = 0; i < holeNum; i++)
                {
                    locationRight = false;

                    while (!locationRight)
                    {
                        locationRight = true;
                        tmp2.x = ran.Next(15);
                        tmp2.y = ran.Next(6);
                        if (tmp2 == GM.gemBlock || tmp2 == GM.PlayerBlock)
                        {
                            locationRight = false;
                        }
                        for (j = 0; j < i; j++)
                        {
                            if (tmp2 == holeBlock["hole" + j.ToString()])
                            {
                                locationRight = false;
                            }
                        }
                    }

                    holeBlock.Add("hole" + i.ToString(), tmp2);
                    GM.graph[tmp2.x, tmp2.y] = 3;
                }


                // see if it's possible for player to win
                // scan the graph
                if (!MyClass.IfPossible(GM.PlayerBlock.x, GM.PlayerBlock.y) && !MyClass.IfPossible(GM.gemBlock.x, GM.gemBlock.y))
                {
                    flag = true;
                    for (i = 0; i < holeNum; i++)
                    {
                        tmp2 = holeBlock["hole" + i.ToString()];
                        GM.graph[tmp2.x, tmp2.y] = 0;
                        holeBlock.Remove("hole" + i.ToString());
                    }
                }
                
            }
        
            // record & block location To pos
            PlayerPrefs.SetInt("holeNum", holeNum);
            for (i = 0; i < holeNum; i++)
            {
                tmp2 = holeBlock["hole" + i.ToString()];
                tmp3 = GM.blockToPos[tmp2.x, tmp2.y];
                holePos.Add("hole" + i.ToString(), tmp3);
                PlayerPrefs.SetFloat("hole" + i.ToString() + "X", tmp3.x);
                PlayerPrefs.SetFloat("hole" + i.ToString() + "Y", tmp3.y);
            }
        }
        else
        {
            holeNum = PlayerPrefs.GetInt("holeNum");
            for (i = 0; i < holeNum; i++)
            {
                tmp3.z = 0;
                tmp3.x = PlayerPrefs.GetFloat("hole" + i.ToString() + "X");
                tmp3.y = PlayerPrefs.GetFloat("hole" + i.ToString() + "Y");
                holePos.Add("hole" + i.ToString(), tmp3);
            }
        }

        /******************************** instantiate holes **********************************/
        for (i = 0; i < holeNum; i++)
        {
            Instantiate(hole, holePos["hole" + i.ToString()], transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

static class MyClass
{
    // 改成较弱的判断了，真要遍历完的话复杂度太高了
    public static bool IfPossible(int x, int y)
    {
        Vector2Int up = new Vector2Int(x, y - 1);
        Vector2Int down = new Vector2Int(x, y + 1);
        Vector2Int left = new Vector2Int(x - 1, y);
        Vector2Int right = new Vector2Int(x + 1, y);

        if (x == 0 && y == 0 && GM.graph[down.x, down.y] == 3 && GM.graph[right.x, right.y] == 3)
        {
            return false;
        }
        else if (x == 0 && y == 5 && GM.graph[up.x, up.y] == 3 && GM.graph[right.x, right.y] == 3)
        {
            return false;
        }
        else if (x == 0 && y != 0 && y != 5 && GM.graph[up.x, up.y] == 3 && GM.graph[down.x, down.y] == 3 && GM.graph[right.x, right.y] == 3)
        {
            return false;
        }
        
        else if (x == 14 && y == 0 && GM.graph[left.x, left.y] == 3 && GM.graph[down.x, down.y] == 3)
        {
            return false;
        }
        else if (x == 14 && y == 5 && GM.graph[left.x, left.y] == 3 && GM.graph[up.x, up.y] == 3)
        {
            return false;
        }
        else if (x == 14 && y != 0 && y != 5 && GM.graph[left.x, left.y] == 3 && GM.graph[up.x, up.y] == 3 && GM.graph[down.x, down.y] == 3)
        {
            return false;
        }
        else if (x != 0 && x != 14 && y != 0 && y != 5 && GM.graph[left.x, left.y] == 3 && GM.graph[right.x, right.y] == 3 && GM.graph[up.x, up.y] == 3 && GM.graph[down.x, down.y] == 3)
        {
            return false;
        }
        else
        {
            return true;
        }




    }



    /*
    public static bool IfPossible(int x, int y)
    {
        spawn.reached[x, y] = true;
        if (GM.graph[x, y] == 2)
        {
            return true;
        }
        else if (GM.graph[x, y] == 3)
        {
            return false;
        }

        // left 
        if (x != 0 && !spawn.reached[x - 1, y])
        {
            Debug.Log("left");
            if (IfPossible(x - 1, y))
            {
                return true;
            }
            else
            {
                spawn.reached[x - 1, y] = false;
            }
        }
        // right
        if (x != 14 && !spawn.reached[x + 1, y])
        {
            Debug.Log("right");
            if (IfPossible(x + 1, y))
            {
                return true;
            }
            else
            {
                spawn.reached[x + 1, y] = false;
            }
        }
        // up
        if (y != 0 && !spawn.reached[x, y - 1])
        {
            Debug.Log("up");
            if (IfPossible(x, y - 1))
            {
                return true;
            }
            else
            {
                spawn.reached[x, y - 1] = false;
            }
        }
        // down
        if (y != 5 && !spawn.reached[x, y + 1])
        {
            Debug.Log("down");
            if (IfPossible(x, y + 1))
            {
                return true;
            }
            else
            {
                spawn.reached[x, y + 1] = false;
            }
        }

        Debug.Log("return false");
        return false;
    }
    */


}