using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentItem;
    public int currentItemCost;
    private Player player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
             player = other.GetComponent<Player>();
            if(player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch(item)
        {

            case 0:UIManager.Instance.UpdateShopSelection(69);
                 currentItem = 0;
                 currentItemCost = 200;
                 break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-32);
                currentItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-121);
                currentItem = 2;
                currentItemCost = 100;
                break;


        }
       
    }

    public void BuyItem()
    {
        if (player.diamonds>=currentItemCost)
        {
            if(currentItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            if(currentItem == 0)
            {
                player.flameSword = true;
            }
            player.diamonds -= currentItemCost;
            UIManager.Instance.OpenShop(player.diamonds);
            UIManager.Instance.UpdateGemCount(player.diamonds);
            shopPanel.SetActive(false);
        }
        else
        {
            shopPanel.SetActive(false);
        }

    }
}
