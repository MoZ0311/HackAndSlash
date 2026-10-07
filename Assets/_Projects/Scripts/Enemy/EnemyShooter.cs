using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyShooter : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] Transform playerTarget;      // プレイヤーのTransform
    [SerializeField] Transform muzzleTransform;    // 銃口のTransform（弾の発生位置）
    [SerializeField] Transform muzzleRoot;

    [Header("Settings")]
    [SerializeField] float minDistance = 4.0f;     // 近すぎると判定する距離（これ以下で後退）
    [SerializeField] float maxDistance = 8.0f;     // 遠すぎると判定する距離（これ以上で追従）
    [SerializeField] float strafeSpeedRadius = 5.0f;      // 円運動の参照半径
    [SerializeField] float changeDirectionInterval = 2.0f;  // 横移動の方向を切り替える間隔（秒）

    [SerializeField] EnemyWeapon enemyWeapon;
    [SerializeField] EnemyAnimation enemyAnimation;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] Animator animator;

    NavMeshAgent agent;
    float directionTimer;
    int strafeDirection = 1; // 1: 時計回り, -1: 反時計回り

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agent.updateRotation = false;
    }

    private void Update()
    {
        if (playerTarget == null || !enemyHealth.CanControl)
        {
            agent.isStopped = true;
            return;
        }

        // 敵本体は常に水平方向（Y軸無視）でプレイヤーを向くように回転
        OrientBodyTowardsPlayer();

        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        // 1. 遠すぎる場合：プレイヤーを追従（射撃はしない）
        if (distanceToPlayer > maxDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTarget.position);

            // トリガーを離す
            enemyWeapon.CancelFire();
        }
        // 2. 近すぎる場合：直線的に後退＋牽制射撃（連射間隔長め）
        else if (distanceToPlayer < minDistance)
        {
            RetreatFromPlayer();

            // トリガーを引く
            enemyWeapon.PeerformFire();

        }
        // 3. 適正距離内の場合：カニ歩き（旋回）＋メイン射撃
        else
        {
            UpdateStrafeDirection();
            MoveStrafingAroundPlayer();

            // トリガーを引く
            enemyWeapon.PeerformFire();
        }

        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        enemyAnimation.HandleAnimation(localVelocity);

    }

    // プレイヤーから離れる方向へ後退
    private void RetreatFromPlayer()
    {
        Vector3 retreatDirection = (transform.position - playerTarget.position).normalized;
        Vector3 targetPosition = transform.position + retreatDirection * 3.0f;

        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 3.0f, NavMesh.AllAreas))
        {
            agent.isStopped = false;
            agent.SetDestination(hit.position);
        }
    }

    // カニ歩き移動の方向切り替えタイマー
    private void UpdateStrafeDirection()
    {
        directionTimer += Time.deltaTime;
        if (directionTimer >= changeDirectionInterval)
        {
            directionTimer = 0f;
            strafeDirection = Random.value > 0.5f ? 1 : -1;
        }
    }

    // プレイヤーを中心に円を描くように横移動（カニ歩き）
    private void MoveStrafingAroundPlayer()
    {
        Vector3 fromPlayer = transform.position - playerTarget.position;
        fromPlayer.y = 0;

        // プレイヤーに対する接線方向（外積）を算出
        Vector3 tangent = Vector3.Cross(fromPlayer, Vector3.up).normalized * strafeDirection;
        Vector3 rawTargetPos = transform.position + tangent * strafeSpeedRadius;

        if (NavMesh.SamplePosition(rawTargetPos, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            agent.isStopped = false;
            agent.SetDestination(hit.position);
        }
    }

    // プレイヤーに向ける
    void OrientBodyTowardsPlayer()
    {
        Vector3 lookDir = playerTarget.position - muzzleRoot.position;
        lookDir.y = 0;

        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
