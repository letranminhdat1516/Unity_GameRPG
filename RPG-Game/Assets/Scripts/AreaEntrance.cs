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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SetPlayerPosition()
    {
    yield return new WaitForEndOfFrame(); // Đợi đến khi cảnh được tải hoàn toàn
    if (transitionName == PlayerController.instance.areaTransitionName)
    {
        PlayerController.instance.transform.position = transform.position;
    }
    }
}
