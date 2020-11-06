using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.Enum.Production;
using Taiyo.Enum.Organization;

namespace StatusConventor
{

    /// <summary>
    /// 原本状态显示是根据数据库中的机器状态
    /// 都是由client记录都没统一很乱. 
    /// 
    /// 现在通过StatusConventor统一转换成Taiyo.Enum.Production下的对应Enum
    /// </summary>

        

    public static class StatusConventor
    {       
        public static MouldingStatus ConventMoulding(string status)
        {
            if (string.IsNullOrEmpty(status))
                throw new ArgumentNullException("Status can not empty or null !");


            switch (status.ToUpper())
            {
                case "1":
                    return MouldingStatus.Running;
                case "1":
                    return MouldingStatus.MaterialTesting;
                case "1":
                    return MouldingStatus.MouldTesting;
                case "1":
                    return MouldingStatus.Adjustment;
                case "1":
                    return MouldingStatus.NoSchedule;
                case "1":
                    return MouldingStatus.ChangeModel;
                case "1":
                    return MouldingStatus.NoOperator;
                case "1":
                    return MouldingStatus.LoginOut;
                case "1":
                    return MouldingStatus.NoMaterial;
                case "1":
                    return MouldingStatus.LoginLate;
                case "1":
                    return MouldingStatus.NoSchedule;
                case "1":
                    return MouldingStatus.BreakTime;
                case "1":
                    return MouldingStatus.MachineBreak;
                case "1":
                    return MouldingStatus.DamageMould;
                case "1":
                    return MouldingStatus.Shutdown;               
                default:
                    throw new ArgumentNullException("Status can not empty or null !");
            }
        }

        public static LaserStatus ConventLaser(string status)
        {
            if (string.IsNullOrEmpty(status))
                throw new ArgumentNullException("Status can not empty or null !");


            switch (status.ToUpper())
            {
                case "1":
                    return LaserStatus.Running;
                case "1":
                    return LaserStatus.Buyoff;
                case "1":
                    return LaserStatus.Setup;
                case "1":
                    return LaserStatus.Testing;
                case "1":
                    return LaserStatus.NoSchedule;
                case "1":
                    return LaserStatus.Maintenance;
                case "1":
                    return LaserStatus.Breakdown;
                case "1":
                    return LaserStatus.Shutdown;
                default:
                    throw new ArgumentNullException("Status can not empty or null !");
            }
        }
        
        public static PQCStatus ConventnPQC(string status)
        {
            if (string.IsNullOrEmpty(status))
                throw new ArgumentNullException("Status can not empty or null !");


            switch (status.ToUpper())
            {
                case "1":
                    return PQCStatus.Checking;
                case "1":
                    return PQCStatus.Packing;
                case "1":
                    return PQCStatus.NoSchedule;               
                case "1":
                    return PQCStatus.Shutdown;
              
                default:
                    throw new ArgumentNullException("Status can not empty or null !");
            }
        }
    }
}
