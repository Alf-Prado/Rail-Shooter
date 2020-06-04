//Original code by mixandjam on github, based on the 
//tutorial video Star Fox's Rail Movement | Mix and Jam
//
//https://www.youtube.com/watch?v=JVbr7osMYTo
//https://github.com/mixandjam/StarFox-RailMovement
//Modified to extend functionality and change characteristics

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultiPlayerController : NetworkBehaviour
{
    private Transform playerModel;

    [SyncVar (hook = "ChangeHealth")] public int health = 100;
    public RectTransform healthBar;

    [Header("Settings")]
    public bool joystick = true;

    [Space]

    [Header("Parameters")]
    public float xySpeed = 18;
    public float lookSpeed = 340;
    public float forwardSpeed = 6;

    [Space]

    [Header("Public References")]
    Transform aimTarget;
    GameObject dollyGameObject;
    CinemachineDollyCart dolly;
    Transform cameraParent;

    [Space]

    public GameObject rearView;
    bool rearIsActive;

    [Space]

    public GameObject bodyObj;

    MeshRenderer bodyRenderer;

    [Space]

    private FSM fsm;

    [Space]
    //Player fire rate
    protected float fireRate = 0.20f;
    private double shootCooldown;

    public GameObject bulletPrefab;
    public Transform bulletSpawnLeftPosition;
    public Transform bulletSpawnRightPosition;
    Transform bulletSpawnPosition;

    void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("GameController").GetComponent<Transform>());
        fsm = GameObject.Find("FSM").GetComponent<FSM>();

        rearView = GameObject.Find("RearView");

        if (!isLocalPlayer)
        {
            // Info from C# (CPU) into de Shader (GPU)
            MaterialPropertyBlock propsBodyOther = new MaterialPropertyBlock();

            propsBodyOther.SetColor("_Color", new Color(66 / 255f, 176 / 255f, 245 / 255f));
            bodyRenderer = bodyObj.GetComponent<MeshRenderer>();
            bodyRenderer.SetPropertyBlock(propsBodyOther);
            return;
        }
        
    }

    public override void OnStartServer()
    {
        rearView = GameObject.Find("RearView");
        rearIsActive = false;
    }

    public override void OnStartLocalPlayer()
    {

        rearView = GameObject.Find("RearView");
        // Info from C# (CPU) into de Shader (GPU)
        MaterialPropertyBlock propsBody = new MaterialPropertyBlock();

        propsBody.SetColor("_Color", new Color(235 / 255f, 125 / 255f, 52 / 255f));
        bodyRenderer = bodyObj.GetComponent<MeshRenderer>();
        bodyRenderer.SetPropertyBlock(propsBody);
        shootCooldown = 0;

        aimTarget = GameObject.Find("AimTarget").GetComponent<Transform>();
        dollyGameObject = GameObject.Find("GameController");
        dolly = GameObject.Find("GameController").GetComponent<CinemachineDollyCart>();
        cameraParent = GameObject.Find("CameraHolder").transform;

        playerModel = transform.GetChild(0);
        SetSpeed(forwardSpeed);
        rearIsActive = false;
        //rearView.SetActive(true);
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        if(rearView == null)
        {
            Debug.Log("Searching for rear view");
            rearView = GameObject.Find("RearView");
        }
        else
        {
            if (rearIsActive)
            {
                rearView.SetActive(true);
            }
            else
            {
                rearView.SetActive(false);
            }
        }
       

        float h = joystick ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        float v = joystick ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        LocalMove(h, v, xySpeed);
        RotationLook(h, v, lookSpeed);
        HorizontalLean(playerModel, h, 80, .1f);

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (rearIsActive)
            {
                rearIsActive = false;
            }
            else
            {
                rearIsActive = true;
            }
        }

        //Check if player can shoot
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        //If spacebar is hit, we shoot a bullet
        if (Input.GetKeyDown(KeyCode.Space) && CanAttack)
        {
            shootCooldown = fireRate;
            CmdShootBullet();
        }

        //FSM
        Vector3 my3DPos = transform.position;

        Vector3 myTexturePos = new Vector3((my3DPos.x + 400.0f) * 512.0f / 800.0f, my3DPos.y, (my3DPos.z + 100.0f) * 512.0f / 200.0f);

        for (int y = 0; y < fsm.texture.height; y++)
        {
            for (int x = 0; x < fsm.texture.width; x++)
            {
                Vector2 vTemp = new Vector2(myTexturePos.x - x, myTexturePos.z - y);
                vTemp.Normalize();
                Color color = Color.black;
                color.r = vTemp.x;
                color.g = myTexturePos.y;
                color.b = vTemp.y;
                fsm.texture.SetPixel(x, y, color);

            }
        }
        fsm.texture.Apply();

    }

    public bool CanAttack
    {
        //Check if the player can shoot by checking if the cooldwon has finished
        get
        {
            return shootCooldown <= 0;
        }
    }

    //Method to shoot a bullet
    [Command]
    void CmdShootBullet()
    {
        float velocity;
        
        if (rearIsActive)
        {
            velocity = -30f;
        }
        else
        {
            velocity = 30f;
        }

        if (bulletSpawnPosition == null)
        {
            bulletSpawnPosition = bulletSpawnLeftPosition;
        }

        GameObject newBullet = (GameObject)Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);

        if (bulletSpawnPosition == bulletSpawnLeftPosition)
        {
            bulletSpawnPosition = bulletSpawnRightPosition;
        }
        else
        {
            bulletSpawnPosition = bulletSpawnLeftPosition;
        }

        //Give the bullet velocity
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * velocity;

        //Spawn the bullet for other players
        NetworkServer.Spawn(newBullet);

        //Destroy the bullet after time passed
        Destroy(newBullet, 3f);
    }

    //Players takes damage
    public void IsDamaged(int damage)
    {
        if (!isServer)
        {
            return;
        }

        health -= damage;

        Debug.Log("Health " + health);
        //If health is equal or lower to 0, the player dies
        if (health <= 0)
        {
            
            Destroy(gameObject);
           
            //CustomNetworkManager.singleton.StopClient();
            
        }
        
    }

    void ChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
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

   /* private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }*/

    public void QuickSpin(int dir)
    {
        if (!DOTween.IsTweening(playerModel))
        {
            playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            //barrel.Play();
        }
    }

    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }

    void DistortionAmount(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>().intensity.value = x;
    }

    void FieldOfView(float fov)
    {
        cameraParent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
    }

    void Chromatic(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = x;
    }
    
    
}
