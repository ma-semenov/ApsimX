﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Models.Core;
using APSIM.Shared.Utilities;
using System.Globalization;
using System.Linq;

namespace Models.Functions
{
    /// <summary>
    /// [DocumentMathFunction -]
    /// </summary>
    [Serializable]
    [Description("From the value of the first child function, subtract the values of the subsequent children functions")]
    public class SubtractFunction : Model, IFunction
    {
        /// <summary>The child functions</summary>
        private List<IFunction> ChildFunctions;
        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public double Value(int arrayIndex = -1)
        {
            if (ChildFunctions == null)
                ChildFunctions = FindAllChildren<IFunction>().ToList();

            double returnValue = 0.0;
            if (ChildFunctions.Count > 0)
            {
                IFunction F = ChildFunctions[0] as IFunction;
                returnValue = F.Value(arrayIndex);

                if (ChildFunctions.Count > 1)
                {
                    for (int i = 1; i < ChildFunctions.Count; i++)
                    {
                        F = ChildFunctions[i] as IFunction;
                        returnValue = returnValue - F.Value(arrayIndex);
                    }
                }
            }
            return returnValue;
        }
    }
}