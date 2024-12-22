using UnityEngine;
using UnityEngine.UI; // 引入 UI 命名空間

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    private Rigidbody2D rb;
    private Vector2 movement;

    public GameObject Flag; // 旗子物件
    private bool nearFlag = false; // 是否靠近旗子

    public GameObject popupWindow; // 彈窗 UI
    public Button closeButton; // 關閉按鈕（Button 元素）

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 隱藏彈窗 UI
        if (popupWindow != null)
        {
            popupWindow.SetActive(false);
            Debug.Log("PopupWindow 已隱藏");
        }
        else
        {
            Debug.LogError("PopupWindow 未設置！");
        }

        // 檢查並添加按鈕點擊事件
        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(false); // 遊戲開始時隱藏按鈕
            closeButton.onClick.AddListener(CloseWindow); // 添加點擊事件
        }
        else
        {
            Debug.LogError("CloseButton 未設置！");
        }
    }

    void Update()
    {
        // 玩家輸入左右移動
        movement.x = Input.GetAxis("Horizontal");

        // 檢測是否按下數字 1 並且靠近旗子
        if (nearFlag && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("視窗彈出！");
            ShowWindow();
        }
    }

    void FixedUpdate()
    {
        // 移動角色
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Flag")
        {
            nearFlag = true; // 靠近旗子
            Debug.Log("靠近旗子！");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Flag")
        {
            nearFlag = false; // 離開旗子
            Debug.Log("離開旗子！");
        }
    }

    void ShowWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(true); // 顯示彈窗
        }

        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(true); // 顯示關閉按鈕
        }
    }

    void CloseWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(false); // 隱藏彈窗
        }

        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(false); // 隱藏關閉按鈕
        }

        Debug.Log("視窗已關閉");
    }
}
