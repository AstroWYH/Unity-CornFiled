using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Sprite normal, tool, seed;

    private Sprite currentSprite;   //�洢��ǰ���ͼƬ
    private Image cursorImage;
    private RectTransform cursorCanvas;

    private void Start()
    {
        cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();
        SetCursorImage(normal);
    }

    private void Update()
    {
        if (cursorCanvas == null) return;
        cursorImage.transform.position = Input.mousePosition;
    }

    /// <summary>
    /// �������ͼƬ
    /// </summary>
    /// <param name="sprite"></param>
    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color = new Color(1, 1, 1, 1);
    }
}
