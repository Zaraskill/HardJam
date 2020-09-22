using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [Header("Rewired Player Settings")]
    public int _PlayerId;
    private Rewired.Player inputPlayer;

    [Header("Cursor Object")]
    public Image cursor;
    private RaycastHit hit;
    public float raduisSphereCast = 0.5f;
    [Space(5)]
    public float leftMax;
    public float rightMax;
    public float UpMax;
    public float DownMax;

    [Header("Player Settings")]
    public LayerMask layerMask;
    public float moveSpeedValue;

    // Start is called before the first frame update
    private void Start()
    {
        inputPlayer = ReInput.players.GetPlayer(_PlayerId);

        hit = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputPlayer.GetButtonDown("Select"))
        {
            CursorRaycast();
        }

        CursorMovement();
    }

    private void CursorMovement()
    {

        float horizontal = 0;
        horizontal = inputPlayer.GetAxis("HorizontalMove");
        float vertical = 0;
        vertical = inputPlayer.GetAxis("VerticalMove");

        Vector2 actualPosition = cursor.rectTransform.anchoredPosition;

        actualPosition.x += horizontal * moveSpeedValue * Time.deltaTime * 100;
        actualPosition.y += vertical * moveSpeedValue * Time.deltaTime * 100;

        if (actualPosition.x < leftMax)
        {
            actualPosition.x = leftMax;
        }
        else if (actualPosition.x > rightMax)
        {
            actualPosition.x = rightMax;
        }

        if (actualPosition.y < DownMax)
        {
            actualPosition.y = DownMax;
        }
        else if (actualPosition.y > UpMax)
        {
            actualPosition.y = UpMax;
        }

        cursor.rectTransform.anchoredPosition = actualPosition;
    }

    private void CursorRaycast()
    {

        if (Physics.SphereCast(cursor.transform.position, raduisSphereCast, cursor.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
//#if UNITY_EDITOR
//            Debug.DrawRay(cursor.transform.position, cursor.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
//            Debug.Log("Did Hit : " + hit.collider.gameObject.name + "" + _PlayerId);
//#endif
        }
        else
        {

//#if UNITY_EDITOR
//            Debug.DrawRay(cursor.transform.position, cursor.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
//            Debug.Log("Did not Hit " + _PlayerId);
//#endif
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        if(hit.transform != null)
            Gizmos.DrawSphere(hit.transform.position, raduisSphereCast);
    }
#endif
}
