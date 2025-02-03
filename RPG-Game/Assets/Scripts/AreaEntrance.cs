using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;
    // Start is called before the first frame update
    void Start()
    {
     StartCoroutine(SetPlayerPosition());
     UIFade.instance.FadeFromBlack();
     // Set when go entrance sence fadingBetweenAreas to fasle
     GameManager.instance.fadingBetweenAreas = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SetPlayerPosition()
    {
    yield return new WaitForEndOfFrame(); // Wait sence loaded 
    if (transitionName == PlayerController.instance.areaTransitionName)
    {
        PlayerController.instance.transform.position = transform.position;
    }
    }
}
