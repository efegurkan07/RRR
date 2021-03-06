﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BodyPart
{
    public enum BodyPartType
    {
        HORN,
        BODY,
        TAIL
    }

    private int _health;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health < 0)
                _health = 0;
        }
    }

    private BodyPartType _type;
    public BodyPartType Type => _type;


    public BodyPart(BodyPartType type)
    {
        _type = type;
        _health = Config.initialBodyPartHealth;
    }

    public void Repair(SparePart sparePart)
    {
        GameManager.Instance.Inventory.Remove(sparePart);
        _health = (int) Mathf.Clamp(Config.healAmount + _health, 0, Config.maximumHealth);
    }

    public void GetDamaged(int damage)
    {
        Health -= damage;
    }
}
