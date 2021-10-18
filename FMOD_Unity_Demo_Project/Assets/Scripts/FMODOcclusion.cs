using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ce script permet de créer un effet d'occlusion simple à partir d'un son 3D
//À attacher à un objet possédant déjà le script FMOD AudioEmitter

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(FMODAudioEmitter))]
public class FMODOcclusion : MonoBehaviour
{
    //Le script a besoin de connaitre l'objet sur lequel se trouve l'audio listener
    //afin de détecter la présence d'obstacle entre celui-ci et la source du son
    [SerializeField] private GameObject audioListener = null;

    private float maxDistanceOcclusion;

    //à activer pour voir se tracer le rayon entre audio listener et source sonore
    //visible seulement dans la vue scene
    public bool useDebug = false;

    //permet de savoir si le personnage est dans la zone d'effet
    private bool isInCollider = false;

    [Header("Occlusion Settings")]

    //permet de désactiver totalement l'effet d'occlusion si besoin
    //seulement pour cette source
    public bool useOcclusion = true;

    //Noms des RTPC dans FMOD servant à contrôler l'effet d'occlusion
    public string lopassParam = "Occlusion_Lopass";
    public string volumeParam = "Occlusion_Volume";

    //Valeurs maximales qu'atteindra l'effet d'occlusion
    //À changer selon le type de matériau créant l'effet
    //Exemple : 0.2f pour du verre
    //0.8f pour du béton
    [Range(0f, 1f)]
    public float lopassMax = 1f;
    [Range(0f, 1f)]
    public float volumeMax = 1f;

    //Certains objets ne devraient pas faire d'occlusion
    //Comme le corps du personnage
    //La liste qui suit est une liste de tag d'objets à ignorer par l'effet d'occlusion
    //Pour empêcher un objet de faire de l'occlusion il faut donc
    //Soit ajouter son tag à la liste et modifier plus bas les conditions de Raycast
    //Soit lui donner le tag "IgnoreOcclusion"
    public string[] ignoreTypeOccluder = new string[] { "IgnoreOcclusion", "Player" };

    private FMOD.Studio.EventInstance eventInstance;

    void Start()
    {
        //Récupère le rayon de la sphère du FMODAudioEmitter pour délimiter la zone d'effet
        maxDistanceOcclusion = GetComponent<SphereCollider>().radius;
    }

    void Update()
    {
        //Si on désactive l'occlusion
        //ou que l'on a pas défini d'audio listener
        //ou que le personnage n'est pas dans la zone d'effet
        //ne fait rien
        if (!useOcclusion || audioListener == null || !isInCollider) { return; }

        RaycastHit outInfo;

        Vector3 direction = audioListener.transform.position - this.transform.position;

        //Trace un rayon entre l'audiolistener et la source sonore
        bool hit = Physics.Raycast(this.transform.position, direction, out outInfo, maxDistanceOcclusion);
        
        if (useDebug) { Debug.DrawRay(this.transform.position, direction, Color.green); }
        

        if (hit)
        {
            //Donne dans la console le nom de l'objet touché
            if (useDebug) { print(outInfo.collider.gameObject.name); }

            //Si l'objet touché n'est pas l'audiolistener ni un des tag à ignorer
            //L'occlusion fait effet
            if (outInfo.collider.gameObject != audioListener && outInfo.collider.gameObject.tag != ignoreTypeOccluder[0] && outInfo.collider.gameObject.tag != ignoreTypeOccluder[1])
            {
                eventInstance.setParameterByName(lopassParam, lopassMax);
                eventInstance.setParameterByName(volumeParam, volumeMax);
            }
            //Sinon l'occlusion est annulée
            else
            {
                eventInstance.setParameterByName(lopassParam, 0f);
                eventInstance.setParameterByName(volumeParam, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Une fois le joueur rentré dans la zone d'effet
        //On récupère sur le FMODAudioEmitter l'instance de l'event en cours
        eventInstance = GetComponent<FMODAudioEmitter>().eventInstance;
        isInCollider = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInCollider = false;
    }
}
