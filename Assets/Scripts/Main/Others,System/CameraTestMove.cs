using UnityEngine;

public class CameraTestMove : MonoBehaviour
{
   [SerializeField] private GameObject player;
    Vector3 myPos;
    private void Start()
    {
        myPos = transform.position;
    }
    void Update()
    {
        // ˆÚ“®
        transform.position = new Vector3(player.transform.position.x,myPos.y, player.transform.position.z);
    }
}
