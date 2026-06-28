using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public static FieldOfView fieldOfView;
    // public float radius;
    // [Range(0,360)]
    // public float angle;
    // public LayerMask player;
    // public LayerMask obstacles;
     public bool canSeePlayer;
    // public GameObject playerObj;
    public FieldOfViewData fieldOfViewData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        fieldOfViewData.playerObj = Moviment.moviment.gameObject;
        if (fieldOfView == null) fieldOfView = this;
        StartCoroutine(FovRoutine());
        fieldOfViewData.pos = transform;
    }
    IEnumerator FovRoutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfViewCheck();
        }
    }
    void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, fieldOfViewData.radius, fieldOfViewData.player);
        if (rangeChecks.Length > 0)
        {
            
            Transform target = rangeChecks[0].transform;
            Vector3 directionToPlayer = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToPlayer) < fieldOfViewData.angle / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);
                // if(distanceToPlayer < 0.5f)
                // {
                // DeathScreenAni.deathScreen.Killer();
                //     return;
                // }
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, fieldOfViewData.obstacles))
                    canSeePlayer = true;
                else canSeePlayer = false;


            }
            else canSeePlayer = false;



        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
    void OnDrawGizmosSelected()
    {
        // 1. Escolhe a cor do Gizmo
        Gizmos.color = Color.yellow; // Ou verde, azul, etc.

        // 3. Desenha o cubo. 
        // Como a matriz já calculou o transform.position, passamos apenas o offset local.
       Gizmos.DrawWireSphere(transform.position, fieldOfViewData.radius);
    }

    // Update is called once per frame

}
