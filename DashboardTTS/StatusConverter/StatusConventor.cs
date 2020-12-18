using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taiyo.Enum.Production;
using Taiyo.Enum.Organization;
using System.ComponentModel;
using System.Reflection;

namespace Taiyo.Tool
{
    /// <summary>
    /// 原本状态的显示都是按照数client保存到数据库中的数据, 没统一很乱. 
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
                case "RUNNING":
                    return MouldingStatus.Running;
                case "MATERIAL TESTING":
                    return MouldingStatus.MaterialTesting;
                case "MOULD TESTING":
                    return MouldingStatus.MouldTesting;
                case "ADJUSTMENT":
                    return MouldingStatus.Adjustment;
                case "NO_SCHEDULE":
                    return MouldingStatus.NoSchedule;
                case "CHANGE MODEL":
                    return MouldingStatus.ChangeModel;
                case "NO OPERATOR":
                    return MouldingStatus.NoOperator;
                case "LOGIN OUT":
                    return MouldingStatus.LoginOut;
                case "NO MATERIAL":
                    return MouldingStatus.NoMaterial;
                case "LOGIN LATE":
                    return MouldingStatus.LoginLate;
                case "BREAK TIME":
                    return MouldingStatus.BreakTime;
                case "MACHINEBREAK":
                    return MouldingStatus.MachineBreak;
                case "DAMAGEMOULD":
                    return MouldingStatus.DamageMould;
                case "SHUTDOWN":
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
                case "RUN":
                case "POWER ON":
                    return LaserStatus.Running;
                case "ADJUSTMENT":        
                case "BUYOFF":
                    return LaserStatus.Buyoff;
                case "SETUP":
                    return LaserStatus.Setup;
                case "TESTING":
                    return LaserStatus.Testing;
                case "NO SCHEDULE":
                    return LaserStatus.NoSchedule;
                case "MAINTAINENCE":
                    return LaserStatus.Maintenance;
                case "BREAKDOWN":
                    return LaserStatus.Breakdown;
                case "POWER OFF":
                case "SHUTDOWN":
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
                case "CHECKING":
                    return PQCStatus.Checking;
                case "PACKING":
                    return PQCStatus.Packing;
                case "NO SCHEDULE":
                    return PQCStatus.NoSchedule;
                case "SHUTDOWN":
                    return PQCStatus.Shutdown;
              
                default:
                    throw new ArgumentNullException("Status can not empty or null !");
            }
        }
        


        /// <summary>
        /// 根据Description获取枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="description">枚举描述</param>
        /// <returns>枚举</returns>
        public static T GetEnumByDescription<T>(string description)
        {
            System.Reflection.FieldInfo[] fields = typeof(T).GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false); //获取描述属性
                if (objs.Length > 0 && (objs[0] as DescriptionAttribute).Description == description)
                {
                    return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }




    }
}
