using UnityEngine;

public class BarrelGatling : BarrelBase
{
    [SerializeField][Min(0.01f)] float chargeTime;
    [SerializeField][Min(0.01f)] float consumption;

    public override float ChargeRatio => Mathf.Clamp01(currentCharge / MaxCharge);

    bool isFireButtonPressed;
    float currentCharge = 0;
    const float MaxCharge = 100;

    public override void UpdateFire()
    {
        switch (FireState)
        {
            case FireState.Idle:
                currentCharge = 0;
                if (isFireButtonPressed)
                {
                    FireState = FireState.Charging;
                    AudioPlayer.Instance.PlaySE("Charge");
                }
                break;

            case FireState.Firing:
                TryFire();
                break;

            case FireState.Charging:
                float chargeAmount = MaxCharge / chargeTime * Time.deltaTime;
                currentCharge = Mathf.Clamp(currentCharge + chargeAmount, 0, MaxCharge);
                break;

            default:
                break;
        }
    }

    protected override void Fire()
    {
        // チャージが0以下にならないように減算
        currentCharge = Mathf.Clamp(currentCharge - consumption, 0, MaxCharge);

        Quaternion rotation = SpreadUtility.CalculateRandomSpread(BarrelData.SpreadAngle, MuzzleTransform.rotation);
        ShootBullet(rotation);
        if (currentCharge <= 0)
        {
            FireState = FireState.Idle;
        }
    }

    public override void OnFirePerformed()
    {
        isFireButtonPressed = true;
        if (FireState == FireState.Idle)
        {
            FireState = FireState.Charging;
            AudioPlayer.Instance.PlaySE("Charge");
        }
    }

    public override void OnFireCanceled()
    {
        isFireButtonPressed = false;
        if (FireState == FireState.Charging)
        {
            if (ChargeRatio < 1)
            {
                FireState = FireState.Idle;
            }
            else
            {
                FireState = FireState.Firing;
            }
        }
    }
}
