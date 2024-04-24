using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        // force the rotation of the object to be fixed on y axies
        if(transform.parent != null)
            transform.rotation = Quaternion.Euler(45.0f, 0, 0);
    }
}