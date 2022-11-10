using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NumericComparason : BehaviourBase
{
    public string a;
    public Operation operation;
    public string b;
    public EvaluationResult failParseResult;

    protected override EvaluationResult OnExecute()
    {
        if (TryGetValue(a, Tree, out var aVal) && TryGetValue(a, Tree, out var bVal))
        {
            bool result = false;

            if (aVal is int && bVal is int)
            {
                switch (operation)
                {
                    case Operation.LEqual:
                        result = (int)aVal <= (int)bVal;
                        break;
                    case Operation.GEqual:
                        result = (int)aVal >= (int)bVal;
                        break;
                    case Operation.Less:
                        result = (int)aVal < (int)bVal;
                        break;
                    case Operation.Greater:
                        result = (int)aVal > (int)bVal;
                        break;
                    case Operation.Equal:
                        result = (int)aVal == (int)bVal;
                        break;
                    case Operation.NotEqual:
                        result = (int)aVal != (int)bVal;
                        break;
                }
            }

            switch (operation)
            {
                case Operation.LEqual:
                    result = (float)aVal <= (float)bVal;
                    break;
                case Operation.GEqual:
                    result = (float)aVal >= (float)bVal;
                    break;
                case Operation.Less:
                    result = (float)aVal < (float)bVal;
                    break;
                case Operation.Greater:
                    result = (float)aVal > (float)bVal;
                    break;
                case Operation.Equal:
                    result = (float)aVal == (float)bVal;
                    break;
                case Operation.NotEqual:
                    result = (float)aVal != (float)bVal;
                    break;
            }

            return result ? EvaluationResult.Success : EvaluationResult.Failure;
        }

        return failParseResult;
    }

    private bool TryGetValue(string key, BehaviourTree ctx, out object result)
    {
        int i;
        float f;
        if (ctx.blackboard.TryGetValue(key, out i))
        {
            result = i;
            return true;
        }

        if (ctx.blackboard.TryGetValue(key, out f))
        {
            result = f;
            return true;
        }

        if (int.TryParse(key, out i))
        {
            result = i;
            return true;
        }

        if (float.TryParse(key, out f))
        {
            result = f;
            return true;
        }

        result = default;
        return false;
    }

    public enum Operation
    {
        LEqual,
        GEqual,
        Less,
        Greater,
        Equal,
        NotEqual,
    }
}
