using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLine : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRender;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float stopVelocity;
    [SerializeField] private float shotPower;
 
    private bool isIdle;
    private bool isAiming;
    private Rigidbody rigidBody;
    private float lineLength;
    private Vector3 lastVelocity;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        lastVelocity = rigidBody.velocity;
        isAiming = false;
        isIdle = true;
        lineRender.enabled = false;
        trailRenderer.enabled = false;
    }
    private void FixedUpdate()
    {
       /* Debug.Log(rigidBody.velocity.magnitude);*/
        /*        if (rigidBody.velocity.magnitude < stopVelocity)
                {
                    Stop();
                }*/
        ProcessAim();
    }

    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }

    private void ProcessAim()
    {
        if(!isAiming || !isIdle)
        {
            return;
        }
        Vector3? worldPoint = CastMouseClickRay();
        if (!worldPoint.HasValue)
        {
            return;
        }
        DrawLine(worldPoint.Value);
        if (Input.GetMouseButtonUp(0))
        {
            Shoot(worldPoint.Value);
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false;
        lineRender.enabled = false;
        trailRenderer.enabled = true;
        StartCoroutine(ShowTrailForDuration(1));

        //only horizontal force
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        horizontalWorldPoint = 2 * transform.position - horizontalWorldPoint;

        Vector3 dir = (horizontalWorldPoint - transform.position).normalized;
       // float strn = Vector3.Distance(transform.position, horizontalWorldPoint);

        float power = (lineLength / 2) * shotPower;
        rigidBody.AddForce(dir * power);
        StartCoroutine(CheckVelocityDifference());
        isIdle = false;
    }
    private IEnumerator ShowTrailForDuration(float duration)
    {
        float timer = 0f;
        float fadeDuration = 1f; // How long it should take for the trail to fade out
        Color startColor = trailRenderer.startColor;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        trailRenderer.enabled = true;

        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;

            // Calculate the current alpha value based on how much time has elapsed
            float alpha = Mathf.Clamp01(1f - (timer - duration + fadeDuration) / fadeDuration);

            // Set the trail renderer's color with the new alpha value
            trailRenderer.startColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            trailRenderer.endColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }

        trailRenderer.enabled = false;
    }

    IEnumerator CheckVelocityDifference()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            float velocityDifference = Vector3.Distance(rigidBody.velocity, lastVelocity) / 1;

            if (velocityDifference < 1)
            {
                Stop();
                yield break;
            }

            lastVelocity = rigidBody.velocity;
        }
    }
    private void DrawLine(Vector3 worldPoint)
    {
        Vector3 reflectedPoint = 2 * transform.position - worldPoint;
        Vector3 dir = new Vector3(reflectedPoint.x - transform.position.x, 0, reflectedPoint.z - transform.position.z).normalized;
        float distance = Mathf.Min(Vector3.Distance(transform.position, reflectedPoint), 2);
        Vector3 endPos = transform.position + dir * distance;
        Vector3[] positions =
        {
            transform.position,
            endPos
        };
        lineRender.SetPositions(positions);
        lineRender.enabled = true;
        lineLength = distance;
    }

    private void Stop()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        isIdle = true;
    }
    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if(Physics.Raycast(worldMousePosNear,worldMousePosFar-worldMousePosNear,out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }


}
