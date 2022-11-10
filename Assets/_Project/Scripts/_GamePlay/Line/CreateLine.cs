using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Pattern.Observer;
using UnityEngine.EventSystems;

public class CreateLine : MonoBehaviour
{
    [SerializeField] GameObject line;
    [SerializeField] GameObject CurrentLine;
    private LineRenderer lineRenderer;
    [SerializeField] List<Vector3> fingerPositions;
    [SerializeField] float offsetTime = 0.08f;
    bool StartDraw = false;
    [SerializeField] bool canDraw = false;
    public Camera camlevel;
    //public GameObject Tray;
    //public GameObject Player;
    //[SerializeField] Camera camForDraw;
    void Start()
    {

    }
    private void OnEnable()
    {
        EventDispatcher.AddListener(EventName.OnMapLevelInitComplete, OnMapLevelInitComplete);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveListener(EventName.OnMapLevelInitComplete, OnMapLevelInitComplete);
    }
    void OnMapLevelInitComplete(EventName e, object data)
    {
        canDraw = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (canDraw == false) return;
        if (IsPointerOverUIObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("buong day " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            fingerPositions.Clear();
            Debug.Log("zzz"+ Input.mousePosition.z);
            Vector3 inputCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            fingerPositions.Add(new Vector3(inputCam.x, inputCam.y,0));
            DataFinger data = new DataFinger();
            data.posInput = fingerPositions;
            Debug.Log("dispart");
            EventDispatcher.Dispatch(EventName.OnStartDraw,data);
            //Tray.transform.position = fingerPositions[0];
        }
        if(Input.GetMouseButton(0))
        {
            Vector2 newfingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 tmp = fingerPositions[fingerPositions.Count - 1];
            
            if ((newfingerPos - tmp).magnitude > 0.15f)
            {
                /*Debug.Log((newfingerPos - tmp).magnitude);
                Debug.Log(fingerPositions.Count);*/
                if (!StartDraw)
                {
                    Drawline();
                    StartDraw = true;
                }
                else
                {
                    List<Vector2> test =  CaculatorPos(tmp, newfingerPos, 0.2f);
                    foreach(var x in test)
                    {
                        UpdateLine(x);
                    }
                    //UpdateLine(newfingerPos);
                }

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            
            StartDraw = false;
            canDraw = false;
            int count = fingerPositions.Count;
            float time = count * offsetTime;
            DataFinger data = new DataFinger();
            data.posInput = fingerPositions;
            data.time = time;
            EventDispatcher.Dispatch(EventName.OnCompleteDraw,data);
            DOTween.Sequence().SetDelay(time).OnComplete(() =>
            {
                EventDispatcher.Dispatch(EventName.OnEndLine);
            });
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Drawline()
    {
        if(CurrentLine != null)
        {
            Destroy(CurrentLine);
        }
        CurrentLine = Instantiate(line, Vector3.zero, Quaternion.identity);
        lineRenderer = CurrentLine.GetComponent<LineRenderer>();
        Vector3 inputCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPositions.Add(new Vector3(inputCam.x, inputCam.y, 0));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);


    }
    public void UpdateLine(Vector2 point)
    {
        RaycastHit hit;
        Physics.Linecast(point, fingerPositions[fingerPositions.Count - 1], out hit);
        if(hit.collider!=null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
        
        fingerPositions.Add(point);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);

    }
    List<Vector2> CaculatorPos(Vector2 posStart, Vector2 posDis, float minDistance)
    {
        List<Vector2> tmp = new List<Vector2>();
        float distanceCurrent = (posDis - posStart).magnitude;
        Vector2 huong = posDis - posStart;
        Vector2 newpos = posStart;

        int chia = (int)(distanceCurrent / minDistance);
        while (chia > 1)
        {
            newpos = newpos + huong * (minDistance / distanceCurrent);
            tmp.Add(newpos);
            chia = chia - 1;
        }
        tmp.Add(posDis);
        return tmp;
    }
    private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.gameObject.name);
        if(results.Count > 0)
        {
            Debug.Log(results[0].gameObject.name);
        }
        
        return results.Count > 0; 
    }




}
