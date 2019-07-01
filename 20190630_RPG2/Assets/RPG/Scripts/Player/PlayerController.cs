using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;

    public LayerMask movementMask;
    public LayerMask monsterMask;


    Camera cam;
    PlayerMotor motor;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {


        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);


                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            // we create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, 100, monsterMask))
            {
                Debug.Log("monster hi!");
            }
            else if (Physics.Raycast(ray, out hit, 100)) // if the ray hits
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
                // If we did set it as our focus
            }
        }
    }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
                focus = newFocus;
                motor.FollowTarget(newFocus);
            }
        }
        Debug.Log("SetET");
        focus = newFocus;
        newFocus.OnFocused(transform);
        motor.FollowTarget(newFocus);
    }

    void RemoveFocus()
    {
        if (focus !=  null){
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget(); 
    }


}
