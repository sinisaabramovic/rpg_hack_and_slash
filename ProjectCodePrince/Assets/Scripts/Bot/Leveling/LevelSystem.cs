using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{

    // Use this for initialization

    public bool TEST_CALC_EXP = false;
    public int TEST_monster_Level = 1;
    public float TEST_monster_give_EPX = 50f;

    Expirence expirenceObject = new Expirence();

    private int[] AreaExpirience = {50, 250, 600, 2000};
    private float multiplyFactor = 5f;

    [SerializeField]
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    public float InitialLevel
    {
        get { return initialLevel; }
        set { initialLevel = value; }
    }

    public float Expirience
    {
        get { return expirience; }
        set { expirience = value; }
    }

    public int CurrentArea{
        get { return currentArea; }
        set { currentArea = value; }
    }

    private float initialLevel = 5f;

    private float expirience;

    private int currentLevel = 1;
    private int currentArea = 0;

    private float CalcExpirience(int level){
        float calcini = (initialLevel * level) + (CalcExtraDifficultyFactor(level));
        calcini = calcini * CalcEarnedExpirienceInArea(currentArea);
        calcini = calcini * ExtraDifficultyReduction(level);

        return calcini;
    }

    private float CalcEarnedExpirienceInArea(int area){

        return AreaExpirience[area] + (multiplyFactor * currentLevel);
    }

    private float CalcExtraDifficultyFactor(float level){
        if(level > 0 && level <= 15){
            return 0f;
        }else if(level > 15 && level <= 20){
            return 1f;
        }else if (level > 20 && level <= 30)
        {
            return 3f;
        }else if (level > 30 && level <= 45)
        {
            return 5f;
        }else if (level > 40 && level <= 45)
        {
            return 6f;
        }else{
            return 10f;
        }
    }

    private float ExtraDifficultyReduction(float level){
        if (level > 0 && level <= 15)
        {
            return 1f;
        }
        else if (level > 15 && level <= 20)
        {
            return (1-(level-5)/100);
        }
        else if (level > 20 && level <= 30)
        {
            return 0.8f;
        }
        else if (level > 30 && level <= 35)
        {
            return 0.7f;
        }
        else if (level > 35 && level <= 45)
        {
            return 0.4f;
        }
        else
        {
            return 1f;
        }
    }

    public void AddExpirience(int monsterLevel, float monsterExpAmount){

        float calc_exp = 0f;
        calc_exp = expirenceObject.calcExpirence(monsterLevel, currentLevel, monsterExpAmount, 1);

        //float exp_level = Mathf.Round(((this.expirience + calc_exp) / CalcExpirience(currentLevel)));
        //Debug.Log("MODULE = " + exp_level);

        if(this.expirience + calc_exp > CalcExpirience(currentLevel + 1)){
            this.currentLevel++;
            this.expirience = 0f;
        }else{
            this.expirience += calc_exp;
        }
    }

	void Start () {

	}
	
    private void TESTADDEXP(){
        Debug.Log("*******************************");
        AddExpirience(TEST_monster_Level, TEST_monster_give_EPX);
        Debug.Log("CURRENT LEVEL = " + this.currentLevel);
        Debug.Log("CURRENT EXP = " + this.expirience);
        Debug.ClearDeveloperConsole();
        TEST_CALC_EXP = false;
    }
	// Update is called once per frame
	void Update () {
        
        if(TEST_CALC_EXP){
            TESTADDEXP();
        }
	}
}
