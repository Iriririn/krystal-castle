using UnityEngine;

public class GuideUI : MonoBehaviour
{
    public GameObject tutorialPanel;
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tutorialPanel.SetActive(true);
        }
    }

}
