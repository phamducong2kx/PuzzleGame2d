using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public static class DailyRewardTimeLogic
{
    private const long TicksPerDay = 86_400_000L;



    //ham tinh thoi giancon lai 
    public static TimeSpan GetRemainingTime()
    {

        //tick hien tai
        long currentTick = (uint)System.Environment.TickCount;
        //  Debug.Log($"tick hien tai la {currentTick}");
        //tich tick cuoi
        long lastTick = SaveManager.Data.lastClaimOSTicks;
        //  Debug.Log($"tick cuoi la {lastTick}");
        //date cuoi
        long lastDatetime = SaveManager.Data.lastClaimDateTime;
        //   Debug.Log($"datetime cuoi la {lastDatetime}");

        //check
        if (lastTick == 0 || lastDatetime == 0)
        {
            //   Debug.Log("vao day 0");
            return TimeSpan.Zero;
        }
        //neu may ko bi reset
        if (currentTick > lastTick)
        {

            long tickElapse = currentTick - lastTick;
            if (tickElapse >= TicksPerDay)
            {
                //  Debug.Log("vao day 1");
                return TimeSpan.Zero;
            }


            long remain_tick = TicksPerDay - tickElapse;
            return TimeSpan.FromMilliseconds(remain_tick);
        }
        //truong hop may bi reset thi tinh timespan dua tren datetime utc
        else
        {
            //datetime hien tai
            long currentDatetime = DateTime.UtcNow.Ticks;
            //tich tick cuoi
            //Debug.Log($"datetime hien tai la {currentDatetime}");
            //neu user change time phone
            if (lastDatetime > currentDatetime)
            {
                //cho ueese nhận luôn , thích thì chiều
                // Debug.Log("vao day 2");
                return TimeSpan.Zero;
            }
            else
            {
                long timeElapse = currentDatetime - lastDatetime;
                if (timeElapse >= TimeSpan.TicksPerDay) return TimeSpan.Zero;
                long remainTime = TimeSpan.TicksPerDay - timeElapse;
                return TimeSpan.FromTicks(remainTime);
            }

        }


    }

    //ham convert timespan sang string
    public static string ConvertTimeSpantoString(TimeSpan time)
    {

        int hours = time.Hours;
        int minutes = time.Minutes;
        int seconds = time.Seconds;
        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }



}




