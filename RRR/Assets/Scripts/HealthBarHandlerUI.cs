using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HealthBarHandlerUI : MonoBehaviour
{
    private Image healthBar;
    private RectTransform healthBarRectTransform;
    
    // Start is called before the first frame update
    void Awake()
    {
        healthBarRectTransform = (RectTransform)transform;
        healthBar = healthBarRectTransform.GetComponent<Image>();
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

        healthBarRectTransform.localScale = new Vector3( (float) health / Config.initialBodyPartHealth, 1,1);
    }

    void SetColor(Color c)
    {
        healthBar.color = c;
    }
}
