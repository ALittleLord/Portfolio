using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static Scaling function to run scaling by type on input floats, and return a scaled integer
/// Call RunScaleFunction to use
/// Required inputs are Scaling.ScalingType, the inputValue as float, the constant of the scaling function as a float, and the level of the function as and integer
/// </summary>
public class Scaling
{
    public enum ScalingType{Polynomal, Exponential}

    /// <summary>
    /// Core public call to run scaling
    /// Delegates to other functions based on inputed scalingType
    /// *Inputs*
    /// scalingType as Scaling.ScalingType; The type of scaling to be run on input values.
    /// scaleInput as float;                The input value that will be scaled and returned as an integer;
    /// scaleConstant as float;             The float constant that decides the strength of the scaling funtion, its usage is slightly different depending on function used;
    /// scaleLevel as int;                  The level of the scaling function. Applied as a magnifier of scaleConstant
    /// *Output*
    /// Scaled value rounded to an integer
    /// </summary>
    public static int RunScaleFunction(ScalingType scalingType, float scaleInput, float scaleConstant, int scaleLevel)
    {
        switch(scalingType)
        {
            case ScalingType.Polynomal:
                return RunPolynomalScaling(scaleInput, scaleConstant, scaleLevel);
            case ScalingType.Exponential:
                return RunExponentialScaling(scaleInput, scaleConstant, scaleLevel);
            default:
                return Mathf.RoundToInt(scaleInput);
        }
    }
    
    /// <summary>
    /// Runs exponential scaling function {input ^ (constant*level)}
    /// Returns an integer
    /// </summary>
    static int RunExponentialScaling(float scaleInput, float scaleConstant, int scaleLevel)
    {
        int returnValue = Mathf.RoundToInt(Mathf.Pow(scaleInput, scaleConstant * scaleLevel));
        return returnValue;
    }

    /// <summary>
    /// Runs polynomial scaling function {input + (constant*level)}
    /// Returns an integer
    /// </summary>
    static int RunPolynomalScaling(float scaleInput, float scaleConstant, int scaleLevel)
    {
        int returnValue = Mathf.RoundToInt(scaleInput + (scaleConstant * scaleLevel));
        return returnValue;
    }
}
