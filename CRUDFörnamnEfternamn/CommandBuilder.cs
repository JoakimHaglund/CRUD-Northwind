using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDFörnamnEfternamn
{
    internal class CommandBuilder
    {
        public enum Action { SELECT,INSERT,UPDATE}
        public CommandBuilder() { }
        public string BuildCommand(Action action, string table, Dictionary<string, string> columNameValue)
        {
            string output = string.Empty;
            if (action == Action.SELECT)
            {
                output = $"SELECT {GetColumNames(columNameValue)} FROM {table}";
            }
            else if (action == Action.INSERT)
            {
                output = CreateInsert(table, columNameValue);
            }
            else if (action == Action.UPDATE)
            {
                output = null;
            }
            return output;
            //INSERT INTO<table>(< columns >) VALUES(< values >)
        }
        private string GetColumNames(Dictionary<string, string> columNameValue)
        {
            string output = string.Empty;
            foreach (KeyValuePair<string, string> item in columNameValue)
            {
                output += item.Key + ", ";
            }
            output = output.Trim();
            output = output.Remove(output.Length - 1, 1);
            return output;
        }
        private string GetColumValues(Dictionary<string, string> columNameValue) 
        {
            string output = string.Empty;
            foreach (KeyValuePair<string, string> item in columNameValue)
            {
                output += item.Value + ", ";
            }
            output = output.Trim();
            output = output.Remove(output.Length - 1, 1);
            return output;
        }
        private string CreateInsert(string table, Dictionary<string, string> columNameValue, bool asParams = false)
        {
            string output = string.Empty;
            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            foreach (KeyValuePair<string, string> item in columNameValue)
            {
                if (item.Value != null)
                {
                    columns.Add(item.Key);
                    if (asParams)
                    {
                        values.Add("@" + item.Key.Substring(0, 1).ToLower() + item.Key.Substring(1));
                    }
                    else
                    {
                        values.Add(item.Value);
                    }
                }
            }
            output = $"INSERT INTO {table} ({string.Join(", ", columns)}) VALUES('{string.Join("', '", values)}')";
            output = output.Trim();
            return output;

        }
    }
}
