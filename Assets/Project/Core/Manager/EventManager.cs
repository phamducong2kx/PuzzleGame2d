using System;
using System.Collections.Generic;


public static class EventManager
{
    // === BOLT EVENTS ===

    public static event Action<Bolt> OnBoltPickedUp;

    public static event Action<Bolt> OnBoltPlaced;

    // === PLANK EVENTS ===
    public static event Action<Plank> OnPlankFallComplete;



    // === ECONOMY EVENTS ===
    public static event Action<int> OnCoinsEarned;

    //jieu ung khi win man choi
    public static event Action<int, int> OnLevelComplete;
    //khi refresh man choi
    public static event Action OnRefreshLevel;
    //suej kien unlock level:
    public static event Action OnUnlockLevel;

    //daily reward
    public static event Action<List<Item_DailyReward>> OnGetDailyReward;


    //get item
    public static event Action OnGetItem;



    // === INVOKE METHODS ===

    public static void InvokeGetItem() => OnGetItem?.Invoke();
    public static void InvokeBoltPickedUp(Bolt bolt) => OnBoltPickedUp?.Invoke(bolt);
    public static void InvokeBoltPlaced(Bolt bolt) => OnBoltPlaced?.Invoke(bolt);

    public static void InvokePlankFallComplete(Plank plank) => OnPlankFallComplete?.Invoke(plank);

    //
    public static void InvokeLevelComplete(int oldStar, int newStar) => OnLevelComplete?.Invoke(oldStar, newStar);

    public static void InvokeRefreshLevel() => OnRefreshLevel?.Invoke();

    public static void InvokeUnlockLevel() => OnUnlockLevel?.Invoke();


    public static void InvokeGetDailyReward(List<Item_DailyReward> x) => OnGetDailyReward?.Invoke(x);


    public static void InvokeCoinsEarned(int coins) => OnCoinsEarned?.Invoke(coins);



    // === UTILITY ===
    public static void ClearAllEvents()
    {

        OnBoltPickedUp = null;
        OnBoltPlaced = null;
        OnPlankFallComplete = null;
        OnRefreshLevel = null;
        OnCoinsEarned = null;
        OnLevelComplete = null;
    }
}
