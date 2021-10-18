using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMaterialCheck2D : MonoBehaviour
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
        Debug.DrawRay(player.transform.position, Vector2.down * distance, Color.blue);
    }

    private void MaterialCheck()
    {
        RaycastHit2D hit;

        // on vérifie le matériau sous les pieds du player
        hit = Physics2D.Raycast(player.transform.position, Vector2.down, distance, 1 << walkableLayer);

        if (hit.collider)
        {
            if (hit.collider.tag == "Material: dirt")
            {
                material = 1f;
            }

            else if (hit.collider.tag == "Material: stone")
            {
                material = 2f;
            }

            else
            {
                // si le tag ne correspond pas, on retombe sur le matériau de base
                material = 1f;
            }
        }
    }

    public void PlayFootstepsEvent(string path)
    {
        FMOD.Studio.EventInstance footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        footsteps.setParameterByName("Material", material);
        footsteps.start();
        footsteps.release();
    }
}
