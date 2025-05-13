using UnityEngine;

public class ClickCats : MonoBehaviour
{
    [SerializeField] 
    private Sprite foundSprite;
    private bool isClicked;

    private void Start()
    {
        isClicked = false;
    }

    private void Update()
    {
        if (isClicked == false && GameManager.Instance.isUIClosed == true)
        {
            HandleInput();
        }
    }

    // 터치/키보드 입력 시 이미지를 빨간색으로 변경
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isClicked = true;
                GetComponent<SpriteRenderer>().sprite = foundSprite;

                SoundManager.Instance.PlaySFXSound();
                GameManager.Instance.UpdateFoundCatInfo();
            }
        }
    }
}
