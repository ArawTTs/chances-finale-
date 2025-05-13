using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ForceClippingPlane : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(ForceClipEveryFrame());
    }

    IEnumerator ForceClipEveryFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (cam != null)
            {
                cam.nearClipPlane = 0.3f;
                cam.farClipPlane = 5000f;
                // Optional: Uncomment to debug
                // Debug.Log("Far Clip forced to: " + cam.farClipPlane);
            }
        }
    }
}
