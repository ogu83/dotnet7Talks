using System.Numerics;

namespace dotnet7Talks.Helpers;

public static class MathExt
{
    public static TResult Sum<T, TResult>(IEnumerable<T> values)
    where T : INumber<T>
    where TResult : INumber<TResult>
    {
        TResult result = TResult.Zero;

        foreach (var value in values)
        {
            result += TResult.CreateChecked(value);
        }

        return result;
    }

    public static TResult Average<T, TResult>(IEnumerable<T> values)
    where T : INumber<T>
    where TResult : INumber<TResult>
    {
        TResult sum = Sum<T, TResult>(values);
        return TResult.CreateChecked(sum) / TResult.CreateChecked(values.Count());
    }

    public static TResult StandardDeviation<T, TResult>(IEnumerable<T> values)
    where T : INumber<T>
    where TResult : IFloatingPointIeee754<TResult>
    {
        TResult standardDeviation = TResult.Zero;

        if (values.Any())
        {
            TResult average = Average<T, TResult>(values);
            TResult sum = Sum<TResult, TResult>(values.Select((value) => {
                var deviation = TResult.CreateSaturating(value) - average;
                return deviation * deviation;
            }));
            standardDeviation = TResult.Sqrt(sum / TResult.CreateSaturating(values.Count() - 1));
        }

        return standardDeviation;
    }
}

