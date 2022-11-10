using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern.Observer;
public class HandController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnEnable()
    {
        EventDispatcher.AddListener(EventName.OnEndLine, GoOffRope);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveListener(EventName.OnEndLine, GoOffRope);
    }
    public void GoOffRope(EventName e, object obj)
    {
        Debug.Log("buonf tay");
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
