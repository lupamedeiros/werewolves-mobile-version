using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatContent : MonoBehaviour
{
    [Header("UI Components")]
    public ScrollRect scrollRect;
    public RectTransform content; 
    public Scrollbar verticalScrollbar;  

    private RectTransform viewport;   

    void Start()
    {
        if (scrollRect == null || content == null || verticalScrollbar == null)
        {
            Debug.LogError("Certifique-se de que todos os componentes necessários estão atribuídos!");
            return;
        }

        viewport = scrollRect.viewport;
        UpdateScrollbarSize();
    }

    void Update()
    {
        UpdateScrollbarSize(); 
    }
    

    private void UpdateScrollbarSize()
    {
        if (content == null || viewport == null) return;
        
        float contentHeight = content.rect.height;
        float viewportHeight = viewport.rect.height;

        if (contentHeight <= 0 || viewportHeight <= 0) return;
        
        float scrollbarSize = Mathf.Clamp(viewportHeight / contentHeight, 0.1f, 1f);
        verticalScrollbar.size = scrollbarSize;
    }
}
