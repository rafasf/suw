using System;
using System.Collections.Generic;
using SUW.Extensions;

namespace SUW
{
    public class InputParser
    {
        private readonly Dictionary<ParameterName, string> _parameters;

        public Dictionary<ParameterName, string> Parameters
        {
            get { return _parameters; }
        }

        public InputParser(string[] parameters)
        {
            _parameters = new Dictionary<ParameterName, string>();

            CreateNamedParameters(parameters);
            
            if (_parameters.Count <= 1)
                throw new ArgumentException("You should gimme more than one parameter.");
        }

        private void CreateNamedParameters(string[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
                _parameters.Add(ParameterNameFor(i), parameters[i].WithoutExtraSpaces() ?? string.Empty);
        }

        private ParameterName ParameterNameFor(int index)
        {
            return (ParameterName)Enum.Parse(typeof(ParameterName), index.ToString());
        }
    }
}