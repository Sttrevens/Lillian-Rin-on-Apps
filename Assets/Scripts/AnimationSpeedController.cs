using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationSpeedController : MonoBehaviour
{
    public Animator animator;
    public float fastSpeed = 2f;
    public float slowSpeed = 0.5f;
    public float slowDownRate = 0.1f;

    private float targetSpeed;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        targetSpeed = slowSpeed;
    }

    void Update()
    {
        // On touch or click
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            targetSpeed = fastSpeed;
        }

        // Gradually slow down animation
        float currentSpeed = Mathf.Lerp(animator.speed, targetSpeed, slowDownRate);
        animator.speed = currentSpeed;

        if (Mathf.Approximately(animator.speed, slowSpeed))
        {
            targetSpeed = slowSpeed;
        }
    }

    private bool IsPointerOverUIObject()
    {
        // Check if the pointer is over a UI object (in case you have UI elements on the screen)
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
