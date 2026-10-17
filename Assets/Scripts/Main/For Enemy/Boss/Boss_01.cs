using UnityEngine;
using System.Collections.Generic;
public class Boss_01 : MonoBehaviour
{
    PlayerHp player;
    [SerializeField]
    private List<Material> mat;

    [SerializeField]
    private GameObject Tentacle;

    MeshRenderer Mes;

    [SerializeField]
    private float PatternTime = 2;
    private float PatternCount;

    void Start()
    {
        player = FindObjectOfType<PlayerHp>();
        Mes = GetComponent<MeshRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        PatternCount += Time.deltaTime;
        if(PatternCount > PatternTime)
        {
            PatternCount = 0;
            Tentacle4Attack();
        }
    }

    void EyeColor()
    {
        int RamdmNum = Random.Range(0, 3);
        Mes.material = mat[RamdmNum];
        switch (RamdmNum)
        {
            case 0:
                //‚P•ªŠÔó‚¯‚éƒ_ƒ[ƒW‚ª‚R‚O“ã¸‚·‚é
                break;
                case 1:
                //‚P•ªŠÔ–ˆ•b‚Qƒ_ƒ[ƒW‚ÌŒp‘±ƒ_ƒ[ƒW‚ğó‚¯‚é
                break;
                case 2:
                //‚S‚O•bŠÔƒvƒŒƒCƒ„[‚ÌˆÚ“®‘€ìiã‰º¶‰Ej‚ğ”½“]‚³‚¹‚é
                break;
        }
    }

    void Tentacle4Attack()
    {
        Instantiate(Tentacle,player.transform.position, Quaternion.identity);
        Instantiate(Tentacle, player.transform.position + Vector3.forward * 3, Quaternion.identity);
        Instantiate(Tentacle, player.transform.position + Vector3.right * 2 + Vector3.back * 2, Quaternion.identity);
        Instantiate(Tentacle, player.transform.position + Vector3.left * 2 + Vector3.back * 2, Quaternion.identity);
    }

}
