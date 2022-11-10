using Pattern.Observer;
using UnityEngine;

public class Level : MonoBehaviour
{
    [ReadOnly] public int BonusMoney;
    private void Start()
    {
        EventDispatcher.Dispatch(EventName.OnMapLevelInitComplete);
    }

    private void OnDestroy()
    {
        
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    public void OnWinGame()
    {
        
    }

    public void OnLoseGame()
    {
        
    }
    
}
