using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerControllerBak : MonoBehaviour
{
    private Transform player;
    public float sideSpeed = 18;
    public float lookSpeed = 340;
    public float forwardSpeed = 6;
    public Transform aimTarget;
    public CinemachineDollyCart dolly;
    public Transform cameraParent;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetChild(0);
        //SetForwardSpeed(forwardSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        LocalMove(h, v, sideSpeed);
        SetSpeed(forwardSpeed);
        RotationLook(h, v, lookSpeed);
        HorizontalLean(player, h, 80, .1f);
    }

    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

}
