using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public static FieldOfView fieldOfView;
    public float radius;
    [Range(0,360)]
    public float angle;
    public LayerMask player;
    public LayerMask obstacles;
    public bool canSeePlayer;
    public GameObject playerObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        playerObj = Moviment.moviment.gameObject;
        if (fieldOfView == null) fieldOfView = this;
        StartCoroutine(FovRoutine());
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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, player);
        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToPlayer = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacles))
                    canSeePlayer = true;
                else canSeePlayer = false;


            }
            else canSeePlayer = false;



        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    // Update is called once per frame

}
