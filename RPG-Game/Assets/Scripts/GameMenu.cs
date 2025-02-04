using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;
    private CharStats[] playerStats;

    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;
    public GameObject[] statusButtons;
    public Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEq, statusWpnPwr, statusArmrEq, statusArmrPwr, statusExp;
    public Image statusImage;

    public ItemBtn[] itemBtns;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDes, useBtnText;
    public static GameMenu instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
             // Debug.Log("Toggle Menu Button Pressed");
            if (theMenu.activeInHierarchy)
            {
                // theMenu.SetActive(false);
                // GameManager.instance.gameMenuOpen = false;
               //  Debug.Log("Menu closed");
               CloseMenu();
            }
            else {
                theMenu.SetActive(true);    
                UpdateMainStats(); 
                GameManager.instance.gameMenuOpen = true;    
                // Debug.Log("Menu opened and stats updated");   
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                
                nameText[i].text = playerStats[i].chartName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Level: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value =  playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
            }else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }
    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();
       //Debug.Log("WindowNumber: " + windowNumber);
        //Debug.Log("Window: " + (windows[windowNumber] != null ? windows[windowNumber].name : "null"));

        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
          //      Debug.Log("Toggled: " + windows[i].name);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }
    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }
    public void OpenStatus()
    {
        UpdateMainStats();
        // update the infortion that is show
        StatusChar(0);
        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].chartName;
        }
    }
    public void StatusChar(int selected){
        statusName.text = playerStats[selected].chartName;
        statusHP.text =""+playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text =""+playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defence.ToString();
        if (playerStats[selected].equippedWpn != "")
        {
            statusWpnEq.text = playerStats[selected].equippedWpn;
        }
        statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();
         if (playerStats[selected].equippedArmr != "")
        {
            statusArmrEq.text = playerStats[selected].equippedArmr;
        }
        statusArmrPwr.text = playerStats[selected].armPwr.ToString();
        statusExp.text = (playerStats[selected]
                            .expToNextLevel
                                [playerStats[selected].playerLevel]
                                    - playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }
 public void ShowItems()
    {
        GameManager.instance.SortItem();
        for (int i = 0; i < itemBtns.Length; i++)
        {
            itemBtns[i].buttonValue = i;

            if (GameManager.instance.itemHeld[i] != "")
            {
                itemBtns[i].buttonImage.gameObject.SetActive(true);
                itemBtns[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemHeld[i]).itemSpite;
                itemBtns[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            } else
            {
                itemBtns[i].buttonImage.gameObject.SetActive(false);
                itemBtns[i].amountText.text = "";
            }
        }

    }
    public void SelectedItem(Item newItem)
    {
        activeItem = newItem;

        if (activeItem.isItem)
        {
            useBtnText.text = "Use";
        }
        if (activeItem.isWeapon || activeItem.isArmor)
        {
             useBtnText.text = "Equip";
        }
        itemName.text = activeItem.itemName;
        itemDes.text = activeItem.description;
    }
} 



