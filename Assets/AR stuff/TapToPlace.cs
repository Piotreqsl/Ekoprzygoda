using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;

public class TapToPlace : MonoBehaviour
{

    public GameObject placementIndicator;
    public GameObject item;
    public bool isPlaced = false;

    public GameObject placeBTN;
    public GameObject retryBTN;
    public GameObject crosshair;
    public GameObject throwoutBTN;

   
    private ARSessionOrigin aro;
    private Pose placementPose;
    private bool placementPoseValid = false;

    // Start is called before the first frame update
    void Start()
    {
        aro = FindObjectOfType<ARSessionOrigin>();


        placeBTN.SetActive(true);
        retryBTN.SetActive(false);
        crosshair.SetActive(false);
        throwoutBTN.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

     

    }

    public void destroyTrash()
    {
        GameObject trash = GameObject.FindGameObjectWithTag("trashcan");
        Destroy(trash);
        isPlaced = false;
        retryBTN.SetActive(false);
        placeBTN.SetActive(true);
        throwoutBTN.SetActive(false);
        crosshair.SetActive(false);
        placementIndicator.SetActive(true);
    }

    public void PlaceObject()
    {
        if (placementPoseValid && isPlaced == false)
        {
            Instantiate(item, placementPose.position, placementPose.rotation);
            isPlaced = true;
            placeBTN.SetActive(false);
            retryBTN.SetActive(true);
            throwoutBTN.SetActive(true);
            crosshair.SetActive(true);

            placementIndicator.SetActive(false);

        }
    }
   

    public void ThrowOut()
    {
        

        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 25f))
        {
            GameObject.FindGameObjectWithTag("GuideText").GetComponent<Text>().text = hit.transform.name + Input.GetTouch(0).position.ToString();
            GameObject trash = GameObject.FindGameObjectWithTag("trash");
            hit.transform.gameObject.GetComponent<TrashIdentifierAR>().VerifyTrash(trash.name);
        }

       

    }

    private void UpdatePlacementPose()
    {

            var center = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            aro.GetComponent<ARRaycastManager>().Raycast(center, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            placementPoseValid = hits.Count > 0;

            if (placementPoseValid)
            {
                placementPose = hits[0].pose;

                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        

    }

    private void UpdatePlacementIndicator()
    {

        if (placementPoseValid && !isPlaced)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

           
        }

        else
        {
            placementIndicator.SetActive(false);
            
        }


        

    }


}
