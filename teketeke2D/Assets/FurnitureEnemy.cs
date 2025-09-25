using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FurnitureEnemy : MonoBehaviour
{
    public Transform player;              // プレイヤーの Transform をセット
    public float detectRange = 3f;        // プレイヤーに気づく距離
    public float moveSpeed = 2f;          // 追いかける速度
    public float chaseTime = 10f;         // 追跡する最大時間（0なら無限）

    private Rigidbody2D rb;
    private bool isChasing = false;
    private float chaseTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isChasing)
        {
            // プレイヤーが範囲に入ったら追跡開始
            float dist = Vector2.Distance(transform.position, player.position);
            if (dist <= detectRange)
            {
                isChasing = true;
                chaseTimer = chaseTime;
                Debug.Log($"{gameObject.name} が動き出した！");
            }
        }
        else
        {
            // 追跡中の時間管理
            if (chaseTime > 0f)
            {
                chaseTimer -= Time.deltaTime;
                if (chaseTimer <= 0f)
                {
                    isChasing = false;
                    rb.velocity = Vector2.zero; // 停止
                    Debug.Log($"{gameObject.name} が追跡をやめた");
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            // プレイヤー方向に移動
            Vector2 dir = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
