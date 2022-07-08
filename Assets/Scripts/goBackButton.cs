using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goBackButton : MonoBehaviour
{
    public Button goBack;
    // Start is called before the first frame update
    void Start()
    {
        goBack.onClick.AddListener(goBackToMain);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void goBackToMain()
    {
        SceneManager.LoadScene(0);   
    }
}
