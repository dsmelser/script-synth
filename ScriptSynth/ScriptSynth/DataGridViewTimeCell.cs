using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptSynth
{
    public class DataGridViewTimeCell : DataGridViewCell
    {
        public DataGridViewTimeCell() : base()
        {
            this.ValueType = typeof(DateTime);
            this.Style = new DataGridViewTimeStyle();
        }
    }
}
