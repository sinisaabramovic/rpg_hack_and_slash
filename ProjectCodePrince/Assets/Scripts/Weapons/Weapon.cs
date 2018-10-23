using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sino.CharacterStats;

public class Weapon : MonoBehaviour {

    public Vector3 handPosition;
    public Vector3 handRotation;

    public string WeaponName;
    public Texture2D WeaponImage;

    public float Radius;
    public float Damage;
    public float AttackDistance;
    public float AttackSpeed;
    public float AttackSpeedAnimation;

    public StatModType ModAttackDistance;
    public StatModType ModTypeDamage;
    public StatModType ModTypeSpeed;

    private StatsModifier attackSpeed;
    private StatsModifier attackDistance;
    private StatsModifier damage;
    private StatsModifier attackSpeedAnimation;
    public string AttackAnimParam;

    private Sino.CharacterStats.PlayerCharacter character;

    public void Equip(Sino.CharacterStats.PlayerCharacter c){

        character = c;
        attackSpeed = new StatsModifier(AttackSpeed, ModTypeSpeed, this);
        attackDistance = new StatsModifier(AttackDistance, ModAttackDistance, this);
        attackSpeedAnimation = new StatsModifier(AttackSpeedAnimation, ModTypeSpeed, this);
        damage = new StatsModifier(Damage, ModTypeDamage, this);

        c.AttackSpeedAnimation.AddModifier(attackSpeedAnimation);
        c.AttackSpeed.AddModifier(attackSpeed);
        c.AttackDsiatnce.AddModifier(attackDistance);
        c.DamageImpact.AddModifier(damage);
    }

    public void UnEquip(Sino.CharacterStats.PlayerCharacter c)
    {
        c.AttackSpeed.RemoveAllModifiersFromSource(this);
        c.DamageImpact.RemoveAllModifiersFromSource(this);
        c.AttackDsiatnce.RemoveAllModifiersFromSource(this);
        c.AttackSpeedAnimation.RemoveAllModifiersFromSource(this);
    }

    public void Awake()
    {
        //character = GetComponent<Character>();
        //transform.root.GetComponent<SendTarget>()
        //character = transform.root.GetComponent<Character>();
        //print(character.name);
        //Equip(character);
    }

    public void OnDestroy()
    {
        if(character != null){
            UnEquip(character);
        }
    }
}
