using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagerScriptDrag2 : MonoBehaviour
{
    IControllableDrag2 selectedObject;
    private float tapBegan;
    private bool tapMoved;
    private float tapTime = 0.5f;
    float starting_distance_to_selected_object;
    private List<IControllableDrag2> AllIcontrollables;
    Ray new_position;
    void Start()
    {
         GameObject ourCameraPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
         ourCameraPlane.transform.up = (Camera.main.transform.position - ourCameraPlane.transform.position).normalized;

        var AllObjects = FindObjectsOfType(typeof(MonoBehaviour));

        AllIcontrollables = new List<IControllableDrag2>();

        foreach (MonoBehaviour obj in AllObjects)
        {
            if (obj is IControllableDrag2)
            {
                AllIcontrollables.Add(obj.GetComponent<IControllableDrag2>());
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            RaycastHit info;
            Ray ourRay = Camera.main.ScreenPointToRay(touch.position);
            Debug.DrawRay(ourRay.origin, 30 * ourRay.direction);
            if (touch.phase == TouchPhase.Began)
            {
                tapBegan = Time.time;
                tapMoved = false;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                tapMoved = true;
            }
            if (touch.phase == TouchPhase.Ended && tapMoved == false)
            {
                float tapLength = Time.time - tapBegan;
                if (tapLength <= tapTime && Physics.Raycast(ourRay, out info))
                {
                    IControllableDrag2 object_hit = info.transform.GetComponent<IControllableDrag2>();
                    if (object_hit != null)
                    {
                        if (selectedObject == null)
                        {
                            object_hit.youveBeenTapped();
                            Debug.Log("YOUVE BEEN TAPPED");
                            selectedObject = object_hit;
                            selectedObject.selectObject(true);
                            starting_distance_to_selected_object = Vector3.Distance(Camera.main.transform.position, info.transform.position);

                            foreach (IControllableDrag2 obj in AllIcontrollables)
                            {
                                if (obj != selectedObject)
                                {
                                    obj.selectObject(false);
                                }
                            }
                        }
                        else
                        {
                            selectedObject = null;
                            Debug.Log("Deselected object");

                            foreach (IControllableDrag2 obj in AllIcontrollables)
                            {
                                obj.selectObject(false);
                            }
                        }
                    }
                }
            }
            //assume selected object is not null. object selected. drag code here
            if (selectedObject != null)
            {
                switch (Input.touches[0].phase)
                {
                    case TouchPhase.Began:

                        break;

                    case TouchPhase.Moved:
                        new_position = Camera.main.ScreenPointToRay(Input.touches[0].position);
                        selectedObject.MoveTo(Input.touches[0], new_position.GetPoint(starting_distance_to_selected_object));
                        tapMoved = false;
                        break;

                    case TouchPhase.Stationary:
                        // new_position = Camera.main.ScreenPointToRay(Input.touches[0].position);
                        // selectedObject.MoveTo(Input.touches[0],new_position.GetPoint(starting_distance_to_selected_object));
                        break;
                    case TouchPhase.Ended:
                        // selectedObject.Stop();
                        break;
                }
            }
        }
    }
}