using UnityEngine;
using UnityEngine.UI; // تأكد من استيراد مكتبة الـ UI

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // متغير يمسك الـ Text
    private int score = 0;

    // دالة لإضافة النقاط
    public void AddScore(int amount)
    {
        score += amount; // تحديث السكور
        scoreText.text = "Score: " + score; // تحديث النص على الشاشة
    }
}
