using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script covers all the extension methods
/// <para>Contributor: Weizhao</para>
/// </summary>
public static class ExtendMethods 
{
    /// <summary>
    /// Method to check if the package list contains item that
    /// is required by the demand
    /// </summary>
    /// <param name="packages">the package list</param>
    /// <param name="demand">the demand</param>
    /// <returns></returns>
    public static bool ContainsDemandItem(this List<Package> packages, Demand demand)
    {
        foreach (var package in packages)
        {
            if (package.GetPackageItemId() == demand.GetRequiredItemId())
            {
                return true;
            }
        }
                
        return false;
    }

    /// <summary>
    /// Method to pop a random spawning spot from the given list
    /// </summary>
    /// <param name="spots">the given spawn spot list</param>
    /// <returns></returns>
    public static Spot RandomSpot(this List<Spot> spots)
    {
        return spots[UnityEngine.Random.Range(0, spots.Count)];
    }
}