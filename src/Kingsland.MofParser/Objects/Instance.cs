﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.Objects
{

    public sealed class Instance
    {

        private Dictionary<string, object> _properties;

        internal Instance()
        {
        }

        public string ClassName
        {
            get;
            private set;
        }

        public string Alias
        {
            get;
            private set;
        }

        public Dictionary<string, object> Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new Dictionary<string, object>();
                }
                return _properties;
            }
        }

        public static Instance FromAstNode(InstanceValueDeclarationAst node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            var instance = new Instance
            {
                ClassName = node.TypeName.Name,
                Alias = node?.Alias?.Name
            };
            foreach (var property in node.PropertyValues.PropertyValues)
            {
                var propertyValue = property.Value;
                switch (propertyValue)
                {
                    case ComplexValueArrayAst complexValueArray:
                        var complexValues = complexValueArray.Values
                            .Select(Instance.GetComplexValue)
                            .ToArray();
                        instance.Properties.Add(property.Key, complexValues);
                        break;
                    case LiteralValueArrayAst literalValueArray:
                        var literalValues = literalValueArray.Values
                            .Select(Instance.GetLiteralValue)
                            .ToArray();
                        instance.Properties.Add(property.Key, literalValues);
                        break;
                    case LiteralValueAst literalValue:
                        instance.Properties.Add(property.Key, Instance.GetLiteralValue(literalValue));
                        break;
                    default:
                        throw new NotImplementedException($"Unhandled property value type '{propertyValue.GetType().FullName}'");
                }
            }
            return instance;
        }

        private static object GetComplexValue(ComplexValueAst node)
        {
            if (node.IsAlias)
            {
                return node.Alias;
            }
            else
            {
                throw new NotImplementedException($"Unhandled value-type complex values.");
            }
        }

        private static object GetLiteralValue(LiteralValueAst node)
        {
            switch (node)
            {
                case BooleanValueAst booleanValue:
                    return booleanValue.Value;
                case IntegerValueAst integerValue:
                    return integerValue.Value;
                case StringValueAst stringValue:
                    return stringValue.Value;
                case NullValueAst _:
                    return null;
                default:
                    throw new NotImplementedException($"Unhandled literal value type '{node.GetType().FullName}'");
            }
        }

    }

}
