using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TobyManager : MonoBehaviour
{
    private NavMeshAgent agent;

    //Toby a une liste (voir inspecteur)
    public List<TargetPoint> targetPoints = new List<TargetPoint>();
    //Targetpoint possède une méthode pour me donner un point.

    private int indexNextDestintation = 0; //Pointer index de la prochaine destination
    private Vector3 actualDestination; //Stocke destination vers laquelle Tobyse dirige 

    void Start()
    {   //J'ai la certitude que j'ai un NavMeshAgent 
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(1, 100);
        agent.speed = Random.Range(1f, 6f);
        NextDestination();
    }


    void Update()
    {
        //SD donne au NavMeshAgent la destination où il doit aller.
        
        if(agent.remainingDistance <= agent.stoppingDistance) {
                NextDestination();
        }
    }

    //Dire à Toby où on doit aller
    private void NextDestination()
    {
        //Next destination dit : on va aller prendre notre index de la next destination
        //Ca me demande le targetpoint au point zéro
        //TP a une méthode givepoint, je peux l'appeler car elle est publique, permet de recevoir un target point autour de lui
        //Stocker ça dans actual...

        //Randomize destination instead of getting the next index

        //Redemander index aléatoire tant que index n'a pas bougé
        int oldIndex = indexNextDestintation;
        while(oldIndex == indexNextDestintation && targetPoints.Count > 1) { //si on met pas le 1, il va tourner en boucle car seule possibilité d'index c'est le 0 par défaut.
            indexNextDestintation = Random.Range(0, targetPoints.Count);
        }
        
        TargetPoint tp = targetPoints[indexNextDestintation];
        actualDestination = tp.GivePoint();
        agent.SetDestination(actualDestination);
        indexNextDestintation++; //nextdest = prochaine destination du tableau
        if (indexNextDestintation >= targetPoints.Count) indexNextDestintation = 0;//pcq il peut pas dépasser longueur du tableau ! il reviendra à sa position à la fin de son tour
        

    }

    //Créer une sphère au dessus tête Toby pour indiquer la priorité
        private void OnDrawGizmos() {
            //si y a pas d'agent, alors tu ne le fais pas
            if (agent != null) {

            //Vector3.up --> 0,1,0 donc la sphère sera au dessus de moi
            //100 - ... --> plus ma priorité est haute, moins ma sphère sera grande. 
            //*0.01 --> pour pas que la sphère soit trop grosse

            Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f + (100 - agent.avoidancePriority) * 0.01f);
        }
    }
}
