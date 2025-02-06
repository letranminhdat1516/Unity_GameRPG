using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;


    public string[] itemHeld;
    public int[] numberOfItems;
    public Item[] referenceItem;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
        SortItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }else
        {
            PlayerController.instance.canMove = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            AddItem("Blabla");

            RemoveItem("HP Potion");
            RemoveItem("blepp");
        }
    }
public Item GetItemDetails(string itemToGrab)
{
    if (referenceItem == null || referenceItem.Length == 0)
    {
        Debug.LogError("GetItemDetails: referenceItem chưa được khởi tạo hoặc rỗng!");
        return null;
    }

    itemToGrab = itemToGrab.Trim(); // Loại bỏ khoảng trắng thừa
    foreach (Item item in referenceItem)
    {
        if (string.Equals(item.itemName, itemToGrab, System.StringComparison.OrdinalIgnoreCase))
        {
            return item;
        }
    }
    Debug.LogWarning($"GetItemDetails: Không tìm thấy item '{itemToGrab}' trong referenceItem!");
    return null;
}
    public void SortItem()
    {
        bool itemAfterSpace = true;
        while (itemAfterSpace)
        {
            itemAfterSpace = false;
        
        for (int i = 0; i < itemHeld.Length-1   ; i++)
        {
            if (itemHeld[i] == "")
            {
                itemHeld[i] = itemHeld[i+1];
                itemHeld[i+1]= "";

                numberOfItems[i] = numberOfItems[i+1];
                numberOfItems[i+1] = 0;
                if (itemHeld[i] != "")
                {
                    itemAfterSpace = true; 
                }
            }
        }
        }
    }
    public void AddItem(string itemToAdd)
    {
        int newItemPositon = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemHeld.Length; i++)
        {
            if (itemHeld[i] == "" || itemHeld[i]==itemToAdd)
            {
                newItemPositon=i;
                i = itemHeld.Length;
                foundSpace = true;
            }
        }
        if (foundSpace)
        {
            bool itemExis = false;
            for (int i = 0; i < referenceItem.Length; i++)
            {
                if (referenceItem[i].itemName == itemToAdd)
                {
                    itemExis = true;
                    i = referenceItem.Length;
                }
            }
            if (itemExis)
            {
                itemHeld[newItemPositon] = itemToAdd;
                numberOfItems[newItemPositon]++;
            } else
            {
                Debug.LogError(itemToAdd + " Does not exits!!");
            }
            GameMenu.instance.ShowItems();
        }
    }
    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;
        for (int i = 0; i < itemHeld.Length; i++)
        {
            if (itemHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;

                i = itemHeld.Length;
            }
        }
        if (foundItem)
        {
            numberOfItems[itemPosition]--;
            if (numberOfItems[itemPosition] <= 0 )
            {
                itemHeld[itemPosition] = "";
            }
            GameMenu.instance.ShowItems();
        }else
        {
               Debug.LogError("Could not find "+ itemToRemove);
        }
    }
}
