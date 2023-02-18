using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvLWin : MonoBehaviour
{
  public int WinLvL;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (MoneyText.Coin == WinLvL)
        {
            MoneyText.Coin = 0;
            SceneManager.LoadScene(0);
        }   
    }
}
