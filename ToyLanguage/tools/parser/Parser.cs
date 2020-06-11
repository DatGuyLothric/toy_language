using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using ToyLanguage.tools.lexer;
using ToyLanguage.tools.lexer.tokenizer;

namespace ToyLanguage.tools.parser
{
    class Parser
    {
        public static int iter = 0;
        public static List<Token> tokens;
        public static LexemTypeEnum lexemTypeEnum = new LexemTypeEnum();

        public SyntaxError error = new SyntaxError();

        public Parser() { }

        public bool start(List<Token> inputTokens)
        {
            tokens = inputTokens;
            while (iter < tokens.Count)
                if (!this.expression())
                    return false;
            return true;
        }

        private bool expression()
        {
            int tempIter = iter;
            return (this.assignExpression() || this.conditionalExpression() || this.loopExpression() || this.print() || this.listInit() || this.hashtableInit() || this.typeOperation()) ? true : (iter = tempIter) != -1 && false;
        }

        private bool assignExpression()
        {
            int tempIter = iter;
            return (!this.var() || !this.assignOperation() || !this.arithmeticalExpression() || !this.eoe()) ? (iter = tempIter) != -1 && false : true;
        }

        private bool conditionalExpression()
        {
            int tempIter = iter;
            if (!this.if_() || !this.lp() || !this.logicalExpression() || !this.rp() || !this.lb())
            {
                iter = tempIter;
                return false;
            }
            while (this.expression()) { }
            if (!this.rb())
            {
                iter = tempIter;
                return false;
            }
            while (true)
            {
                if (!this.else_()) 
                    return true;
                if (!this.lb())
                {
                    iter = tempIter;
                    return false;
                }
                while (this.expression()) { }
                if (!this.rb())
                {
                    iter = tempIter;
                    return false;
                }
            }
        }

        private bool loopExpression()
        {
            int tempIter = iter;
            if (!this.while_() || !this.lp() || !this.logicalExpression() || !this.rp() || !this.lb())
            {
                iter = tempIter;
                return false;
            }
            while (this.expression()) { }
            if (!this.rb())
            {
                iter = tempIter;
                return false;
            }
            return true;
        }

        private bool print()
        {
            int tempIter = iter;
            return (!this.print_() || !this.lp() || !this.arithmeticalExpression() || !this.rp() || !this.eoe()) ? false && (iter = tempIter) != -1 : true;
        }

        private bool typeOperation()
        {
            int tempIter = iter;
            return (!this.insert() || !this.delete()) ? false && (iter = tempIter) != -1 : true;
        }

        private bool listInit()
        {
            int tempIter = iter;
            return (!this.list_() || !this.var() || !this.eoe()) ? false && (iter = tempIter) != -1 : true;
        }

        private bool insert()
        {
            int tempIter = iter;
            return (!this.insert_() || !this.arithmeticalExpression() || !this.into_() || !this.var() || !this.on_() || !this.arithmeticalExpression() || !this.eoe()) ? false && (iter = tempIter) != -1 : true;
        }

        private bool delete()
        {
            int tempIter = iter;
            return (!this.delete_() || !this.arithmeticalExpression() || !this.from_() || !this.var() || !this.eoe()) ? false && (iter = tempIter) != -1 : true;
        }

        private bool hashtableInit()
        {
            int tempIter = iter;
            return (!this.hashtable_() || !this.var() || !this.eoe()) ? false && (iter = tempIter) != -1 : true;
        }

        private bool arithmeticalExpression()
        {
            int tempIter = iter;
            if (!value())
                return false;
            while (true)
            {
                if (!this.operation())
                    return true;
                if (!value())
                {
                    iter = tempIter;
                    return false;
                }
            }
        }

        private bool value()
        {
            int tempIter = iter;
            return (this.var() || this.digit() || this.parenthesesExpression() || this.count() || this.get()) ? true : (iter = tempIter) != -1 && false;
        }

        private bool parenthesesExpression()
        {
            int tempIter = iter;
            return (!this.lp() || !this.arithmeticalExpression() || !this.rp()) ? (iter = tempIter) != -1 && false : true;
        }

        private bool logicalExpression()
        {
            int tempIter = iter;
            return (!this.arithmeticalExpression() || !this.comparisonOperation() || !this.arithmeticalExpression()) ? (iter = tempIter) != -1 && false : true;
        }

        private bool count()
        {
            int tempIter = iter;
            return (!this.countof_() || !this.var() || !this.eoe()) ? false && (iter = tempIter) != -1 : true;
        }

        private bool get()
        {
            int tempIter = iter;
            return (!this.get_() || !this.arithmeticalExpression() || !this.from_() || !this.var() || !this.eoe()) ? false && (iter = tempIter) != -1 : true;
        }

        private bool var()
        {
            return this.match(lexemTypeEnum.types["VAR"]);
        }

        private bool digit()
        {
            return this.match(lexemTypeEnum.types["DIGIT"]);
        }

        private bool lp()
        {
            return this.match(lexemTypeEnum.types["LP"]);
        }

        private bool rp()
        {
            return this.match(lexemTypeEnum.types["RP"]);
        }

        private bool operation()
        {
            return this.match(lexemTypeEnum.types["OP"]);
        }

        private bool assignOperation()
        {
            return this.match(lexemTypeEnum.types["ASSIGN_OP"]);
        }

        private bool comparisonOperation()
        {
            return this.match(lexemTypeEnum.types["COMPARISON_OP"]);
        }

        private bool rb()
        {
            return this.match(lexemTypeEnum.types["RB"]);
        }

        private bool lb()
        {
            return this.match(lexemTypeEnum.types["LB"]);
        }

        private bool if_()
        {
            return this.match(lexemTypeEnum.types["IF"]);
        }

        private bool else_()
        {
            return this.match(lexemTypeEnum.types["ELSE"]);
        }

        private bool while_()
        {
            return this.match(lexemTypeEnum.types["WHILE"]);
        }

        private bool print_()
        {
            return this.match(lexemTypeEnum.types["PRINT"]);
        }

        private bool list_()
        {
            return this.match(lexemTypeEnum.types["LIST"]);
        }

        private bool hashtable_()
        {
            return this.match(lexemTypeEnum.types["HASHTABLE"]);
        }

        private bool insert_()
        {
            return this.match(lexemTypeEnum.types["INSERT"]);
        }

        private bool into_()
        {
            return this.match(lexemTypeEnum.types["INTO"]);
        }

        private bool on_()
        {
            return this.match(lexemTypeEnum.types["ON"]);
        }

        private bool delete_()
        {
            return this.match(lexemTypeEnum.types["DELETE"]);
        }

        private bool from_()
        {
            return this.match(lexemTypeEnum.types["FROM"]);
        }

        private bool get_()
        {
            return this.match(lexemTypeEnum.types["GET"]);
        }

        private bool countof_()
        {
            return this.match(lexemTypeEnum.types["COUNTOF"]);
        }

        private bool eoe()
        {
            return this.match(lexemTypeEnum.types["EOE"]);
        }

        private bool match(Lexem lexem)
        {
            return iter >= tokens.Count ? false : tokens[iter].getLexemType().getType() == lexem.getType() ? iter++ != -1 && true : this.error.add(iter, tokens[iter], lexem) && false;
        }
    }
}
