using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    public GameObject arObjectToSpawn; // The AR object to spawn
    public GameObject placementIndicator; // Indicator for object placement
    public GameObject keepScanningCanvas; // Canvas to display while scanning

    private GameObject spawnedObject; // The spawned AR object
    private Pose PlacementPose; // The pose for object placement
    private ARRaycastManager aRRaycastManager; // AR raycast manager for hit testing
    private bool placementPoseIsValid = false; // Flag indicating if placement pose is valid

    // Start is called before the first frame update
    void Start()
    {
        placementPoseIsValid = false;

        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    // Update the placement pose based on the raycast hit
    void UpdatePlacementPose()
    {
        if (spawnedObject == null && placementPoseIsValid)
        {
            keepScanningCanvas.SetActive(false);
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    // Update the placement indicator based on the raycast hit
    void UpdatePlacementIndicator()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    // Place the AR object at the placement pose
    void ARPlaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);
    }

    // Load a scene by scene number
    public void loadTheScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
