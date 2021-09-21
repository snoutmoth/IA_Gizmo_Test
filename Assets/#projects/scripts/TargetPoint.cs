using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float radius = 1f;
    // Start is called before the first frame update
   
   public Vector3 GivePoint () {
       //Objet qui est un point dans une sphère de rayon 1 qui est multipliée par son radius. 
       //Donc théoriquement, j'ai un opoint dans une sphère qui estégale à mon rayon.
       //Pour transformer un point en sphère

       Vector3 point = Random.insideUnitSphere * radius; //cercle avec un rayon
       point.z = point.y; //inverser axe x pour obtenir le cercle en y
       point.y = 0;
       point += transform.position;
       return point;

       //a retenir : si j'ajoute un vector à position, il devient point dans un rayon autour objet ??
   }    
   private void OnDrawGizmos() {
        Gizmos.color = new Color(0f, 0.5f, 0.9f, 0.4f);
        Gizmos.DrawSphere(transform.position, radius);
   }
   
}
