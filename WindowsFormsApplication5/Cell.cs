using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication5
{
    class Cell
    {
        bool state;

        public Cell()
        {
            state = false;
        }
        public Cell(bool state)
        {
            this.state = state;
        }
        public void SetState(bool state)
        {
            this.state = state;
        }
        public bool GetState()
        {
            return state;
        }
    }
}
