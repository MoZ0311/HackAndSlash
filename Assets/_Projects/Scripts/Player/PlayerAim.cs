using UnityEngine;
using System;
using Unity.Cinemachine;

/// <summary>
/// プレイヤーのエイムを管理するクラス
/// </summary>
public class PlayerAim : MonoBehaviour
{
    [SerializeField] Transform muzzleRoot;
    public Vector2 TargetPosition { private get; set; }
    public Vector2 CurrentPosition { get; private set; }
    Vector3 currentVelocity;
    Vector3 rawAimPosition;

    // 狙った位置のy座標を自身の高さに補正して返す
    public Vector3 AimPosition => new(rawAimPosition.x, muzzleRoot.position.y, rawAimPosition.z);

    // 狙った位置へ向けた単位ベクトルを作成して返す
    public Vector3 AimDirection
    {
        get
        {
            Vector3 direction = AimPosition - muzzleRoot.position;

            if (direction.AlmostZero())
            {
                Debug.LogError("AlmostZero");
                return Vector3.zero;
            }

            return direction.normalized;
        }
    }

    Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void HandleAim(float smoothTime)
    {
        // カメラが見つからない or 破棄されていれば早期return
        if (mainCamera == null)
        {
            return;
        }

        CurrentPosition = Vector3.SmoothDamp(
            CurrentPosition,
            TargetPosition,
            ref currentVelocity,
            MathF.Max(0, smoothTime)
        );

        Plane plane = new(Vector3.up, muzzleRoot.position);

        // 照準の座標をもとに、カメラから直進するRayを作成する
        Ray ray = mainCamera.ScreenPointToRay(CurrentPosition);

        //Rayと地面の交点を取得
        if (plane.Raycast(ray, out float hitDistance))
        {
            rawAimPosition = ray.GetPoint(hitDistance);
        }
    }
}
