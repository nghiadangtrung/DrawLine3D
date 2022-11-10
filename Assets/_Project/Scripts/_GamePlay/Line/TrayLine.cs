using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern.Observer;
using DG.Tweening;
public class TrayLine : MonoBehaviour
{
    // Start is called before the first frame update
    DataFinger dataFinger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 newfingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("vi tri " + gameObject.transform.position);
        }
    }
    private void OnEnable()
    {
        //EventDispatcher.AddListener(EventName.OnStartDraw, OnStartDraw);
        EventDispatcher.AddListener(EventName.OnCompleteDraw, OnDrawComplete);
    }
    private void OnDisable()
    {
        //EventDispatcher.RemoveListener(EventName.OnStartDraw, OnStartDraw);
        EventDispatcher.RemoveListener(EventName.OnCompleteDraw, OnDrawComplete);
    }
    public void OnStartDraw(EventName e, object obj)
    {
        if (obj is DataFinger)
        {
            dataFinger = obj as DataFinger;
            gameObject.transform.position = dataFinger.posInput[0];
        }

    }
    public void OnDrawComplete(EventName e, object obj)
    {
        if (obj is DataFinger)
        {
            dataFinger = obj as DataFinger;
            gameObject.transform.position = dataFinger.posInput[0];
        }
        this.transform.DOPath(dataFinger.posInput.ToArray(), dataFinger.time).SetEase(Ease.Linear);
    }
    
}
