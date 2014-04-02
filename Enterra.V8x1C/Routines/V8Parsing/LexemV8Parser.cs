using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.CompilerServices;
using Enterra.V8x1C.Routines.V8Parsing;

namespace Enterra.V8x1C.Routines.V8Parsing
{
    public static class LexemV8Parser
    {
        private const char StartLexem = '{';
        private const char EndLexem = '}';
        private const char DelimiterLexem = ',';
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static LexemV8 Parse(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                return null;
            }
            Stack<LexemV8> _lexemStack = new Stack<LexemV8>();
            LexemV8 lexemV8 = null;
            for (int i = 0; i < data.Length; i++)
            {
                char ch = data[i];
                if (ch == StartLexem
                    && IsValidStringLexem(lexemV8))
                {
                    LexemV8 braceLexemV8 = new LexemV8 { LexemType = LexemV8Type.Brace };
                    if (_lexemStack.Count > 0)
                    {
                        LexemV8 parentBrace = _lexemStack.Peek();
                        parentBrace.AddChild(braceLexemV8);
                    }
                    _lexemStack.Push(braceLexemV8);
                }
                else if (ch == DelimiterLexem
                    && IsValidStringLexem(lexemV8))
                {
                    LexemV8 parentBrace = _lexemStack.Peek();
                    parentBrace.AddChild(lexemV8);
                    lexemV8 = null;
                }
                else if(ch == EndLexem
                    && IsValidStringLexem(lexemV8))
                {
                    LexemV8 parentBrace = _lexemStack.Pop();
                    parentBrace.AddChild(lexemV8);
                    if (_lexemStack.Count == 0)
                    {
                        lexemV8 = parentBrace;
                    }
                    else
                    {
                        lexemV8 = null;
                    }
                }
                else if (NeedAddChar(lexemV8, ch))
                {
                    if (lexemV8 == null)
                    {
                        lexemV8 = new LexemV8();
                    }
                    lexemV8.AddChar(ch);
                }
            }
            return lexemV8;
        }

        private static bool IsValidStringLexem(LexemV8 lexemV8)
        {
            if (lexemV8 == null)
            {
                return true;
            }
            return lexemV8.DoubleQuoteCount % 2 == 0;
        }

        private static bool NeedAddChar(LexemV8 lexemV8, char ch)
        {
            if (lexemV8 == null)
            {
                return !Char.IsWhiteSpace(ch);
            }
            else
            {
                 return true;
            }
        }
    }
}
