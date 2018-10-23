using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sino.CharacterStats{
    public class PlayerCharacter : MonoBehaviour
    {

        public string CharacterName;

        public bool boolAddWeapon = false;
        public bool boolAddShield = false;

        public float CharacterAgility;
        public float CharacterIntelligence;
        public float CharacterVitality;
        public float CharacterStrength;
        [Space]
        public float CharacterDamage;
        public float CharacterMoveSpeed;
        public float CharacterAttackSpeed;
        public float CharacterHealth;
        public float CharacterAttackDistance;
        public float CharacterBlockDamage;
        [Space]
        public CharacterStats Strength;
        public CharacterStats Agility;
        public CharacterStats Intelligence;
        public CharacterStats Vitality;

        [Space]
        public CharacterStats DamageImpact;
        public CharacterStats MoveSpeed;
        public CharacterStats Health;
        public CharacterStats AttackSpeed;
        public CharacterStats AttackDsiatnce;
        public CharacterStats AttackSpeedAnimation;
        public CharacterStats BlockAmount;

        public GameObject WeaponPrefab;
        public GameObject ShieldPrefab;

        public GameObject Weapon;
        public GameObject Shield;

        public Transform RightHandPivot;
        public Transform LeftHandPivot;

        public Transform ShieldPivot;

        private Animator animator;

        public void AddShield()
        {
            if (Shield != null)
            {
                Destroy(Shield);
            }

            if (ShieldPrefab == null)
            {
                return;
            }

            Shield = Instantiate(ShieldPrefab, ShieldPivot);
            Shield.transform.parent = ShieldPivot;
            Shield.transform.localEulerAngles = Shield.GetComponent<Shield>().handRotation;
            Shield.transform.localPosition = Shield.GetComponent<Shield>().handPosition;

            Shield.GetComponent<Shield>().Equip(this);

            CharacterBlockDamage = BlockAmount.Value;
        }

        public void AddWeapon()
        {
            if (Weapon != null)
            {
                Destroy(Weapon);
            }

            if (WeaponPrefab == null)
            {
                return;
            }

            Weapon = Instantiate(WeaponPrefab, RightHandPivot);
            Weapon.transform.parent = RightHandPivot;
            Weapon.transform.localEulerAngles = Weapon.GetComponent<Weapon>().handRotation;
            Weapon.transform.localPosition = Weapon.GetComponent<Weapon>().handPosition;
            animator.SetFloat("AttackSpeedMultiplier", AttackSpeedAnimation.Value);

            Weapon.GetComponent<Weapon>().Equip(this);

            CharacterDamage = DamageImpact.Value;
            CharacterMoveSpeed = MoveSpeed.Value;
            CharacterAttackSpeed = AttackSpeed.Value;
            CharacterAttackDistance = AttackDsiatnce.Value;

        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Strength = new CharacterStats(CharacterStrength);
            Health = new CharacterStats(CharacterHealth);
            MoveSpeed = new CharacterStats(CharacterMoveSpeed);
            AttackSpeed = new CharacterStats((float)CharacterAttackSpeed);
            AttackSpeedAnimation = new CharacterStats((float)1.0f);
            DamageImpact = new CharacterStats(CharacterDamage);
            BlockAmount = new CharacterStats(CharacterBlockDamage);

            CharacterName = "BASIC";

        }

        private void Update()
        {
            if (boolAddWeapon)
            {
                AddWeapon();
                boolAddWeapon = false;
            }

            if (boolAddShield)
            {
                AddShield();
                boolAddShield = false;
            }
        }

    }

}
