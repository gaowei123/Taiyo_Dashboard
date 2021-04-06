using System;
using System.Data;
using System.Collections.Generic;
using Common.Class.Model;
using System.Data.SqlClient;

namespace Common.Class.BLL
{
	/// <summary>
	/// PQCPackDetailTracking
	/// </summary>
	public partial class PQCPackDetailTracking
	{
		private readonly Common.Class.DAL.PQCPackDetailTracking_DAL dal=new Common.Class.DAL.PQCPackDetailTracking_DAL();
		public PQCPackDetailTracking()
		{}


		
        public SqlCommand UpdateCommand(Model.PQCPackDetailTracking_Model model)
        {
            return dal.UpdatePQCMaintenance(model);
        }
        

		public List<Common.Class.Model.PQCPackDetailTracking_Model> GetModelList(string trackingID)
		{
			DataSet ds = dal.GetList(trackingID);
			return DataTableToList(ds.Tables[0]);
		}


		private List<Common.Class.Model.PQCPackDetailTracking_Model> DataTableToList(DataTable dt)
		{
			List<Common.Class.Model.PQCPackDetailTracking_Model> modelList = new List<Common.Class.Model.PQCPackDetailTracking_Model>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Common.Class.Model.PQCPackDetailTracking_Model model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}
        
	}
}

