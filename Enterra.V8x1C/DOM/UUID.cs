using System;
using System.Collections.Generic;
using System.Text;
using Enterra.V8x1C.Routines;
using Enterra.V8x1C.Routines.V8Parsing;

namespace Enterra.V8x1C.DOM
{
    /// <summary>
    /// УникальныйИдентификатор (UUID)
    /// </summary>
    public class UUID : BaseSessionObject
    {
        private string _uuidStr;

        internal UUID(Session session, string uuidStr)
            : base(session, null)
        {
            _uuidStr = uuidStr;
        }

        internal UUID(Session session, object ptr)
            : base(session, ptr)
        {
            _uuidStr = null;
        }

        public override string ToString()
        {
            if (_uuidStr == null)
            {
                var str = Session.ValueToStringInternal(Ptr);

                var lex = LexemV8Parser.Parse(str);
                if (lex.ChildLexemList.Count == 3)
                {
                    _uuidStr = lex.ChildLexemList[2].Data;
                }
            }

            return _uuidStr;
        }

        protected override void OnPtrRequired()
        {
            Ptr = Session.NewObject("UUID", _uuidStr);
        }
    }
}
