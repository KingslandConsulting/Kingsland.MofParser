using System;
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
                if ((propertyValue as LiteralValueArrayAst) != null)
                {
                    var itemValues = ((LiteralValueArrayAst)propertyValue).Values
                                                                          .Select(Instance.GetLiteralValue)
                                                                          .ToArray();
                    instance.Properties.Add(property.Key, itemValues);
                }
                else if ((propertyValue as LiteralValueAst) != null)
                {
                    instance.Properties.Add(property.Key, Instance.GetLiteralValue((LiteralValueAst)propertyValue));
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            return instance;
        }

        private static object GetLiteralValue(LiteralValueAst node)
        {
			if ((node as BooleanValueAst) != null)
			{
				return ((BooleanValueAst)node).Value;
			}
			else if ((node as IntegerValueAst) != null)
			{
				return ((IntegerValueAst)node).Value;
			}
			else if ((node as StringValueAst) != null)
			{
				return ((StringValueAst)node).Value;
			}
			else if ((node as NullValueAst) != null)
			{
				return null;
			}
			else
			{
				throw new InvalidOperationException();
			}
        }

    }

}
