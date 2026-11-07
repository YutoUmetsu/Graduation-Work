using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TitleSceneMove()
    {
        SceneManager.LoadScene("TestTitle");
    }

    public void PlaySceneMove()
    {
        SceneManager.LoadScene("TestMainNew");
    }
    public void ResultSceneMove()
    {
        SceneManager.LoadScene("TestMainNew");
    }

}
