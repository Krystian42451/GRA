using System.Collections;
using UnityEngine;

public class CameraZoomOnSpace : MonoBehaviour
{
    public Transform cameraTransform; // Referencja do kamery
    public float zoomDistance = 4f; // O ile kamera ma siê oddaliæ
    public float zoomDuration = 0.1f; // Jak d³ugo kamera pozostaje oddalona
    public float zoomSpeed = 4f; // Jak szybko kamera oddala siê
    public float returnSpeed = 2f; // Szybkoœæ powrotu kamery

    private Vector3 defaultPosition; // Pocz¹tkowa pozycja kamery
    private bool isZooming = false; // Flaga, czy kamera jest w trakcie oddalania

    void Start()
    {
        // Zapisz pocz¹tkow¹ pozycjê kamery
        if (cameraTransform == null)
        {
            cameraTransform = transform;
        }
        defaultPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        // Gdy naciœniesz klawisz spacji i kamera nie jest ju¿ oddalana
        if (Input.GetKeyDown(KeyCode.Space) && !isZooming)
        {
            StartCoroutine(ZoomCamera());
        }
    }

    IEnumerator ZoomCamera()
    {
        isZooming = true;

        // Cel oddalenia kamery
        Vector3 zoomedPosition = defaultPosition + new Vector3(0, 0, -zoomDistance);

        // Oddalanie kamery
        while (Vector3.Distance(cameraTransform.localPosition, zoomedPosition) > 0.01f)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomedPosition, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        // Czekanie w oddaleniu
        yield return new WaitForSeconds(zoomDuration);

        // Przywracanie kamery do domyœlnej pozycji
        while (Vector3.Distance(cameraTransform.localPosition, defaultPosition) > 0.01f)
        {
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, defaultPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }

        // Ustawienie pozycji dok³adnie na domyœln¹
        cameraTransform.localPosition = defaultPosition;
        isZooming = false;
    }
}
