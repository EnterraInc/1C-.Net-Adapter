using System;
using System.Collections.Generic;
using System.Text;
using Enterra.V8x1C.Routines.V8Parsing;

namespace Enterra.V8x1C.Routines.V8Parsing
{
    public class LexemV8
    {
        private List<LexemV8> _childLexemList;
        private StringBuilder _data;
        private int _doubleQuoteCounter = 0;

        public LexemV8()
        {
            LexemType = LexemV8Type.Object; 
        }

        public LexemV8 Parent
        {
            get;
            set;
        }

        public List<LexemV8> ChildLexemList
        {
            get
            {
                return _childLexemList;
            }
        }

        public LexemV8Type LexemType
        {
            get;
            set;
        }

        public string Data
        {
            get
            {
                if (_data == null)
                {
                    return String.Empty;
                }
                return _data.ToString();
            }
        }

        public void AddChar(char ch)
        {
            if (_data == null)
            {
                _data = new StringBuilder();
            }
            _data.Append(ch);
            if (ch == '\"')
            {
                _doubleQuoteCounter++;
            }
        }

        public void AddChild(LexemV8 child)
        {
            if (child == null)
            {
                return;
            }
            if (_childLexemList == null)
            {
                _childLexemList = new List<LexemV8>();
            }
            _childLexemList.Add(child);
            child.Parent = this;
        }

        public LexemV8 FindFirstChild(LexemV8Type lexemType)
        {
            foreach (var ch in ChildLexemList)
            {
                if (ch.LexemType == lexemType)
                {
                    return ch;
                }
            }

            return null;
        }

        internal int DoubleQuoteCount
        {
            get
            {
                return _doubleQuoteCounter;
            }
        }
    }
}
