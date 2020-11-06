using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiyo.SearchParam
{
    public class HomeParam:BaseParam
    {
        //用于判断是否隐藏 各部门output都是0的天
        public bool  IsDisplayOffday { get; set; }
    }
}
