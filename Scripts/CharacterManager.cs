using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoint transforms
    public float moveSpeed = 3f; // Speed at which the NPC moves between waypoints

    private int currentWaypointIndex = 0; // Index of the current waypoint

    public bool isWalking = true;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            // Move the NPC to the first waypoint
            transform.position = waypoints[0].position;
            transform.rotation = waypoints[0].rotation;
        }
    }

    private void Update()
    {
        if(isWalking)
        {
            if (waypoints.Length == 0)
                return;

            // Calculate the direction to the current waypoint
            Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

            // Move towards the current waypoint
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

            // Rotate towards the current waypoint
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

            // Check if the NPC has reached the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.01f)
            {
                // Increment the waypoint index
                currentWaypointIndex++;

                // Reset the waypoint index if it exceeds the array length
                if (currentWaypointIndex >= waypoints.Length)
                    currentWaypointIndex = 0;
            }
        }
    }
}
