using UnityEngine;
using System.Collections.Generic;
public class Boss_01 : MonoBehaviour
{

    [SerializeField]
    private List<Material> mat;

    MeshRenderer Mes;

    void Start()
    {
        Mes = GetComponent<MeshRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
