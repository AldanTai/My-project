using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    private Rigidbody2D rb;
    private Vector2 movement;

    public GameObject Flag; // 旗子物件
    private bool nearFlag = false; // 是否靠近旗子

    public GameObject popupWindow; // 彈窗 UI
    public GameObject closeButtonObject; // 關閉按鈕的物件

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 隱藏彈窗 UI 和關閉按鈕
        if (popupWindow != null)
        {
            popupWindow.SetActive(false);
            Debug.Log("PopupWindow 已隱藏");
        }
        else
        {
            Debug.LogError("PopupWindow 未設置！");
        }

        if (closeButtonObject != null)
        {
            closeButtonObject.SetActive(false);
            Debug.Log("CloseButton 已隱藏");
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

        // 檢測滑鼠左鍵點擊關閉按鈕物件
        if (Input.GetMouseButtonDown(0)) // 左鍵點擊
        {
            CheckButtonClick();
        }
    }

    void FixedUpdate()
    {
        // 移動角色
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
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

        if (closeButtonObject != null)
        {
            closeButtonObject.SetActive(true); // 顯示關閉按鈕
        }
    }

    void CheckButtonClick()
    {
        // 創建從滑鼠指向場景的射線
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.gameObject == closeButtonObject)
        {
            Debug.Log("關閉按鈕被點擊！");
            CloseWindow();
        }
    }

    void CloseWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(false); // 隱藏彈窗
        }

        if (closeButtonObject != null)
        {
            closeButtonObject.SetActive(false); // 隱藏按鈕
        }

        Debug.Log("視窗已關閉");
    }
}
