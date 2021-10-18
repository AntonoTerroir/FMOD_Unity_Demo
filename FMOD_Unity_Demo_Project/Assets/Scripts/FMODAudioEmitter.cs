using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ce script permet de jouer un son 3D à partir d'un objet placé dans l'espace
//Il limite l'utilisation de cpu en stoppant l'event si le joueur sort de la distance maximale d'atténuation


public class FMODAudioEmitter : MonoBehaviour
{
    //Event à jouer
    public FMODUnity.EventReference eventRef;

    //Instance de l'evenement qui nous permettra de le jouer, de le modifier et de le stopper plus tard
    public FMOD.Studio.EventInstance eventInstance;

    //Le script nécéssite pour cela un Sphere Collider pour détecter la présence du joueur
    //Il faut fournir la distance maximale d'attenuation de l'event pour que le script modifie le rayon du sphere collider en fonction de celle-ci
    private SphereCollider sphereCol;
    public float maxAttenuationDistance = 20f;

    private Rigidbody rigidBody;

    void Start()
    {
        //On vérifie la présence sur le GameObject d'un sphere collider
        if (this.GetComponent<SphereCollider>() != null)
        {
            //S'il y en a un, on dit au script que le teme "sphereCol" fera référence à ce SphereCollider
            sphereCol = this.GetComponent<SphereCollider>();
        }
        else
        {
            //S'il n'y en a pas, on en ajoute un au GameObject
            sphereCol = gameObject.AddComponent<SphereCollider>() as SphereCollider;
            //On regle le rayon du collider en fonction de la distance maximale d'attenuation
            sphereCol.radius = maxAttenuationDistance;
            //On dit a la sphere que c'est un trigger
            //Si on ne le fait pas la sphere devient tangible et on ne peut pas rentrer dedans
            //Ce serait un peu con, faut avouer
            sphereCol.isTrigger = true;
        }
        //On verifie la présence sur le GameObject d'un RigidBody
        if (this.GetComponent<Rigidbody>() != null)
        {
            //S'il y en a un, on dit au script que le teme "rigidBody" fera référence à ce RigidBody
            rigidBody = this.GetComponent<Rigidbody>();
        }
        else
        {
            //S'il n'y en a pas, on en ajoute un au GameObject
            rigidBody = gameObject.AddComponent<Rigidbody>() as Rigidbody;
            //Et on desactive l'utilisation de la gravite,
            //Sinon notre objet chute au demarrage de la scene
            //Ce serait un peu con, faut avouer
            rigidBody.useGravity = false;
        }

        //Afin de pouvoir en modifier des paramètres plus tard
        //Il faut créer une instance de l'event avant de le jouer
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventRef);

        //Pour que le son puisse être placé dans l'espace 3D
        //et même pour qu'il puisse suivre un objet mouvant si besoin
        //il faut attacher l'instance d'event à un GameObject
        //Ici par son transform et un rigidbody
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(eventInstance, this.transform, rigidBody);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si c'est le personnage joueur qui rentre dans la sphère d'effet, lance le son
        if (other.CompareTag("Player"))
        {
            eventInstance.start();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Si c'est le personnage joueur qui sort de la sphère d'effet, coupe le son
        if (other.CompareTag("Player"))
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
