using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Settings
{
    public const float itemFadeDuration = 0.35f;

    public const float targetAlpha = 0.45f;

    public const float secondThreshold = 0.012f; //��ֵԽСʱ��Խ��

    public const int secondHold = 59;

    public const int minuteHold = 59;

    public const int hoursHold = 23;

    public const int dayHold = 10;

    public const int seasonHold = 4;

    public const float fadeDuration = 0.8f;

    //�����������
    public const int reapAmount = 2;

    //NPC�����ƶ�
    public const float gridCellSize = 1;
    public const float gridCellDiagonalSize = 1.41f;

    public const float pixelSize = 0.05f;   //20*20ռ1 unit

    public const float animationBreakTime = 5f;     //�������ʱ��

    public const int maxGridSize = 999;

    //�ƹ�
    public const float lightChangeDuration = 25f;

    public static TimeSpan morningTime = new TimeSpan(6, 0, 0);

    public static TimeSpan nightTime = new TimeSpan(19, 0, 0);

    public static Vector3 playerStartPos = new Vector3(-0.5f, -6f, 0);

    public const int playerStartMoney = 100;
}

