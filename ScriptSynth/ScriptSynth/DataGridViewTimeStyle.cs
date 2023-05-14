using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScriptSynth
{
    public class DataGridViewTimeStyle : DataGridViewCellStyle
    {
        public DataGridViewTimeStyle() : base()
        {
            this.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Format = "m:ss";
            this.NullValue = "0:00";
            this.Padding = new Padding(0, 0, 2, 0);
            this.WrapMode = DataGridViewTriState.False;
        }
    }
}