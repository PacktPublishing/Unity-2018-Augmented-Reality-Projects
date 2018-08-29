using UnityEngine;

public class TargetLocation : MonoBehaviour
{
    public GameObject targetObject;

    void SetLocation()
    {
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                    Instantiate(targetObject, new Vector3(Input.GetTouch(i).position.x, 4.23f, Input.GetTouch(i).position.y), Quaternion.identity);
        }
    }

    private void Update()
    {
        SetLocation();
    }
}
