using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerIcons : MonoBehaviour
{
    [SerializeField] GameObject traitIconsPrefab;
    public CustomerProfile customerProfile;
    private string[] traitText;
    private GameObject trait1;
    private GameObject trait2;
    private Camera mainCam;
    private Canvas canvasObj;
    // Start is called before the first frame update
    void Start()
    {
        GameObject camCam = GameObject.FindWithTag("MainCamera");
        mainCam = camCam.GetComponent<Camera>();

        // Find the icons on profile
        customerProfile = GetComponent<CustomerProfile>();

        // Don't get the traits until the CustomerProfile has loaded them by subscribing to the Customer Profile event
        customerProfile.OnLoaded += SetupTraitIcons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupTraitIcons()
    {
        traitText = customerProfile.GetTraits();

        GameObject traitCanvas = Instantiate(
            traitIconsPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        canvasObj = traitCanvas.GetComponent<Canvas>();
        // Sets the camera to be a World Camera. This is done programmtically because Customer is a prefab and not a scene level asset
        canvasObj.worldCamera = mainCam;
        // Sets the canvas to appear above the object (customer) and rotate 45 deg for the camera
        traitCanvas.transform.localPosition = new Vector3(-0.7f, 2.6f, -1.16f);
        traitCanvas.transform.localEulerAngles = new Vector3(-45f, 0f, 0f);
        Debug.Log("Running the canvas creation");
        // Assigns the individual icons to variables for later use
        trait1 = traitCanvas.transform.Find(traitText[0]).gameObject;  
        trait2 = traitCanvas.transform.Find(traitText[1]).gameObject;
        // Separate them so they don't appear on top of each other
        trait1.transform.localPosition = new Vector3(-50f, 0, 0); // L
        trait2.transform.localPosition = new Vector3(50f, 0, 0); // R

        // Ensures those icons are disabled at start
        if (traitCanvas != null) 
        {
            trait1.SetActive(false);
            trait2.SetActive(false);
        }
    }

    // Shows or hides the icons
    public void ToggleShowIcons(bool toggle)
    {
        trait1.SetActive(toggle);
        trait2.SetActive(toggle);
    }
}
