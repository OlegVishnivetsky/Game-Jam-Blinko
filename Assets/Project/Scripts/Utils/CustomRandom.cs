using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomRandom
{
    public static Color RandomColor(float a = 1f)
    {
        return new Color(Random.value, Random.value, Random.value, a);
    }

    public static Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value, Random.value);
    }

    public static bool TryWithChanceInPercent(float chance)
    {
        if(Random.value * 100 < chance)
        {
            return true;
        }
        return false;
    }

    public static bool TryWithChanceNormalized(float chance)
    {
        if (Random.value < chance)
        {
            return true;
        }
        return false;
    }

    public static Vector2 GetRandomPointInRectWithOffset(Rect rect, float offset)
    {
        if (rect.width * 0.5f < offset) 
            throw new System.Exception("Getting random value error. Offset is bigger than half of rect width");
        if (rect.height * 0.5f < offset)
            throw new System.Exception("Getting random value error. Offset is bigger than half of rect height");

        var innerRect = new Rect(
            rect.x + offset, 
            rect.y - offset, 
            rect.width - 2 * offset, 
            rect.height - 2 * offset);
        
        return GetRandomPointInRect(innerRect);
    }

    public static Vector2 GetRandomPointInRect(Rect rect)
    {
        var result = new Vector2();
        result.x = Random.Range(rect.xMin, rect.xMax);
        result.y = Random.Range(rect.yMin, rect.yMax);
        return result;
    }

    public static T GetRandomElement<T>(ICollection<T> collection)
    {
        if(collection.Count == 0) throw new System.Exception($"Invalid collection error. CustomRandom recieved collection without elements.");
        var targetItemIndex = Random.Range(0, collection.Count);
        var counter = 0;
        foreach ( var item in collection)
        {
            if( counter == targetItemIndex) return item;
            counter++;
        }
        throw new System.Exception($"Giving random element error. CustomRandom can not give random collection element.");
    }
}