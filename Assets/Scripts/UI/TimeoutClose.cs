using UnityEngine;
using System.Collections;

/*
    This class functions as a timed animation switch for the UI to exit its current state after a specified time
    Used primarily for simple splash screens, must have a correct gameobject set via editor for the next menu to open
*/

public class TimeoutClose : MonoBehaviour {
    
    
    public GameObject m_openNext;
    private ScreenManager manager;
    private float m_duration; // Duration of timer in seconds


	// Use this for initialization
	void Start () {
        m_duration = 1.2f;
        manager = GetComponentInParent<ScreenManager>();

        if(m_openNext)
        {
            StartCoroutine(CloseScreen());
        }
	}
	
	private IEnumerator CloseScreen()
    {
        yield return new WaitForSeconds(m_duration);
        manager.OpenMenu(m_openNext);
    }
}
