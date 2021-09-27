using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;  
    [SerializeField] float moveSpeed = 2f;

    List<Transform> waypoints;

    int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[currentWaypoint].transform.position;       
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();    
    }

    private void MoveToWaypoint()
    {
        if (currentWaypoint <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[currentWaypoint].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                currentWaypoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
