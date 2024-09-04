using System;

static class Program
{
    public static double ConvertToDouble (string text)
    {
        if (text.Contains('.'))
        {
            string[] numberParts = text.Split('.');
            return double.Parse(numberParts[0]) + double.Parse(numberParts[1]) * Math.Pow(10, -numberParts[1].Length);
        }
        else return double.Parse(text);
    }

    public static bool Validate(string text, double min, double max)
    {
        double value = 0;
        bool parsed = false;
        if (text.Contains('.'))
        {
            string[] numberParts = text.Split('.');

            if (numberParts.Length > 2) return false;
            else
            {
                parsed = double.TryParse(numberParts[0], out value) & double.TryParse(numberParts[1], out double decimals);
                if (parsed) value += decimals * Math.Pow(10, -numberParts[1].Length);
            }
        }
        else parsed = double.TryParse(text, out value);

        return parsed && value >= min && value <= max;
    }

    public static double[] VerhulstModel(double initialValue, double parameter)
    {
        double[] results = new double[101];
        results[0] = initialValue;
        for (int i = 1; i < 101; i++)
        {
            results[i] = parameter * results[i - 1] * (1 - results[i - 1]);
        }
        return results;
    }

    public static double[] VerhulstModel(string initialValue, string parameter) => VerhulstModel(ConvertToDouble(initialValue), ConvertToDouble(parameter));
}
