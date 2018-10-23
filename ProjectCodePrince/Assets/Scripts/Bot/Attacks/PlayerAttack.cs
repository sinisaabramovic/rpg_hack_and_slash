using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sino.CharacterStats;

public class PlayerAttack : MonoBehaviour {

    // Use this for initialization

    public Image filledWaitImage_1;
    public Image filledWaitImage_2;
    public Image filledWaitImage_3;
    public Image filledWaitImage_4;
    public Image filledWaitImage_5;
    public Image filledWaitImage_6;

    private int[] fadeImages = new int[] {0, 0, 0, 0, 0, 0};

    private Animator animator;
    private bool canAttack = false;
    private Vector3 enemyTarget;

    private PlayerMove playerMove;
    private PlayerMoveAgent playerMoveAgent;

    public LayerMask enemyMask;
    private float currentAttackTime = 0f;

    private Sino.CharacterStats.PlayerCharacter Character;


    public bool CanAttack{
        get { return canAttack; }
        set { canAttack = value; }
    }

	void Awake () {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerMoveAgent = GetComponent<PlayerMoveAgent>();
        Character = GetComponent<Sino.CharacterStats.PlayerCharacter>();

	}

	// Update is called once per frame
	void Update () 
    {
        if(Character.Weapon != null){
            Attack();    
        }

	}

    public void PlayerAttacks(){

        if (currentAttackTime >= Character.CharacterAttackSpeed && canAttack)
        {

            //GetCurrentAnimatorStateInfo(0).IsName(current_state_name)
            //animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            int atkRange = Random.Range(1, 3);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemyTarget - transform.position), 15f * Time.deltaTime);
            animator.SetInteger(Character.Weapon.GetComponent<Weapon>().AttackAnimParam, atkRange);
            animator.SetBool("Run", false);
            currentAttackTime = 0f;
        }
        else
        {
            animator.SetInteger(Character.Weapon.GetComponent<Weapon>().AttackAnimParam, 0);
            currentAttackTime += Time.deltaTime;
        }
    }

    void Attack(){
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, enemyMask))
            {
                
                if(hit.collider.gameObject.tag == "Enemy"){
                    
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                                      Quaternion.LookRotation(hit.transform.position - transform.position),
                                      15f * Time.deltaTime);
                    
                    if (Vector3.Distance(transform.position, hit.point) <= Character.CharacterAttackDistance)
                    {
                        enemyTarget = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        canAttack = true;
                    }
                }
            }
        }
        PlayerAttacks();
    }

    //void CheckInput(){
    //    if(animator.GetInteger("Atk") == 0){
    //        playerMove.FinishedMovement = false;

    //        if(!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Stand")){
    //            playerMove.FinishedMovement = true;
    //        }                     
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        playerMove.TargetPosition = transform.position;

    //        if (playerMove.FinishedMovement && fadeImages[0] != 1 && canAttack)
    //        {
    //            fadeImages[0] = 1;
    //            animator.SetInteger("Atk", 1);
    //        }
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        playerMove.TargetPosition = transform.position;

    //        if (playerMove.FinishedMovement && fadeImages[1] != 1 && canAttack)
    //        {
    //            fadeImages[1] = 1;
    //            animator.SetInteger("Atk", 2);
    //        }
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        playerMove.TargetPosition = transform.position;

    //        if (playerMove.FinishedMovement && fadeImages[2] != 1 && canAttack)
    //        {
    //            fadeImages[2] = 1;
    //            animator.SetInteger("Atk", 3);
    //        }
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        playerMove.TargetPosition = transform.position;

    //        if (playerMove.FinishedMovement && fadeImages[3] != 1 && canAttack)
    //        {
    //            fadeImages[3] = 1;
    //            animator.SetInteger("Atk", 4);
    //        }
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha5))
    //    {
    //        playerMove.TargetPosition = transform.position;

    //        if (playerMove.FinishedMovement && fadeImages[4] != 1 && canAttack)
    //        {
    //            fadeImages[4] = 1;
    //            animator.SetInteger("Atk", 5);
    //        }
    //    }
    //    else if (Input.GetMouseButtonDown(1))
    //    {
    //        playerMove.TargetPosition = transform.position;

    //        if (playerMove.FinishedMovement && fadeImages[5] != 1 && canAttack)
    //        {
    //            fadeImages[5] = 1;
    //            animator.SetInteger("Atk", 6);
    //        }
    //    }
    //    else
    //    {
    //        animator.SetInteger("Atk", 0);
    //    }

    //    if(Input.GetKey(KeyCode.Space)){
    //        Vector3 targetPos = Vector3.zero;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        RaycastHit hit;

    //        if(Physics.Raycast(ray, out hit)){
    //            targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);

    //            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), 15f * Time.deltaTime);

    //        }
    //    }
    //}

    //void CheckToFade(){
    //    if(fadeImages[0] == 1){
    //        if(FadeAndWait(filledWaitImage_1, 1.0f)){
    //            fadeImages[0] = 0;
    //        }
    //    }
    //    if (fadeImages[1] == 1)
    //    {
    //        if (FadeAndWait(filledWaitImage_2, 0.7f))
    //        {
    //            fadeImages[1] = 0;
    //        }
    //    }
    //    if (fadeImages[2] == 1)
    //    {
    //        if (FadeAndWait(filledWaitImage_3, 1.0f))
    //        {
    //            fadeImages[2] = 0;
    //        }
    //    }
    //    if (fadeImages[3] == 1)
    //    {
    //        if (FadeAndWait(filledWaitImage_4, 0.3f))
    //        {
    //            fadeImages[3] = 0;
    //        }
    //    }
    //    if (fadeImages[4] == 1)
    //    {
    //        if (FadeAndWait(filledWaitImage_5, 0.5f))
    //        {
    //            fadeImages[4] = 0;
    //        }
    //    }
    //    if (fadeImages[5] == 1)
    //    {
    //        if (FadeAndWait(filledWaitImage_6, 0.8f))
    //        {
    //            fadeImages[5] = 0;
    //        }
    //    }
    //}

    //bool FadeAndWait(Image fadeImage, float fadeTime){
    //    bool faded = false;

    //    if(fadeImage == null){
    //        return faded;
    //    }

    //    if(!fadeImage.gameObject.activeInHierarchy){
    //        fadeImage.gameObject.SetActive(true);
    //        fadeImage.fillAmount = 1f;
    //    }

    //    fadeImage.fillAmount -= fadeTime * Time.deltaTime; 

    //    if(fadeImage.fillAmount <= 0.0f){
    //        fadeImage.gameObject.SetActive(false);
    //        faded = true;
    //    }

    //    return faded;
    //}

}
