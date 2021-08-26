using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayFabManager playFabManager;
 public Text scoretxt;
   public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            scoretxt.text=(int.Parse(scoretxt.text)+1).ToString();
        }
    }
    public void UpdateLeaderBoard()
    {
        playFabManager.SendLeadBoard(int.Parse(scoretxt.text));
    }
}
