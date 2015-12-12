namespace Kingsland.MofParser.Ast
{
    public sealed class FieldAst : MemberAst
    {
        public string Type { get; set; }
        public bool IsArray { get; set; }
        public bool IsRef { get; set; }
        public PrimitiveTypeValueAst Initializer { get; set; }
    }

}
