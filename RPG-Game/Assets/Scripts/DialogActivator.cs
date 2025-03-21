using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] lines;
    private bool canActivate;
    public bool isPerson = true;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // show dialog box next line we set
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDiaLog(lines, isPerson);
        }
    }
    private void OnTriggerEnter2D(Collider2D orther)
    {
        if (orther.tag == "Player")
        {
            canActivate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D orther)
    {
        if (orther.tag == "Player")
        {
            canActivate = false;
        }
    }
}
