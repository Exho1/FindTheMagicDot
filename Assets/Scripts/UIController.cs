using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public Text TimerText;
	public float timer = 0.0f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
	

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
        TimerText.text = "Time: " + Mathf.Round(timer);
    }
	
	public void OnRestartButtonClick()
    {
      	SceneManager.LoadScene("OfflineScene");
    }
}
