using System;
using System.Collections.Generic;

class RPNCalculator
{
    static void Main()
    {
        string expression = "";
        double result = EvaluateRPN(expression);
        Console.WriteLine($"Le résultat de l'expression RPN est : {result}");
    }

    static double EvaluateRPN(string expression)
    {
        if (expression.Count() == 0)
        {
            throw new ArgumentNullException(nameof(expression), "L'expression ne peut pas être nulle.");
        }

        Stack<double> stack = new Stack<double>();
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                stack.Push(number);
            }
            else if (IsOperator(token))
            {
                double operand2 = stack.Pop();
                double operand1 = stack.Pop();
                double result = PerformOperation(token, operand1, operand2);
                stack.Push(result);
            }
            else
            {
                throw new ArgumentException($"Token non valide : {token}");
            }
        }

        return stack.Pop();
    }

    static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    static double PerformOperation(string op, double x, double y)
    {
        switch (op)
        {
            case "+":
                return x + y;
            case "-":
                return x - y;
            case "*":
                return x * y;
            case "/":
                if (y == 0)
                {
                    throw new DivideByZeroException("Division par zéro.");
                }
                return x / y;
            default:
                throw new ArgumentException($"Opérateur non valide : {op}");
        }
    }
}
