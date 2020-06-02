using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("UI Manager is null");
            }
            return _instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImg;
    public Text gemCountText;
    public Image[] lives;
    
    public void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = ""+gemCount +"G";
    }

    public void UpdateShopSelection(int ypos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, ypos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int livesRemaning)
    {
        lives[livesRemaning].enabled = false;
        
           
    }
  


}
