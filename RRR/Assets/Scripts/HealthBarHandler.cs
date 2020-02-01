using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarHandler : MonoBehaviour
{
    private SpriteRenderer _healthBarSprite;

    private Transform _healthBarTransform;

    private float _initialXScale;
    // Start is called before the first frame update
    void Start()
    {
        _healthBarTransform = transform.GetChild(1);
        _initialXScale = _healthBarTransform.localScale.x;
        _healthBarSprite = _healthBarTransform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void UpdateHealth(float health)
    {
        Vector3 localScale = _healthBarTransform.localScale;
        localScale = new Vector3(_initialXScale * (health / Config.initialBodyPartHealth), localScale.y, localScale.z);
        _healthBarTransform.localScale = localScale;
        
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
    }

    void SetColor(Color c)
    {
        _healthBarSprite.color = c;
    }
}
