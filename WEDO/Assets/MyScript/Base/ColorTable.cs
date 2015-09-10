using System;
using UnityEngine;

public class ColorTable
{
    public static Color A1 = getColor(255, 255, 255);
    public static Color A2 = getColor(203, 204, 203);
    public static Color A3 = getColor(178, 179, 178);
    public static Color A4 = getColor(148, 149, 148);
    public static Color A5 = getColor(129, 128, 129);
    public static Color A6 = getColor(74, 75, 74);
    public static Color A7 = getColor(52, 51, 52);
    public static Color A8 = getColor(0, 0, 0);
    public static Color B1 = getColor(255, 110, 206);
    public static Color B2 = getColor(255, 103, 255);
    public static Color B3 = getColor(255, 0, 255);
    public static Color B4 = getColor(255, 1, 129);
    public static Color B5 = getColor(255, 0, 0);
    public static Color B6 = getColor(255, 101, 101);
    public static Color B7 = getColor(255, 128, 0);
    public static Color B8 = getColor(255, 204, 104);
    public static Color C1 = getColor(255, 255, 3);
    public static Color C2 = getColor(203, 255, 102);
    public static Color C3 = getColor(0, 255, 0);
    public static Color C4 = getColor(0, 128, 0);
    public static Color C5 = getColor(0, 128, 126);
    public static Color C6 = getColor(0, 127, 255);
    public static Color C7 = getColor(102, 204, 255);
    public static Color C8 = getColor(0, 0, 255);

    public static Color[,] Table= new Color[3, 8]{
    {A1, A2, A3, A4, A5, A6, A7, A8},
    {B1, B2, B3, B4, B5, B6, B7, B8},
    {C1, C2, C3, C4, C5, C6, C7, C8}
    };

    public static Color getColor(string colorname)
    {
        int row = colorname.ToCharArray()[0] - 'A';
        int col = colorname.ToCharArray()[1] - '1';
        return Table[row, col];
    }


    public static Color getColor(int r, int g, int b)
    {
        float rr = ((float)r) / 255;
        float gg = ((float)g) / 255;
        float bb = ((float)b) / 255;

        return new Color(rr, gg, bb);
    }
}
