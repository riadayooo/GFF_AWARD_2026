// PlayerController2D.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 3.5f;       // 通常速度
    public float dashSpeed = 6.5f;       // ダッシュ速度
    public KeyCode dashKey = KeyCode.LeftShift;

    private Rigidbody2D rb;
    private Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // 回転を固定
    }

    void Update()
    {
        // 入力取得
        float hx = Input.GetAxisRaw("Horizontal"); // 左右
        float vy = Input.GetAxisRaw("Vertical");   // 上下

        // 十字方向のみ（斜め禁止）
        if (Mathf.Abs(hx) > 0.1f && Mathf.Abs(vy) > 0.1f)
        {
            // どちらかを優先（ここでは横優先）
            vy = 0f;
        }

        input = new Vector2(hx, vy).normalized;
    }

    void FixedUpdate()
    {
        // ダッシュ判定
        float speed = Input.GetKey(dashKey) ? dashSpeed : moveSpeed;

        // 移動
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }
}
