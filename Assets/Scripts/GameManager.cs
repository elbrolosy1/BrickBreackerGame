using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// تعريف كلاس GameManager الموروث من MonoBehaviour (مكون يمكن إضافته إلى كائنات في Unity)
public class GameManager : MonoBehaviour
{
    public Ball ball {  get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }
    // تعريف المتغيرات الأساسية للعبة
    public int level = 1;    // المستوى الحالي
    public int score = 0;    // النقاط الحالية
    public int lives = 3;    // عدد المحاولات المتبقية

    // يتم استدعاء Awake قبل Start مرة واحدة عند بداية تشغيل اللعبة
    private void Awake()
    {
        // جعل الكائن لا يُحذف عند الانتقال بين المشاهد (الـScenes)
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += onLevelLoaded;
    }

    private void onLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball=FindAnyObjectByType<Ball>();
        this.paddle=FindAnyObjectByType<Paddle>();
        this.bricks=FindObjectsOfType<Brick>();
    }

    // يتم استدعاء Start مرة واحدة عند بداية اللعبة
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // إعادة ضبط القيم عند بدء لعبة جديدة
    private void NewGame()
    {
        this.score = 0;   // إعادة النقاط إلى الصفر
        this.lives = 3;   // إعادة المحاولات إلى 3
        LoadLevel(1);     // تحميل المستوى الأول
    }

    // تحميل مستوى معين وتحديث المتغير
    private void LoadLevel(int level)
    {
        this.level = level;  // تحديث المستوى الحالي
        SceneManager.LoadScene("Level" + level);
    }
    public void Hit(Brick brick)
    {
        this.score += brick.points;
        //if (Cleared()) 
        //{
        //    LoadLevel(this.level + 1);
        //}
    }

    public void Miss()
    {
        lives--;
        if (this.lives > 0)
        {
            ResetLevel();
        }
        else 
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        //SceneManager.LoadScene("GameOver");
        NewGame();
    }

    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
        for (int i = 0; i < this.bricks.Length; i++)
        {
            this.bricks[i].ResetBrick();
        }

    }

    private bool Cleared() 
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable) 
            {
                return false;
            }
        }
        return true;

    }
}
