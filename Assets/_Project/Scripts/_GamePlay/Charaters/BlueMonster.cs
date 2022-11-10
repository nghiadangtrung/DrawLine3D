using Pattern.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMonster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        EventDispatcher.AddListener(EventName.OnStartDraw, OnStartDraw);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveListener(EventName.OnStartDraw, OnStartDraw);
    }
    public void OnStartDraw(EventName e, object obj)
    {
        if(obj is DataFinger)
        {
            var h = obj as DataFinger;
            Debug.Log("adad");
            Debug.Log(h.posInput);
        }
        
    }
    public void OnDrawComplete(EventName e, object obj)
    {

    }
}
