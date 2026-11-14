using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutoreal : MonoBehaviour
{
    [SerializeField]
    public List<bool> TaskCheckers;
    [SerializeField]
    private List<GameObject> Tasks;
    [SerializeField]
    private List<GameObject> CheckerObjects;

    [SerializeField]
    private List<GameObject> Pin;
    public int PinNum;

    public static int EnemyKills;

    void Start()
    {

    }

    void Update()
    {
        OnCheckers();
    }

    void OnCheckers()
    {
        for(int i = 0; i < CheckerObjects.Count; i++)
        {
            if (TaskCheckers[i] == true)
            {
                CheckerObjects[i].SetActive(true);
            }
            else
            {
                CheckerObjects[i].SetActive(false);
            }
        }
    }

    public static void TaskCheck(int num)
    {
       FindObjectOfType<Tutoreal>().TaskCheckers[num] = true;
    }
}
