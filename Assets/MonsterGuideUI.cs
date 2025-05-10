using UnityEngine;

public class MonsterGuideUI : MonoBehaviour
{
    public GameObject MonsterTutorialPanel;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            
            if (hit.collider != null && hit.collider.gameObject == MonsterTutorialPanel)
            {
                MonsterTutorialPanel.SetActive(true);
            }
        }
    }
}
