using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.ViewModels.Common
{
    public record ComboBoxItemViewModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public bool Disabled { get; set; }

        // default constructor
        public ComboBoxItemViewModel()
        {

        }
        //constructor
        public ComboBoxItemViewModel(string label, string value)
        {
            Label = label;
            Value = value;
            Disabled = false;
        }

        //overload constructor
        public ComboBoxItemViewModel(string label, string value, bool disabled)
        {
            Label = label;
            Value = value;
            Disabled = disabled;
        }
    }
}
