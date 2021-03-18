using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControllerDrag2 : MonoBehaviour, IControllableDrag2
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void youveBeenTapped()
    {
        selectObject(true);
    }
    public void MoveTo(Touch touch, Vector3 destination)
    {
        // get the touch position from the screen touch to world point
        Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
        // lerp and set the position of the current object to that of the touch, but smoothly over time.
        transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime * 10);
    }

    public void selectObject(bool isSelected)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (isSelected)
        {
            renderer.material.SetColor("_Color", Color.green);
        }
        else
        {
            renderer.material.SetColor("_Color", Color.white);
        }
    }
}
