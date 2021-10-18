using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMaterialCheck3D : MonoBehaviour
{
    // Script à attacher à l'objet player
    //Permet de checker le matériau au sol à l'aide de son tag
    //et change un paramètre RTPC en conséquence pour les bruits de pas

    // distance entre la position de l'objet player et le sol
    public float distance = 0.1f;

    public float material = 1f;

    private GameObject player;

    // Layer sur lequel se trouvent les plateformes
    public int walkableLayer = 0;

    private void Start()
    {
        // trouve l'objet unique player dans la scène
        // ne fonctionne que si un objet possède ce tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        MaterialCheck();
        Debug.DrawRay(player.transform.position, Vector3.down * distance, Color.blue);
    }

    private void MaterialCheck()
    {
        // on vérifie le matériau sous les pieds du player
        RaycastHit hit;

        if (Physics.Linecast(player.transform.position, Vector3.down, out hit))
        {
            if (hit.collider)
            {
                float materialValue = FMODSoundManager.sharedInstance.gameObject.GetComponent<FMODMaterialList>().GetTagValue(hit.collider.tag);
                player.GetComponent<FMODCharacterLocomotion>().currentMaterialValue = materialValue;
            }
        }
    }
}
