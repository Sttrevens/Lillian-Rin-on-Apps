using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationSpeedController : MonoBehaviour
{
    public Animator animator;
    public float fastSpeed = 2f;
    public float slowSpeed = 0.5f;
    public float slowDownRate = 0.1f;
    public float speedIncrement = 0.1f;
    public float speedDecrement = 0.2f;
    public float maxSpeed = 3f;

    private float targetSpeed;

    [SerializeField]
    private List<GameObject> objectsToActivate;

    [SerializeField]
    private float activationSpeedThreshold = 2.5f;

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
            // Increase speed on each click, up to the maximum speed
            targetSpeed = Mathf.Min(targetSpeed + speedIncrement, maxSpeed);
        }

        // If current speed is greater than slowSpeed, gradually slow down animation
        if (targetSpeed > slowSpeed)
        {
            targetSpeed = Mathf.Max(targetSpeed - speedDecrement * Time.deltaTime, slowSpeed);
            float currentSpeed = Mathf.Lerp(animator.speed, targetSpeed, slowDownRate);
            animator.speed = currentSpeed;
            Debug.Log("currentSpeed:" + currentSpeed);
        }

        Debug.Log("targetSpeed:" + targetSpeed);

        if (targetSpeed >= activationSpeedThreshold)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                }
            }
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
