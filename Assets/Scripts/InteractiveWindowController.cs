using UnityEngine;
using UnityEngine.UI;

public class InteractiveWindowController : MonoBehaviour
{
    public float moveSpeedContent;
    
    private ScrollRect _scrollRect;
    private bool _isRight, _isLeft;

    void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (_isLeft)
        {
            var content = _scrollRect.content;
            var contentPosition = content.anchoredPosition;
            content.anchoredPosition = new Vector2(contentPosition.x + moveSpeedContent, contentPosition.y);
        }
        else if (_isRight)
        {
            var content = _scrollRect.content;
            var contentPosition = content.anchoredPosition;
            content.anchoredPosition = new Vector2(contentPosition.x - moveSpeedContent, contentPosition.y);
        }
    }

    public void PointerDownLeft()
    {
        _isLeft = true;
    }

    public void PointerUpLeft()
    {
        _isLeft = false;
    }

    public void PointerDownRight()
    {
        _isRight = true;
    }

    public void PointerUpRight()
    {
        _isRight = false;
    }
}