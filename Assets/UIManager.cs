using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject GameWinPanel;
    public GameObject GameLoosePanel;

    // Start is called before the first frame update
    void Start()
    {
        GameWinPanel.SetActive(false);
        GameLoosePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { if(CardManager.instance.score >= 30)
        {
            Winpanel();
        }
    if(CardManager.instance.score <= -20)
        {
            LoosePanel();
        }
        
    }
    public void Winpanel()
    {
        GameWinPanel.SetActive(true);
        GameLoosePanel.SetActive(false);
    }
    public void LoosePanel()
    {
        GameLoosePanel.SetActive(true);
        GameWinPanel.SetActive(false);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
