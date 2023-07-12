# 49383-Final-Project

Some important code bits

ARPlacementManager.cs

public class ARPlacementManager : MonoBehaviour
{
public GameObject arObjectToSpawn; // The AR object to spawn
public GameObject placementIndicator; // Indicator for object
placement
public GameObject keepScanningCanvas; // Canvas to display while
scanning
private GameObject spawnedObject; // The spawned AR object
private Pose PlacementPose; // The pose for object placement
private ARRaycastManager aRRaycastManager; // AR raycast manager for
hit testing
private bool placementPoseIsValid = false; // Flag indicating if
placement pose is valid
// Start is called before the first frame update
void Start()
{
placementPoseIsValid = false;
aRRaycastManager = FindObjectOfType&lt;ARRaycastManager&gt;();
}
// Update is called once per frame
void Update()
{
if (spawnedObject == null &amp;&amp; placementPoseIsValid &amp;&amp;
Input.touchCount &gt; 0 &amp;&amp; Input.GetTouch(0).phase == TouchPhase.Began)
{
ARPlaceObject();
}
UpdatePlacementPose();
UpdatePlacementIndicator();
}
// Update the placement pose based on the raycast hit

void UpdatePlacementPose()
{
if (spawnedObject == null &amp;&amp; placementPoseIsValid)
{
keepScanningCanvas.SetActive(false);
placementIndicator.SetActive(true);
placementIndicator.transform.SetPositionAndRotation(PlacementPose.position
, PlacementPose.rotation);
}
else
{
placementIndicator.SetActive(false);
}
}
// Update the placement indicator based on the raycast hit
void UpdatePlacementIndicator()
{
var screenCenter = Camera.current.ViewportToScreenPoint(new
Vector3(0.5f, 0.5f));
var hits = new List&lt;ARRaycastHit&gt;();
aRRaycastManager.Raycast(screenCenter, hits,
TrackableType.Planes);
placementPoseIsValid = hits.Count &gt; 0;
if (placementPoseIsValid)
{
PlacementPose = hits[0].pose;
}
}
// Place the AR object at the placement pose
void ARPlaceObject()
{
spawnedObject = Instantiate(arObjectToSpawn,
PlacementPose.position, PlacementPose.rotation);
}
// Load a scene by scene number

public void loadTheScene(int sceneNum)
{
SceneManager.LoadScene(sceneNum);
}
}

CharacterManager.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterManager : MonoBehaviour
{
public Transform[] waypoints; // Array of waypoint transforms
public float moveSpeed = 3f; // Speed at which the NPC moves between
waypoints
private int currentWaypointIndex = 0; // Index of the current waypoint
public bool isWalking = true;
private void Start()
{
if (waypoints.Length &gt; 0)
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
Vector3 direction = waypoints[currentWaypointIndex].position -
transform.position;
// Move towards the current waypoint
transform.Translate(direction.normalized * moveSpeed *
Time.deltaTime, Space.World);
// Rotate towards the current waypoint
Quaternion targetRotation =
Quaternion.LookRotation(direction);
transform.rotation = Quaternion.Lerp(transform.rotation,
targetRotation, 5f * Time.deltaTime);
// Check if the NPC has reached the current waypoint
if (Vector3.Distance(transform.position,
waypoints[currentWaypointIndex].position) &lt; 0.01f)
{
// Increment the waypoint index
currentWaypointIndex++;
// Reset the waypoint index if it exceeds the array length
if (currentWaypointIndex &gt;= waypoints.Length)
currentWaypointIndex = 0;
}
}
}
}

The following code snippet inside MainMovements.cs

public void OnTriggerEnter(Collider col)
{
carAudioSource.Play();
if(col.gameObject.tag == &quot;NPC&quot;)
{
col.gameObject.GetComponent&lt;Animator&gt;().SetBool(&quot;isHit&quot;,
true);
}
if(col.GetComponent&lt;CharacterManager&gt;() != null)
{
col.GetComponent&lt;CharacterManager&gt;().isWalking = false;
}
}
public void OnTriggerExit(Collider col)
{
if(col.gameObject.tag == &quot;NPC&quot;)
{
col.gameObject.GetComponent&lt;Animator&gt;().SetBool(&quot;isHit&quot;,
false);
}
if(col.GetComponent&lt;CharacterManager&gt;() != null)
{
col.GetComponent&lt;CharacterManager&gt;().isWalking = true;
}
}
