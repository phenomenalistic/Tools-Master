using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif
using UnityEngine.UI;

public static class Notifications
{
    
    static string
        largeIconName = "icon_0",
        smallIconName = "icon_1"
        ;
    public static void Init() // if notif...status is on
    {
        //#if UNITY_ANDROID
        DeleteAllNotificalion(); // удалить все уведомления
        CreateNotificationChannels();
        ScheduleNotifications();
        //#endif
    }

    public static void ScheduleNotifications() // заплпнировать уведомления на день
    {
        DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 16);

        if (DateTime.Now.Hour >= 19) { time = time.AddDays(1); }

        CreateNotification("👉 You top of the top! 👈", "Will we continue to win? 🎮", time, notificationsCID, largeIconName);
        time = time.AddDays(1);

        CreateNotification("It's Words Fight Time!", "We are waiting for you! 👀", time, notificationsCID, largeIconName);
        time = time.AddDays(1);

        CreateNotification("Hey winner! 🥇", "We miss you!👀", time, notificationsCID, largeIconName);
        time = time.AddDays(1);

        CreateNotification("👉 C'mon, try it on more time! 👈", "We miss you! 💔", time, notificationsCID, largeIconName);
        time = time.AddDays(1);

        CreateNotification("Hey winner! 🥇", "We miss you!👀", time, notificationsCID, largeIconName);
        time = time.AddDays(1);

        CreateNotification("It's Words Fight Time!", "Play some more! 🎮", time, notificationsCID, largeIconName);
        time = time.AddDays(1);

        CreateNotification("👉 C'mon, try it on more time! 👈", "We miss you!👀", time, notificationsCID, largeIconName);
        time = time.AddDays(1);

        CreateNotification("Hey winner! 🥇", "We miss you!👀", time, notificationsCID, largeIconName);
        time = time.AddDays(1);

    }

    public static void DeleteAllNotificalion()
    {
        //#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllNotifications(); // удалить все уведомления
                                                            //#endif
    }


    public static void CreateNotification(string title, string body, DateTime deliveryTime, string channelId, string largeIcon)
    {
        //#if UNITY_ANDROID
        var notification = new AndroidNotification();

        notification.Title = title;
        notification.Text = body;
        notification.FireTime = deliveryTime;
        notification.Color = Color.white;
        notification.LargeIcon = largeIcon;
        notification.SmallIcon = smallIconName;
        notification.ShouldAutoCancel = true;

        AndroidNotificationCenter.SendNotification(notification, channelId);
        //#endif
    }

    public static string
        notificationsCID = "notifications";
    private static void CreateNotificationChannels()
    {
        //#if UNITY_ANDROID
        var intervalGiftsChannel = new AndroidNotificationChannel()
        {
            Id = notificationsCID,
            Name = "Basic",
            Importance = Importance.Default,
            Description = "Standard notifications",
            EnableLights = false,
            CanShowBadge = true
        };
        AndroidNotificationCenter.RegisterNotificationChannel(intervalGiftsChannel);
        //#endif
    }

    private static int startN = 7, endN = 21;
    public static DateTime InsertInTimeFrame(DateTime time) // вычисляет время с учетом временных рамок доставки уведомлений с startN до endN
    {
        if (time.Hour > endN)
        {
            time = time.AddDays(1);
            return new DateTime(time.Year, time.Month, time.Day, startN, UnityEngine.Random.Range(10, 20), 0);
        }
        else if (time.Hour < startN)
        {
            return new DateTime(time.Year, time.Month, time.Day, startN, UnityEngine.Random.Range(10, 20), 0);
        }
        else
        {
            return time;
        }
    }
    
}


