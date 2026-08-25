using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScrewPuzzle/GameConfig")]
public class GameConfig : ScriptableObject
{
    // Số lần Undo cho phép mỗi level
    [Header("Gameplay")]
    public int maxUndoCount = 3;

    // Số coins thưởng khi hoàn thành level (base)
    public int baseCoinReward = 50;

    // Chi phí hint bằng coins
    //public int hintCost = 100;

    // Thời gian fade transition giữa các màn (giây)
    public float screenFadeDuration = 0.3f;

    // Vận tốc plank rơi (đơn vị Unity/giây)
    public float plankFallSpeed = 5f;

    // Vận tốc plank nghiêng (độ/giây)
    public float plankTiltSpeed = 90f;

    [Header("Level")]
    // Level đầu tiên unlock sẵn
    public int startingLevel = 1;

    // Số miss moves tối đa trước khi fail
    //  public int maxMissMoves = 5;

    [Header("Timer")]
    // Thời gian giới hạn mỗi level (giây) — mặc định 3 phút
    public float levelTimeLimit = 180f;

    // Ngưỡng cảnh báo (giây) — khi còn lại ≤ giá trị này → UI đổi màu đỏ
    public float warnThreshold = 10f;
}
