using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemBtn : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


    }
    public void Press()
    {
        if (GameManager.instance.itemHeld[buttonValue] != "")
        {
            GameMenu.instance.SelectedItem(GameManager.instance.GetItemDetails(GameManager.instance.itemHeld[buttonValue]));
        }

    }
}
