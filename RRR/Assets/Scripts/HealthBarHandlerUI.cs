using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HealthBarHandlerUI : MonoBehaviour
{
    private Image healthBar;
    private RectTransform healthBarRectTransform;
    
    private float initialHeight;
    // Start is called before the first frame update
    void Awake()
    {
        healthBarRectTransform = (RectTransform)transform.GetChild(0);
        healthBar = healthBarRectTransform.GetComponent<Image>();
        initialHeight = healthBarRectTransform.rect.height;
    }

    public void SetHealth(int health)
    {
        if (health > Config.yellowHealthIndicatorValue)
        {
            SetColor(Color.green);
        }
        else if (health > Config.redHealthIndicatorValue && health <= Config.yellowHealthIndicatorValue)
        {
            SetColor(Color.yellow);
        }
        else 
        {
            SetColor(Color.red);
        }

        healthBarRectTransform.localScale = new Vector3(1, (float) health / Config.initialBodyPartHealth, 1);
    }

    void SetColor(Color c)
    {
        healthBar.color = c;
    }
}
