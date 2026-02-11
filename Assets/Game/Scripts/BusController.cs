using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UI.Image;

public class BusController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float steerSpeed = 4f;
    [SerializeField] float maxSteerOffset = 10f;
    private float _shootTimer;
    private float _steerValue;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _canon;
    [SerializeField] private Camera _busCamera;
    private bool _isDriving = false;
    private bool _canShoot = true;

    [SerializeField] private VisualEffect _waterCanon;

    internal void Steer(float xSteer)
    {
        float mapped =
    Mathf.Lerp(-1, 1,
        Mathf.InverseLerp(0, 1920, xSteer));
        _steerValue = mapped;
    }

    internal void StartDriving()
    { _isDriving = true; }

    internal void Shoot(float uiX, float uiY)
    {
        if (!_canShoot) return;
        _canShoot = false;

        // Convert pixel coordinates to viewport (0-1)
        Vector3 viewportPos = _busCamera.ScreenToViewportPoint(
            new Vector3(Screen.width - uiX, Screen.height - uiY, 0f)
        );

        // Create a ray from the camera through that point
        Ray ray = _busCamera.ViewportPointToRay(viewportPos);

        Debug.DrawRay(_canon.position, ray.direction * 10f, Color.green, 2f);

        // Use the ray direction as projectile direction
        Vector3 direction = ray.direction.normalized;

        Projectile p = Instantiate(
            _projectilePrefab,
            _canon.position,
            Quaternion.LookRotation(direction)
        );

        p.Initialize(direction);
        _waterCanon.SetVector3("WaterDirection", direction*10f);
        _waterCanon.SetVector3("CanonPosition", _canon.position);
    }




    void Update()
    {
        if (!_isDriving) return;
        _shootTimer += Time.deltaTime;
        if(_shootTimer>= 3f)
        {
            _canShoot=true;
            _shootTimer=0;
        }

        // Always move forward
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;



        // Calculate lateral movement
        Vector3 rightMove = transform.right * _steerValue * steerSpeed * Time.deltaTime;

        // Apply steering
        transform.position += rightMove;

        // Optional: clamp sideways movement
        ClampSideways();
    }

    void ClampSideways()
    {
        Vector3 localPos = transform.localPosition;
        localPos.x = Mathf.Clamp(localPos.x, -maxSteerOffset, maxSteerOffset);
        transform.localPosition = localPos;
    }



}
