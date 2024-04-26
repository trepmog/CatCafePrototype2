using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatIcons : MonoBehaviour
{
    [SerializeField] GameObject traitIconsPrefab;
    private CatProfile catProfile;
    private string[] traitText;
    private GameObject trait1;
    private GameObject trait2;
    private Camera mainCam;
    private Canvas canvasObj;



    void Start()
    {
        GameObject camCam = GameObject.FindWithTag("MainCamera");
        mainCam = camCam.GetComponent<Camera>();
        // Find the icons on profile
        catProfile = GetComponent<CatProfile>();

        // Don't get the traits until the CatProfile has loaded them by subscribing to the Cat Profile event
        catProfile.OnLoaded += SetupTraitIcons;
        
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupTraitIcons()
    {
        // Gets the text for the cat traits
        traitText = catProfile.GetTraits();
        // Creates the canvas containing the trait icons
        GameObject traitCanvas = Instantiate(
            traitIconsPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        canvasObj = traitCanvas.GetComponent<Canvas>();
        canvasObj.worldCamera = mainCam;

        // Sets the canvas to appear above the object (cat) and rotate 45 deg for the camera
        traitCanvas.transform.localPosition = new Vector3(0, 1.2f, 0);
        traitCanvas.transform.localEulerAngles = new Vector3(45, 0, 0);
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
