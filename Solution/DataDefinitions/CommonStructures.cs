using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.DataDefinitions
{
    public class SearchInput
    {
        public SearchInput(string entity, List<SearchParameter> parameters)
        {
            Entity = entity;
            Parameters = parameters;
        }

        public string Entity { get; set; }
        public List<SearchParameter> Parameters { get; set; }
    }

    public class SearchParameter
    {
        public SearchParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
