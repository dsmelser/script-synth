using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptSynth
{
    public class DataGridViewTimeColumn : DataGridViewColumn
    {
        DataGridViewCellStyle _style = new DataGridViewTimeStyle();
        DataGridViewCell _template = new DataGridViewTimeCell();


        public DataGridViewTimeColumn() : base()
        {
            this.CellTemplate = _template;
            this.DefaultCellStyle = _style;
        }

    }
}
