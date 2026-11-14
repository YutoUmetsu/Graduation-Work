using UnityEngine;

public class TutorialPin : MonoBehaviour
{
    PlayerHp player;
    [SerializeField]
    Tutoreal tutoreal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerHp>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            tutoreal.PinNum++;
            if(tutoreal.PinNum >= 3)
            {
                tutoreal.TaskCheckers[0] = true;
            }
            Destroy(gameObject);
        }
    }
}
