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
}
