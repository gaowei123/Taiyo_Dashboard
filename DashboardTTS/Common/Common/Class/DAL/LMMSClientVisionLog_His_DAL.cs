
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBHelp;
namespace Common.DAL
{
	/// <summary>
	/// 数据访问类:LMMSClientVisionLog_His_DAL
	/// </summary>
	public class LMMSClientVisionLog_His_DAL
	{
		public LMMSClientVisionLog_His_DAL()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Common.Model.LMMSClientVisionLog_His_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSClientVisionLog_His(");
			strSql.Append("id,dateTime,UpdatedTime,TransType,machineID,partNumber,jobNumber,modeStatus,currentStatus,totalGraphic,currentQuantity,totalQuantity,mainResult,vSystemOk,runningOk,inspectOk,passOk,failOk,graXYname1,graXYval1,graXYres1,graXYname2,graXYval2,graXYres2,graXYname3,graXYval3,graXYres3,graXYname4,graXYval4,graXYres4,graXYname5,graXYval5,graXYres5,graXYname6,graXYval6,graXYres6,graXYname7,graXYval7,graXYres7,graXYname8,graXYval8,graXYres8,graXYname9,graXYval9,graXYres9,graXYname10,graXYval10,graXYres10,graXYname11,graXYval11,graXYres11,graXYname12,graXYval12,graXYres12,graXYname13,graXYval13,graXYres13,graXYname14,graXYval14,graXYres14,graXYname15,graXYval15,graXYres15,graXYname16,graXYval16,graXYres16,graXYname17,graXYval17,graXYres17,graXYname18,graXYval18,graXYres18,graXYname19,graXYval19,graXYres19,graXYname20,graXYval20,graXYres20,graXYname21,graXYval21,graXYres21,graXYname22,graXYval22,graXYres22,graXYname23,graXYval23,graXYres23,graXYname24,graXYval24,graXYres24,graXYname25,graXYval25,graXYres25,graXYname26,graXYval26,graXYres26,graXYname27,graXYval27,graXYres27,graXYname28,graXYval28,graXYres28,graXYname29,graXYval29,graXYres29,graXYname30,graXYval30,graXYres30,overallResult,totalJig)");
			strSql.Append(" values (");
			strSql.Append("@id,@dateTime,@UpdatedTime,@TransType,@machineID,@partNumber,@jobNumber,@modeStatus,@currentStatus,@totalGraphic,@currentQuantity,@totalQuantity,@mainResult,@vSystemOk,@runningOk,@inspectOk,@passOk,@failOk,@graXYname1,@graXYval1,@graXYres1,@graXYname2,@graXYval2,@graXYres2,@graXYname3,@graXYval3,@graXYres3,@graXYname4,@graXYval4,@graXYres4,@graXYname5,@graXYval5,@graXYres5,@graXYname6,@graXYval6,@graXYres6,@graXYname7,@graXYval7,@graXYres7,@graXYname8,@graXYval8,@graXYres8,@graXYname9,@graXYval9,@graXYres9,@graXYname10,@graXYval10,@graXYres10,@graXYname11,@graXYval11,@graXYres11,@graXYname12,@graXYval12,@graXYres12,@graXYname13,@graXYval13,@graXYres13,@graXYname14,@graXYval14,@graXYres14,@graXYname15,@graXYval15,@graXYres15,@graXYname16,@graXYval16,@graXYres16,@graXYname17,@graXYval17,@graXYres17,@graXYname18,@graXYval18,@graXYres18,@graXYname19,@graXYval19,@graXYres19,@graXYname20,@graXYval20,@graXYres20,@graXYname21,@graXYval21,@graXYres21,@graXYname22,@graXYval22,@graXYres22,@graXYname23,@graXYval23,@graXYres23,@graXYname24,@graXYval24,@graXYres24,@graXYname25,@graXYval25,@graXYres25,@graXYname26,@graXYval26,@graXYres26,@graXYname27,@graXYval27,@graXYres27,@graXYname28,@graXYval28,@graXYres28,@graXYname29,@graXYval29,@graXYres29,@graXYname30,@graXYval30,@graXYres30,@overallResult,@totalJig)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@UpdatedTime", SqlDbType.DateTime2,8),
					new SqlParameter("@TransType", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@jobNumber", SqlDbType.VarChar,50),
					new SqlParameter("@modeStatus", SqlDbType.VarChar,50),
					new SqlParameter("@currentStatus", SqlDbType.VarChar,50),
					new SqlParameter("@totalGraphic", SqlDbType.Int,4),
					new SqlParameter("@currentQuantity", SqlDbType.Int,4),
					new SqlParameter("@totalQuantity", SqlDbType.Int,4),
					new SqlParameter("@mainResult", SqlDbType.VarChar,50),
					new SqlParameter("@vSystemOk", SqlDbType.VarChar,50),
					new SqlParameter("@runningOk", SqlDbType.VarChar,50),
					new SqlParameter("@inspectOk", SqlDbType.VarChar,50),
					new SqlParameter("@passOk", SqlDbType.VarChar,50),
					new SqlParameter("@failOk", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname30", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval30", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres30", SqlDbType.VarChar,50),
					new SqlParameter("@overallResult", SqlDbType.VarChar,50),
					new SqlParameter("@totalJig", SqlDbType.Int,4)};
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSClientVisionLog_His_DAL" , "Function:		public void Add(Common.Model.LMMSClientVisionLog_His_Model model)"  + "TableName:LMMSClientVisionLog_His" , ";id = "+ (model.id == null ? "" : model.id.ToString() ) + ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString() ) + ";UpdatedTime = "+ (model.UpdatedTime == null ? "" : model.UpdatedTime.ToString() ) + ";TransType = "+ (model.TransType == null ? "" : model.TransType.ToString() ) + ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString() ) + ";partNumber = "+ (model.partNumber == null ? "" : model.partNumber.ToString() ) + ";jobNumber = "+ (model.jobNumber == null ? "" : model.jobNumber.ToString() ) + ";modeStatus = "+ (model.modeStatus == null ? "" : model.modeStatus.ToString() ) + ";currentStatus = "+ (model.currentStatus == null ? "" : model.currentStatus.ToString() ) + ";totalGraphic = "+ (model.totalGraphic == null ? "" : model.totalGraphic.ToString() ) + ";currentQuantity = "+ (model.currentQuantity == null ? "" : model.currentQuantity.ToString() ) + ";totalQuantity = "+ (model.totalQuantity == null ? "" : model.totalQuantity.ToString() ) + ";mainResult = "+ (model.mainResult == null ? "" : model.mainResult.ToString() ) + ";vSystemOk = "+ (model.vSystemOk == null ? "" : model.vSystemOk.ToString() ) + ";runningOk = "+ (model.runningOk == null ? "" : model.runningOk.ToString() ) + ";inspectOk = "+ (model.inspectOk == null ? "" : model.inspectOk.ToString() ) + ";passOk = "+ (model.passOk == null ? "" : model.passOk.ToString() ) + ";failOk = "+ (model.failOk == null ? "" : model.failOk.ToString() ) + ";graXYname1 = "+ (model.graXYname1 == null ? "" : model.graXYname1.ToString() ) + ";graXYval1 = "+ (model.graXYval1 == null ? "" : model.graXYval1.ToString() ) + ";graXYres1 = "+ (model.graXYres1 == null ? "" : model.graXYres1.ToString() ) + ";graXYname2 = "+ (model.graXYname2 == null ? "" : model.graXYname2.ToString() ) + ";graXYval2 = "+ (model.graXYval2 == null ? "" : model.graXYval2.ToString() ) + ";graXYres2 = "+ (model.graXYres2 == null ? "" : model.graXYres2.ToString() ) + ";graXYname3 = "+ (model.graXYname3 == null ? "" : model.graXYname3.ToString() ) + ";graXYval3 = "+ (model.graXYval3 == null ? "" : model.graXYval3.ToString() ) + ";graXYres3 = "+ (model.graXYres3 == null ? "" : model.graXYres3.ToString() ) + ";graXYname4 = "+ (model.graXYname4 == null ? "" : model.graXYname4.ToString() ) + ";graXYval4 = "+ (model.graXYval4 == null ? "" : model.graXYval4.ToString() ) + ";graXYres4 = "+ (model.graXYres4 == null ? "" : model.graXYres4.ToString() ) + ";graXYname5 = "+ (model.graXYname5 == null ? "" : model.graXYname5.ToString() ) + ";graXYval5 = "+ (model.graXYval5 == null ? "" : model.graXYval5.ToString() ) + ";graXYres5 = "+ (model.graXYres5 == null ? "" : model.graXYres5.ToString() ) + ";graXYname6 = "+ (model.graXYname6 == null ? "" : model.graXYname6.ToString() ) + ";graXYval6 = "+ (model.graXYval6 == null ? "" : model.graXYval6.ToString() ) + ";graXYres6 = "+ (model.graXYres6 == null ? "" : model.graXYres6.ToString() ) + ";graXYname7 = "+ (model.graXYname7 == null ? "" : model.graXYname7.ToString() ) + ";graXYval7 = "+ (model.graXYval7 == null ? "" : model.graXYval7.ToString() ) + ";graXYres7 = "+ (model.graXYres7 == null ? "" : model.graXYres7.ToString() ) + ";graXYname8 = "+ (model.graXYname8 == null ? "" : model.graXYname8.ToString() ) + ";graXYval8 = "+ (model.graXYval8 == null ? "" : model.graXYval8.ToString() ) + ";graXYres8 = "+ (model.graXYres8 == null ? "" : model.graXYres8.ToString() ) + ";graXYname9 = "+ (model.graXYname9 == null ? "" : model.graXYname9.ToString() ) + ";graXYval9 = "+ (model.graXYval9 == null ? "" : model.graXYval9.ToString() ) + ";graXYres9 = "+ (model.graXYres9 == null ? "" : model.graXYres9.ToString() ) + ";graXYname10 = "+ (model.graXYname10 == null ? "" : model.graXYname10.ToString() ) + ";graXYval10 = "+ (model.graXYval10 == null ? "" : model.graXYval10.ToString() ) + ";graXYres10 = "+ (model.graXYres10 == null ? "" : model.graXYres10.ToString() ) + ";graXYname11 = "+ (model.graXYname11 == null ? "" : model.graXYname11.ToString() ) + ";graXYval11 = "+ (model.graXYval11 == null ? "" : model.graXYval11.ToString() ) + ";graXYres11 = "+ (model.graXYres11 == null ? "" : model.graXYres11.ToString() ) + ";graXYname12 = "+ (model.graXYname12 == null ? "" : model.graXYname12.ToString() ) + ";graXYval12 = "+ (model.graXYval12 == null ? "" : model.graXYval12.ToString() ) + ";graXYres12 = "+ (model.graXYres12 == null ? "" : model.graXYres12.ToString() ) + ";graXYname13 = "+ (model.graXYname13 == null ? "" : model.graXYname13.ToString() ) + ";graXYval13 = "+ (model.graXYval13 == null ? "" : model.graXYval13.ToString() ) + ";graXYres13 = "+ (model.graXYres13 == null ? "" : model.graXYres13.ToString() ) + ";graXYname14 = "+ (model.graXYname14 == null ? "" : model.graXYname14.ToString() ) + ";graXYval14 = "+ (model.graXYval14 == null ? "" : model.graXYval14.ToString() ) + ";graXYres14 = "+ (model.graXYres14 == null ? "" : model.graXYres14.ToString() ) + ";graXYname15 = "+ (model.graXYname15 == null ? "" : model.graXYname15.ToString() ) + ";graXYval15 = "+ (model.graXYval15 == null ? "" : model.graXYval15.ToString() ) + ";graXYres15 = "+ (model.graXYres15 == null ? "" : model.graXYres15.ToString() ) + ";graXYname16 = "+ (model.graXYname16 == null ? "" : model.graXYname16.ToString() ) + ";graXYval16 = "+ (model.graXYval16 == null ? "" : model.graXYval16.ToString() ) + ";graXYres16 = "+ (model.graXYres16 == null ? "" : model.graXYres16.ToString() ) + ";graXYname17 = "+ (model.graXYname17 == null ? "" : model.graXYname17.ToString() ) + ";graXYval17 = "+ (model.graXYval17 == null ? "" : model.graXYval17.ToString() ) + ";graXYres17 = "+ (model.graXYres17 == null ? "" : model.graXYres17.ToString() ) + ";graXYname18 = "+ (model.graXYname18 == null ? "" : model.graXYname18.ToString() ) + ";graXYval18 = "+ (model.graXYval18 == null ? "" : model.graXYval18.ToString() ) + ";graXYres18 = "+ (model.graXYres18 == null ? "" : model.graXYres18.ToString() ) + ";graXYname19 = "+ (model.graXYname19 == null ? "" : model.graXYname19.ToString() ) + ";graXYval19 = "+ (model.graXYval19 == null ? "" : model.graXYval19.ToString() ) + ";graXYres19 = "+ (model.graXYres19 == null ? "" : model.graXYres19.ToString() ) + ";graXYname20 = "+ (model.graXYname20 == null ? "" : model.graXYname20.ToString() ) + ";graXYval20 = "+ (model.graXYval20 == null ? "" : model.graXYval20.ToString() ) + ";graXYres20 = "+ (model.graXYres20 == null ? "" : model.graXYres20.ToString() ) + ";graXYname21 = "+ (model.graXYname21 == null ? "" : model.graXYname21.ToString() ) + ";graXYval21 = "+ (model.graXYval21 == null ? "" : model.graXYval21.ToString() ) + ";graXYres21 = "+ (model.graXYres21 == null ? "" : model.graXYres21.ToString() ) + ";graXYname22 = "+ (model.graXYname22 == null ? "" : model.graXYname22.ToString() ) + ";graXYval22 = "+ (model.graXYval22 == null ? "" : model.graXYval22.ToString() ) + ";graXYres22 = "+ (model.graXYres22 == null ? "" : model.graXYres22.ToString() ) + ";graXYname23 = "+ (model.graXYname23 == null ? "" : model.graXYname23.ToString() ) + ";graXYval23 = "+ (model.graXYval23 == null ? "" : model.graXYval23.ToString() ) + ";graXYres23 = "+ (model.graXYres23 == null ? "" : model.graXYres23.ToString() ) + ";graXYname24 = "+ (model.graXYname24 == null ? "" : model.graXYname24.ToString() ) + ";graXYval24 = "+ (model.graXYval24 == null ? "" : model.graXYval24.ToString() ) + ";graXYres24 = "+ (model.graXYres24 == null ? "" : model.graXYres24.ToString() ) + ";graXYname25 = "+ (model.graXYname25 == null ? "" : model.graXYname25.ToString() ) + ";graXYval25 = "+ (model.graXYval25 == null ? "" : model.graXYval25.ToString() ) + ";graXYres25 = "+ (model.graXYres25 == null ? "" : model.graXYres25.ToString() ) + ";graXYname26 = "+ (model.graXYname26 == null ? "" : model.graXYname26.ToString() ) + ";graXYval26 = "+ (model.graXYval26 == null ? "" : model.graXYval26.ToString() ) + ";graXYres26 = "+ (model.graXYres26 == null ? "" : model.graXYres26.ToString() ) + ";graXYname27 = "+ (model.graXYname27 == null ? "" : model.graXYname27.ToString() ) + ";graXYval27 = "+ (model.graXYval27 == null ? "" : model.graXYval27.ToString() ) + ";graXYres27 = "+ (model.graXYres27 == null ? "" : model.graXYres27.ToString() ) + ";graXYname28 = "+ (model.graXYname28 == null ? "" : model.graXYname28.ToString() ) + ";graXYval28 = "+ (model.graXYval28 == null ? "" : model.graXYval28.ToString() ) + ";graXYres28 = "+ (model.graXYres28 == null ? "" : model.graXYres28.ToString() ) + ";graXYname29 = "+ (model.graXYname29 == null ? "" : model.graXYname29.ToString() ) + ";graXYval29 = "+ (model.graXYval29 == null ? "" : model.graXYval29.ToString() ) + ";graXYres29 = "+ (model.graXYres29 == null ? "" : model.graXYres29.ToString() ) + ";graXYname30 = "+ (model.graXYname30 == null ? "" : model.graXYname30.ToString() ) + ";graXYval30 = "+ (model.graXYval30 == null ? "" : model.graXYval30.ToString() ) + ";graXYres30 = "+ (model.graXYres30 == null ? "" : model.graXYres30.ToString() ) + ";overallResult = "+ (model.overallResult == null ? "" : model.overallResult.ToString() ) + ";totalJig = "+ (model.totalJig == null ? "" : model.totalJig.ToString() ) + "");
			parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id ;
			parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[2].Value = model.UpdatedTime == null ? (object)DBNull.Value : model.UpdatedTime ;
			parameters[3].Value = model.TransType == null ? (object)DBNull.Value : model.TransType ;
			parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[5].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber ;
			parameters[6].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber ;
			parameters[7].Value = model.modeStatus == null ? (object)DBNull.Value : model.modeStatus ;
			parameters[8].Value = model.currentStatus == null ? (object)DBNull.Value : model.currentStatus ;
			parameters[9].Value = model.totalGraphic == null ? (object)DBNull.Value : model.totalGraphic ;
			parameters[10].Value = model.currentQuantity == null ? (object)DBNull.Value : model.currentQuantity ;
			parameters[11].Value = model.totalQuantity == null ? (object)DBNull.Value : model.totalQuantity ;
			parameters[12].Value = model.mainResult == null ? (object)DBNull.Value : model.mainResult ;
			parameters[13].Value = model.vSystemOk == null ? (object)DBNull.Value : model.vSystemOk ;
			parameters[14].Value = model.runningOk == null ? (object)DBNull.Value : model.runningOk ;
			parameters[15].Value = model.inspectOk == null ? (object)DBNull.Value : model.inspectOk ;
			parameters[16].Value = model.passOk == null ? (object)DBNull.Value : model.passOk ;
			parameters[17].Value = model.failOk == null ? (object)DBNull.Value : model.failOk ;
			parameters[18].Value = model.graXYname1 == null ? (object)DBNull.Value : model.graXYname1 ;
			parameters[19].Value = model.graXYval1 == null ? (object)DBNull.Value : model.graXYval1 ;
			parameters[20].Value = model.graXYres1 == null ? (object)DBNull.Value : model.graXYres1 ;
			parameters[21].Value = model.graXYname2 == null ? (object)DBNull.Value : model.graXYname2 ;
			parameters[22].Value = model.graXYval2 == null ? (object)DBNull.Value : model.graXYval2 ;
			parameters[23].Value = model.graXYres2 == null ? (object)DBNull.Value : model.graXYres2 ;
			parameters[24].Value = model.graXYname3 == null ? (object)DBNull.Value : model.graXYname3 ;
			parameters[25].Value = model.graXYval3 == null ? (object)DBNull.Value : model.graXYval3 ;
			parameters[26].Value = model.graXYres3 == null ? (object)DBNull.Value : model.graXYres3 ;
			parameters[27].Value = model.graXYname4 == null ? (object)DBNull.Value : model.graXYname4 ;
			parameters[28].Value = model.graXYval4 == null ? (object)DBNull.Value : model.graXYval4 ;
			parameters[29].Value = model.graXYres4 == null ? (object)DBNull.Value : model.graXYres4 ;
			parameters[30].Value = model.graXYname5 == null ? (object)DBNull.Value : model.graXYname5 ;
			parameters[31].Value = model.graXYval5 == null ? (object)DBNull.Value : model.graXYval5 ;
			parameters[32].Value = model.graXYres5 == null ? (object)DBNull.Value : model.graXYres5 ;
			parameters[33].Value = model.graXYname6 == null ? (object)DBNull.Value : model.graXYname6 ;
			parameters[34].Value = model.graXYval6 == null ? (object)DBNull.Value : model.graXYval6 ;
			parameters[35].Value = model.graXYres6 == null ? (object)DBNull.Value : model.graXYres6 ;
			parameters[36].Value = model.graXYname7 == null ? (object)DBNull.Value : model.graXYname7 ;
			parameters[37].Value = model.graXYval7 == null ? (object)DBNull.Value : model.graXYval7 ;
			parameters[38].Value = model.graXYres7 == null ? (object)DBNull.Value : model.graXYres7 ;
			parameters[39].Value = model.graXYname8 == null ? (object)DBNull.Value : model.graXYname8 ;
			parameters[40].Value = model.graXYval8 == null ? (object)DBNull.Value : model.graXYval8 ;
			parameters[41].Value = model.graXYres8 == null ? (object)DBNull.Value : model.graXYres8 ;
			parameters[42].Value = model.graXYname9 == null ? (object)DBNull.Value : model.graXYname9 ;
			parameters[43].Value = model.graXYval9 == null ? (object)DBNull.Value : model.graXYval9 ;
			parameters[44].Value = model.graXYres9 == null ? (object)DBNull.Value : model.graXYres9 ;
			parameters[45].Value = model.graXYname10 == null ? (object)DBNull.Value : model.graXYname10 ;
			parameters[46].Value = model.graXYval10 == null ? (object)DBNull.Value : model.graXYval10 ;
			parameters[47].Value = model.graXYres10 == null ? (object)DBNull.Value : model.graXYres10 ;
			parameters[48].Value = model.graXYname11 == null ? (object)DBNull.Value : model.graXYname11 ;
			parameters[49].Value = model.graXYval11 == null ? (object)DBNull.Value : model.graXYval11 ;
			parameters[50].Value = model.graXYres11 == null ? (object)DBNull.Value : model.graXYres11 ;
			parameters[51].Value = model.graXYname12 == null ? (object)DBNull.Value : model.graXYname12 ;
			parameters[52].Value = model.graXYval12 == null ? (object)DBNull.Value : model.graXYval12 ;
			parameters[53].Value = model.graXYres12 == null ? (object)DBNull.Value : model.graXYres12 ;
			parameters[54].Value = model.graXYname13 == null ? (object)DBNull.Value : model.graXYname13 ;
			parameters[55].Value = model.graXYval13 == null ? (object)DBNull.Value : model.graXYval13 ;
			parameters[56].Value = model.graXYres13 == null ? (object)DBNull.Value : model.graXYres13 ;
			parameters[57].Value = model.graXYname14 == null ? (object)DBNull.Value : model.graXYname14 ;
			parameters[58].Value = model.graXYval14 == null ? (object)DBNull.Value : model.graXYval14 ;
			parameters[59].Value = model.graXYres14 == null ? (object)DBNull.Value : model.graXYres14 ;
			parameters[60].Value = model.graXYname15 == null ? (object)DBNull.Value : model.graXYname15 ;
			parameters[61].Value = model.graXYval15 == null ? (object)DBNull.Value : model.graXYval15 ;
			parameters[62].Value = model.graXYres15 == null ? (object)DBNull.Value : model.graXYres15 ;
			parameters[63].Value = model.graXYname16 == null ? (object)DBNull.Value : model.graXYname16 ;
			parameters[64].Value = model.graXYval16 == null ? (object)DBNull.Value : model.graXYval16 ;
			parameters[65].Value = model.graXYres16 == null ? (object)DBNull.Value : model.graXYres16 ;
			parameters[66].Value = model.graXYname17 == null ? (object)DBNull.Value : model.graXYname17 ;
			parameters[67].Value = model.graXYval17 == null ? (object)DBNull.Value : model.graXYval17 ;
			parameters[68].Value = model.graXYres17 == null ? (object)DBNull.Value : model.graXYres17 ;
			parameters[69].Value = model.graXYname18 == null ? (object)DBNull.Value : model.graXYname18 ;
			parameters[70].Value = model.graXYval18 == null ? (object)DBNull.Value : model.graXYval18 ;
			parameters[71].Value = model.graXYres18 == null ? (object)DBNull.Value : model.graXYres18 ;
			parameters[72].Value = model.graXYname19 == null ? (object)DBNull.Value : model.graXYname19 ;
			parameters[73].Value = model.graXYval19 == null ? (object)DBNull.Value : model.graXYval19 ;
			parameters[74].Value = model.graXYres19 == null ? (object)DBNull.Value : model.graXYres19 ;
			parameters[75].Value = model.graXYname20 == null ? (object)DBNull.Value : model.graXYname20 ;
			parameters[76].Value = model.graXYval20 == null ? (object)DBNull.Value : model.graXYval20 ;
			parameters[77].Value = model.graXYres20 == null ? (object)DBNull.Value : model.graXYres20 ;
			parameters[78].Value = model.graXYname21 == null ? (object)DBNull.Value : model.graXYname21 ;
			parameters[79].Value = model.graXYval21 == null ? (object)DBNull.Value : model.graXYval21 ;
			parameters[80].Value = model.graXYres21 == null ? (object)DBNull.Value : model.graXYres21 ;
			parameters[81].Value = model.graXYname22 == null ? (object)DBNull.Value : model.graXYname22 ;
			parameters[82].Value = model.graXYval22 == null ? (object)DBNull.Value : model.graXYval22 ;
			parameters[83].Value = model.graXYres22 == null ? (object)DBNull.Value : model.graXYres22 ;
			parameters[84].Value = model.graXYname23 == null ? (object)DBNull.Value : model.graXYname23 ;
			parameters[85].Value = model.graXYval23 == null ? (object)DBNull.Value : model.graXYval23 ;
			parameters[86].Value = model.graXYres23 == null ? (object)DBNull.Value : model.graXYres23 ;
			parameters[87].Value = model.graXYname24 == null ? (object)DBNull.Value : model.graXYname24 ;
			parameters[88].Value = model.graXYval24 == null ? (object)DBNull.Value : model.graXYval24 ;
			parameters[89].Value = model.graXYres24 == null ? (object)DBNull.Value : model.graXYres24 ;
			parameters[90].Value = model.graXYname25 == null ? (object)DBNull.Value : model.graXYname25 ;
			parameters[91].Value = model.graXYval25 == null ? (object)DBNull.Value : model.graXYval25 ;
			parameters[92].Value = model.graXYres25 == null ? (object)DBNull.Value : model.graXYres25 ;
			parameters[93].Value = model.graXYname26 == null ? (object)DBNull.Value : model.graXYname26 ;
			parameters[94].Value = model.graXYval26 == null ? (object)DBNull.Value : model.graXYval26 ;
			parameters[95].Value = model.graXYres26 == null ? (object)DBNull.Value : model.graXYres26 ;
			parameters[96].Value = model.graXYname27 == null ? (object)DBNull.Value : model.graXYname27 ;
			parameters[97].Value = model.graXYval27 == null ? (object)DBNull.Value : model.graXYval27 ;
			parameters[98].Value = model.graXYres27 == null ? (object)DBNull.Value : model.graXYres27 ;
			parameters[99].Value = model.graXYname28 == null ? (object)DBNull.Value : model.graXYname28 ;
			parameters[100].Value = model.graXYval28 == null ? (object)DBNull.Value : model.graXYval28 ;
			parameters[101].Value = model.graXYres28 == null ? (object)DBNull.Value : model.graXYres28 ;
			parameters[102].Value = model.graXYname29 == null ? (object)DBNull.Value : model.graXYname29 ;
			parameters[103].Value = model.graXYval29 == null ? (object)DBNull.Value : model.graXYval29 ;
			parameters[104].Value = model.graXYres29 == null ? (object)DBNull.Value : model.graXYres29 ;
			parameters[105].Value = model.graXYname30 == null ? (object)DBNull.Value : model.graXYname30 ;
			parameters[106].Value = model.graXYval30 == null ? (object)DBNull.Value : model.graXYval30 ;
			parameters[107].Value = model.graXYres30 == null ? (object)DBNull.Value : model.graXYres30 ;
			parameters[108].Value = model.overallResult == null ? (object)DBNull.Value : model.overallResult ;
			parameters[109].Value = model.totalJig == null ? (object)DBNull.Value : model.totalJig ;

			DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public SqlCommand AddCommand(Common.Model.LMMSClientVisionLog_His_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LMMSClientVisionLog_His(");
			strSql.Append("id,dateTime,UpdatedTime,TransType,machineID,partNumber,jobNumber,modeStatus,currentStatus,totalGraphic,currentQuantity,totalQuantity,mainResult,vSystemOk,runningOk,inspectOk,passOk,failOk,graXYname1,graXYval1,graXYres1,graXYname2,graXYval2,graXYres2,graXYname3,graXYval3,graXYres3,graXYname4,graXYval4,graXYres4,graXYname5,graXYval5,graXYres5,graXYname6,graXYval6,graXYres6,graXYname7,graXYval7,graXYres7,graXYname8,graXYval8,graXYres8,graXYname9,graXYval9,graXYres9,graXYname10,graXYval10,graXYres10,graXYname11,graXYval11,graXYres11,graXYname12,graXYval12,graXYres12,graXYname13,graXYval13,graXYres13,graXYname14,graXYval14,graXYres14,graXYname15,graXYval15,graXYres15,graXYname16,graXYval16,graXYres16,graXYname17,graXYval17,graXYres17,graXYname18,graXYval18,graXYres18,graXYname19,graXYval19,graXYres19,graXYname20,graXYval20,graXYres20,graXYname21,graXYval21,graXYres21,graXYname22,graXYval22,graXYres22,graXYname23,graXYval23,graXYres23,graXYname24,graXYval24,graXYres24,graXYname25,graXYval25,graXYres25,graXYname26,graXYval26,graXYres26,graXYname27,graXYval27,graXYres27,graXYname28,graXYval28,graXYres28,graXYname29,graXYval29,graXYres29,graXYname30,graXYval30,graXYres30,overallResult,totalJig)");
			strSql.Append(" values (");
			strSql.Append("@id,@dateTime,@UpdatedTime,@TransType,@machineID,@partNumber,@jobNumber,@modeStatus,@currentStatus,@totalGraphic,@currentQuantity,@totalQuantity,@mainResult,@vSystemOk,@runningOk,@inspectOk,@passOk,@failOk,@graXYname1,@graXYval1,@graXYres1,@graXYname2,@graXYval2,@graXYres2,@graXYname3,@graXYval3,@graXYres3,@graXYname4,@graXYval4,@graXYres4,@graXYname5,@graXYval5,@graXYres5,@graXYname6,@graXYval6,@graXYres6,@graXYname7,@graXYval7,@graXYres7,@graXYname8,@graXYval8,@graXYres8,@graXYname9,@graXYval9,@graXYres9,@graXYname10,@graXYval10,@graXYres10,@graXYname11,@graXYval11,@graXYres11,@graXYname12,@graXYval12,@graXYres12,@graXYname13,@graXYval13,@graXYres13,@graXYname14,@graXYval14,@graXYres14,@graXYname15,@graXYval15,@graXYres15,@graXYname16,@graXYval16,@graXYres16,@graXYname17,@graXYval17,@graXYres17,@graXYname18,@graXYval18,@graXYres18,@graXYname19,@graXYval19,@graXYres19,@graXYname20,@graXYval20,@graXYres20,@graXYname21,@graXYval21,@graXYres21,@graXYname22,@graXYval22,@graXYres22,@graXYname23,@graXYval23,@graXYres23,@graXYname24,@graXYval24,@graXYres24,@graXYname25,@graXYval25,@graXYres25,@graXYname26,@graXYval26,@graXYres26,@graXYname27,@graXYval27,@graXYres27,@graXYname28,@graXYval28,@graXYres28,@graXYname29,@graXYval29,@graXYres29,@graXYname30,@graXYval30,@graXYres30,@overallResult,@totalJig)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@UpdatedTime", SqlDbType.DateTime2,8),
					new SqlParameter("@TransType", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@jobNumber", SqlDbType.VarChar,50),
					new SqlParameter("@modeStatus", SqlDbType.VarChar,50),
					new SqlParameter("@currentStatus", SqlDbType.VarChar,50),
					new SqlParameter("@totalGraphic", SqlDbType.Int,4),
					new SqlParameter("@currentQuantity", SqlDbType.Int,4),
					new SqlParameter("@totalQuantity", SqlDbType.Int,4),
					new SqlParameter("@mainResult", SqlDbType.VarChar,50),
					new SqlParameter("@vSystemOk", SqlDbType.VarChar,50),
					new SqlParameter("@runningOk", SqlDbType.VarChar,50),
					new SqlParameter("@inspectOk", SqlDbType.VarChar,50),
					new SqlParameter("@passOk", SqlDbType.VarChar,50),
					new SqlParameter("@failOk", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname30", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval30", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres30", SqlDbType.VarChar,50),
					new SqlParameter("@overallResult", SqlDbType.VarChar,50),
					new SqlParameter("@totalJig", SqlDbType.Int,4)};
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSClientVisionLog_His_DAL" , "Function:		public SqlCommand AddCommand(Common.Model.LMMSClientVisionLog_His_Model model)"  + "TableName:LMMSClientVisionLog_His" , ";id = "+ (model.id == null ? "" : model.id.ToString()) + ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString()) + ";UpdatedTime = "+ (model.UpdatedTime == null ? "" : model.UpdatedTime.ToString()) + ";TransType = "+ (model.TransType == null ? "" : model.TransType.ToString()) + ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString()) + ";partNumber = "+ (model.partNumber == null ? "" : model.partNumber.ToString()) + ";jobNumber = "+ (model.jobNumber == null ? "" : model.jobNumber.ToString()) + ";modeStatus = "+ (model.modeStatus == null ? "" : model.modeStatus.ToString()) + ";currentStatus = "+ (model.currentStatus == null ? "" : model.currentStatus.ToString()) + ";totalGraphic = "+ (model.totalGraphic == null ? "" : model.totalGraphic.ToString()) + ";currentQuantity = "+ (model.currentQuantity == null ? "" : model.currentQuantity.ToString()) + ";totalQuantity = "+ (model.totalQuantity == null ? "" : model.totalQuantity.ToString()) + ";mainResult = "+ (model.mainResult == null ? "" : model.mainResult.ToString()) + ";vSystemOk = "+ (model.vSystemOk == null ? "" : model.vSystemOk.ToString()) + ";runningOk = "+ (model.runningOk == null ? "" : model.runningOk.ToString()) + ";inspectOk = "+ (model.inspectOk == null ? "" : model.inspectOk.ToString()) + ";passOk = "+ (model.passOk == null ? "" : model.passOk.ToString()) + ";failOk = "+ (model.failOk == null ? "" : model.failOk.ToString()) + ";graXYname1 = "+ (model.graXYname1 == null ? "" : model.graXYname1.ToString()) + ";graXYval1 = "+ (model.graXYval1 == null ? "" : model.graXYval1.ToString()) + ";graXYres1 = "+ (model.graXYres1 == null ? "" : model.graXYres1.ToString()) + ";graXYname2 = "+ (model.graXYname2 == null ? "" : model.graXYname2.ToString()) + ";graXYval2 = "+ (model.graXYval2 == null ? "" : model.graXYval2.ToString()) + ";graXYres2 = "+ (model.graXYres2 == null ? "" : model.graXYres2.ToString()) + ";graXYname3 = "+ (model.graXYname3 == null ? "" : model.graXYname3.ToString()) + ";graXYval3 = "+ (model.graXYval3 == null ? "" : model.graXYval3.ToString()) + ";graXYres3 = "+ (model.graXYres3 == null ? "" : model.graXYres3.ToString()) + ";graXYname4 = "+ (model.graXYname4 == null ? "" : model.graXYname4.ToString()) + ";graXYval4 = "+ (model.graXYval4 == null ? "" : model.graXYval4.ToString()) + ";graXYres4 = "+ (model.graXYres4 == null ? "" : model.graXYres4.ToString()) + ";graXYname5 = "+ (model.graXYname5 == null ? "" : model.graXYname5.ToString()) + ";graXYval5 = "+ (model.graXYval5 == null ? "" : model.graXYval5.ToString()) + ";graXYres5 = "+ (model.graXYres5 == null ? "" : model.graXYres5.ToString()) + ";graXYname6 = "+ (model.graXYname6 == null ? "" : model.graXYname6.ToString()) + ";graXYval6 = "+ (model.graXYval6 == null ? "" : model.graXYval6.ToString()) + ";graXYres6 = "+ (model.graXYres6 == null ? "" : model.graXYres6.ToString()) + ";graXYname7 = "+ (model.graXYname7 == null ? "" : model.graXYname7.ToString()) + ";graXYval7 = "+ (model.graXYval7 == null ? "" : model.graXYval7.ToString()) + ";graXYres7 = "+ (model.graXYres7 == null ? "" : model.graXYres7.ToString()) + ";graXYname8 = "+ (model.graXYname8 == null ? "" : model.graXYname8.ToString()) + ";graXYval8 = "+ (model.graXYval8 == null ? "" : model.graXYval8.ToString()) + ";graXYres8 = "+ (model.graXYres8 == null ? "" : model.graXYres8.ToString()) + ";graXYname9 = "+ (model.graXYname9 == null ? "" : model.graXYname9.ToString()) + ";graXYval9 = "+ (model.graXYval9 == null ? "" : model.graXYval9.ToString()) + ";graXYres9 = "+ (model.graXYres9 == null ? "" : model.graXYres9.ToString()) + ";graXYname10 = "+ (model.graXYname10 == null ? "" : model.graXYname10.ToString()) + ";graXYval10 = "+ (model.graXYval10 == null ? "" : model.graXYval10.ToString()) + ";graXYres10 = "+ (model.graXYres10 == null ? "" : model.graXYres10.ToString()) + ";graXYname11 = "+ (model.graXYname11 == null ? "" : model.graXYname11.ToString()) + ";graXYval11 = "+ (model.graXYval11 == null ? "" : model.graXYval11.ToString()) + ";graXYres11 = "+ (model.graXYres11 == null ? "" : model.graXYres11.ToString()) + ";graXYname12 = "+ (model.graXYname12 == null ? "" : model.graXYname12.ToString()) + ";graXYval12 = "+ (model.graXYval12 == null ? "" : model.graXYval12.ToString()) + ";graXYres12 = "+ (model.graXYres12 == null ? "" : model.graXYres12.ToString()) + ";graXYname13 = "+ (model.graXYname13 == null ? "" : model.graXYname13.ToString()) + ";graXYval13 = "+ (model.graXYval13 == null ? "" : model.graXYval13.ToString()) + ";graXYres13 = "+ (model.graXYres13 == null ? "" : model.graXYres13.ToString()) + ";graXYname14 = "+ (model.graXYname14 == null ? "" : model.graXYname14.ToString()) + ";graXYval14 = "+ (model.graXYval14 == null ? "" : model.graXYval14.ToString()) + ";graXYres14 = "+ (model.graXYres14 == null ? "" : model.graXYres14.ToString()) + ";graXYname15 = "+ (model.graXYname15 == null ? "" : model.graXYname15.ToString()) + ";graXYval15 = "+ (model.graXYval15 == null ? "" : model.graXYval15.ToString()) + ";graXYres15 = "+ (model.graXYres15 == null ? "" : model.graXYres15.ToString()) + ";graXYname16 = "+ (model.graXYname16 == null ? "" : model.graXYname16.ToString()) + ";graXYval16 = "+ (model.graXYval16 == null ? "" : model.graXYval16.ToString()) + ";graXYres16 = "+ (model.graXYres16 == null ? "" : model.graXYres16.ToString()) + ";graXYname17 = "+ (model.graXYname17 == null ? "" : model.graXYname17.ToString()) + ";graXYval17 = "+ (model.graXYval17 == null ? "" : model.graXYval17.ToString()) + ";graXYres17 = "+ (model.graXYres17 == null ? "" : model.graXYres17.ToString()) + ";graXYname18 = "+ (model.graXYname18 == null ? "" : model.graXYname18.ToString()) + ";graXYval18 = "+ (model.graXYval18 == null ? "" : model.graXYval18.ToString()) + ";graXYres18 = "+ (model.graXYres18 == null ? "" : model.graXYres18.ToString()) + ";graXYname19 = "+ (model.graXYname19 == null ? "" : model.graXYname19.ToString()) + ";graXYval19 = "+ (model.graXYval19 == null ? "" : model.graXYval19.ToString()) + ";graXYres19 = "+ (model.graXYres19 == null ? "" : model.graXYres19.ToString()) + ";graXYname20 = "+ (model.graXYname20 == null ? "" : model.graXYname20.ToString()) + ";graXYval20 = "+ (model.graXYval20 == null ? "" : model.graXYval20.ToString()) + ";graXYres20 = "+ (model.graXYres20 == null ? "" : model.graXYres20.ToString()) + ";graXYname21 = "+ (model.graXYname21 == null ? "" : model.graXYname21.ToString()) + ";graXYval21 = "+ (model.graXYval21 == null ? "" : model.graXYval21.ToString()) + ";graXYres21 = "+ (model.graXYres21 == null ? "" : model.graXYres21.ToString()) + ";graXYname22 = "+ (model.graXYname22 == null ? "" : model.graXYname22.ToString()) + ";graXYval22 = "+ (model.graXYval22 == null ? "" : model.graXYval22.ToString()) + ";graXYres22 = "+ (model.graXYres22 == null ? "" : model.graXYres22.ToString()) + ";graXYname23 = "+ (model.graXYname23 == null ? "" : model.graXYname23.ToString()) + ";graXYval23 = "+ (model.graXYval23 == null ? "" : model.graXYval23.ToString()) + ";graXYres23 = "+ (model.graXYres23 == null ? "" : model.graXYres23.ToString()) + ";graXYname24 = "+ (model.graXYname24 == null ? "" : model.graXYname24.ToString()) + ";graXYval24 = "+ (model.graXYval24 == null ? "" : model.graXYval24.ToString()) + ";graXYres24 = "+ (model.graXYres24 == null ? "" : model.graXYres24.ToString()) + ";graXYname25 = "+ (model.graXYname25 == null ? "" : model.graXYname25.ToString()) + ";graXYval25 = "+ (model.graXYval25 == null ? "" : model.graXYval25.ToString()) + ";graXYres25 = "+ (model.graXYres25 == null ? "" : model.graXYres25.ToString()) + ";graXYname26 = "+ (model.graXYname26 == null ? "" : model.graXYname26.ToString()) + ";graXYval26 = "+ (model.graXYval26 == null ? "" : model.graXYval26.ToString()) + ";graXYres26 = "+ (model.graXYres26 == null ? "" : model.graXYres26.ToString()) + ";graXYname27 = "+ (model.graXYname27 == null ? "" : model.graXYname27.ToString()) + ";graXYval27 = "+ (model.graXYval27 == null ? "" : model.graXYval27.ToString()) + ";graXYres27 = "+ (model.graXYres27 == null ? "" : model.graXYres27.ToString()) + ";graXYname28 = "+ (model.graXYname28 == null ? "" : model.graXYname28.ToString()) + ";graXYval28 = "+ (model.graXYval28 == null ? "" : model.graXYval28.ToString()) + ";graXYres28 = "+ (model.graXYres28 == null ? "" : model.graXYres28.ToString()) + ";graXYname29 = "+ (model.graXYname29 == null ? "" : model.graXYname29.ToString()) + ";graXYval29 = "+ (model.graXYval29 == null ? "" : model.graXYval29.ToString()) + ";graXYres29 = "+ (model.graXYres29 == null ? "" : model.graXYres29.ToString()) + ";graXYname30 = "+ (model.graXYname30 == null ? "" : model.graXYname30.ToString()) + ";graXYval30 = "+ (model.graXYval30 == null ? "" : model.graXYval30.ToString()) + ";graXYres30 = "+ (model.graXYres30 == null ? "" : model.graXYres30.ToString()) + ";overallResult = "+ (model.overallResult == null ? "" : model.overallResult.ToString()) + ";totalJig = "+ (model.totalJig == null ? "" : model.totalJig.ToString()) + "");
			parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id ;
			parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[2].Value = model.UpdatedTime == null ? (object)DBNull.Value : model.UpdatedTime ;
			parameters[3].Value = model.TransType == null ? (object)DBNull.Value : model.TransType ;
			parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[5].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber ;
			parameters[6].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber ;
			parameters[7].Value = model.modeStatus == null ? (object)DBNull.Value : model.modeStatus ;
			parameters[8].Value = model.currentStatus == null ? (object)DBNull.Value : model.currentStatus ;
			parameters[9].Value = model.totalGraphic == null ? (object)DBNull.Value : model.totalGraphic ;
			parameters[10].Value = model.currentQuantity == null ? (object)DBNull.Value : model.currentQuantity ;
			parameters[11].Value = model.totalQuantity == null ? (object)DBNull.Value : model.totalQuantity ;
			parameters[12].Value = model.mainResult == null ? (object)DBNull.Value : model.mainResult ;
			parameters[13].Value = model.vSystemOk == null ? (object)DBNull.Value : model.vSystemOk ;
			parameters[14].Value = model.runningOk == null ? (object)DBNull.Value : model.runningOk ;
			parameters[15].Value = model.inspectOk == null ? (object)DBNull.Value : model.inspectOk ;
			parameters[16].Value = model.passOk == null ? (object)DBNull.Value : model.passOk ;
			parameters[17].Value = model.failOk == null ? (object)DBNull.Value : model.failOk ;
			parameters[18].Value = model.graXYname1 == null ? (object)DBNull.Value : model.graXYname1 ;
			parameters[19].Value = model.graXYval1 == null ? (object)DBNull.Value : model.graXYval1 ;
			parameters[20].Value = model.graXYres1 == null ? (object)DBNull.Value : model.graXYres1 ;
			parameters[21].Value = model.graXYname2 == null ? (object)DBNull.Value : model.graXYname2 ;
			parameters[22].Value = model.graXYval2 == null ? (object)DBNull.Value : model.graXYval2 ;
			parameters[23].Value = model.graXYres2 == null ? (object)DBNull.Value : model.graXYres2 ;
			parameters[24].Value = model.graXYname3 == null ? (object)DBNull.Value : model.graXYname3 ;
			parameters[25].Value = model.graXYval3 == null ? (object)DBNull.Value : model.graXYval3 ;
			parameters[26].Value = model.graXYres3 == null ? (object)DBNull.Value : model.graXYres3 ;
			parameters[27].Value = model.graXYname4 == null ? (object)DBNull.Value : model.graXYname4 ;
			parameters[28].Value = model.graXYval4 == null ? (object)DBNull.Value : model.graXYval4 ;
			parameters[29].Value = model.graXYres4 == null ? (object)DBNull.Value : model.graXYres4 ;
			parameters[30].Value = model.graXYname5 == null ? (object)DBNull.Value : model.graXYname5 ;
			parameters[31].Value = model.graXYval5 == null ? (object)DBNull.Value : model.graXYval5 ;
			parameters[32].Value = model.graXYres5 == null ? (object)DBNull.Value : model.graXYres5 ;
			parameters[33].Value = model.graXYname6 == null ? (object)DBNull.Value : model.graXYname6 ;
			parameters[34].Value = model.graXYval6 == null ? (object)DBNull.Value : model.graXYval6 ;
			parameters[35].Value = model.graXYres6 == null ? (object)DBNull.Value : model.graXYres6 ;
			parameters[36].Value = model.graXYname7 == null ? (object)DBNull.Value : model.graXYname7 ;
			parameters[37].Value = model.graXYval7 == null ? (object)DBNull.Value : model.graXYval7 ;
			parameters[38].Value = model.graXYres7 == null ? (object)DBNull.Value : model.graXYres7 ;
			parameters[39].Value = model.graXYname8 == null ? (object)DBNull.Value : model.graXYname8 ;
			parameters[40].Value = model.graXYval8 == null ? (object)DBNull.Value : model.graXYval8 ;
			parameters[41].Value = model.graXYres8 == null ? (object)DBNull.Value : model.graXYres8 ;
			parameters[42].Value = model.graXYname9 == null ? (object)DBNull.Value : model.graXYname9 ;
			parameters[43].Value = model.graXYval9 == null ? (object)DBNull.Value : model.graXYval9 ;
			parameters[44].Value = model.graXYres9 == null ? (object)DBNull.Value : model.graXYres9 ;
			parameters[45].Value = model.graXYname10 == null ? (object)DBNull.Value : model.graXYname10 ;
			parameters[46].Value = model.graXYval10 == null ? (object)DBNull.Value : model.graXYval10 ;
			parameters[47].Value = model.graXYres10 == null ? (object)DBNull.Value : model.graXYres10 ;
			parameters[48].Value = model.graXYname11 == null ? (object)DBNull.Value : model.graXYname11 ;
			parameters[49].Value = model.graXYval11 == null ? (object)DBNull.Value : model.graXYval11 ;
			parameters[50].Value = model.graXYres11 == null ? (object)DBNull.Value : model.graXYres11 ;
			parameters[51].Value = model.graXYname12 == null ? (object)DBNull.Value : model.graXYname12 ;
			parameters[52].Value = model.graXYval12 == null ? (object)DBNull.Value : model.graXYval12 ;
			parameters[53].Value = model.graXYres12 == null ? (object)DBNull.Value : model.graXYres12 ;
			parameters[54].Value = model.graXYname13 == null ? (object)DBNull.Value : model.graXYname13 ;
			parameters[55].Value = model.graXYval13 == null ? (object)DBNull.Value : model.graXYval13 ;
			parameters[56].Value = model.graXYres13 == null ? (object)DBNull.Value : model.graXYres13 ;
			parameters[57].Value = model.graXYname14 == null ? (object)DBNull.Value : model.graXYname14 ;
			parameters[58].Value = model.graXYval14 == null ? (object)DBNull.Value : model.graXYval14 ;
			parameters[59].Value = model.graXYres14 == null ? (object)DBNull.Value : model.graXYres14 ;
			parameters[60].Value = model.graXYname15 == null ? (object)DBNull.Value : model.graXYname15 ;
			parameters[61].Value = model.graXYval15 == null ? (object)DBNull.Value : model.graXYval15 ;
			parameters[62].Value = model.graXYres15 == null ? (object)DBNull.Value : model.graXYres15 ;
			parameters[63].Value = model.graXYname16 == null ? (object)DBNull.Value : model.graXYname16 ;
			parameters[64].Value = model.graXYval16 == null ? (object)DBNull.Value : model.graXYval16 ;
			parameters[65].Value = model.graXYres16 == null ? (object)DBNull.Value : model.graXYres16 ;
			parameters[66].Value = model.graXYname17 == null ? (object)DBNull.Value : model.graXYname17 ;
			parameters[67].Value = model.graXYval17 == null ? (object)DBNull.Value : model.graXYval17 ;
			parameters[68].Value = model.graXYres17 == null ? (object)DBNull.Value : model.graXYres17 ;
			parameters[69].Value = model.graXYname18 == null ? (object)DBNull.Value : model.graXYname18 ;
			parameters[70].Value = model.graXYval18 == null ? (object)DBNull.Value : model.graXYval18 ;
			parameters[71].Value = model.graXYres18 == null ? (object)DBNull.Value : model.graXYres18 ;
			parameters[72].Value = model.graXYname19 == null ? (object)DBNull.Value : model.graXYname19 ;
			parameters[73].Value = model.graXYval19 == null ? (object)DBNull.Value : model.graXYval19 ;
			parameters[74].Value = model.graXYres19 == null ? (object)DBNull.Value : model.graXYres19 ;
			parameters[75].Value = model.graXYname20 == null ? (object)DBNull.Value : model.graXYname20 ;
			parameters[76].Value = model.graXYval20 == null ? (object)DBNull.Value : model.graXYval20 ;
			parameters[77].Value = model.graXYres20 == null ? (object)DBNull.Value : model.graXYres20 ;
			parameters[78].Value = model.graXYname21 == null ? (object)DBNull.Value : model.graXYname21 ;
			parameters[79].Value = model.graXYval21 == null ? (object)DBNull.Value : model.graXYval21 ;
			parameters[80].Value = model.graXYres21 == null ? (object)DBNull.Value : model.graXYres21 ;
			parameters[81].Value = model.graXYname22 == null ? (object)DBNull.Value : model.graXYname22 ;
			parameters[82].Value = model.graXYval22 == null ? (object)DBNull.Value : model.graXYval22 ;
			parameters[83].Value = model.graXYres22 == null ? (object)DBNull.Value : model.graXYres22 ;
			parameters[84].Value = model.graXYname23 == null ? (object)DBNull.Value : model.graXYname23 ;
			parameters[85].Value = model.graXYval23 == null ? (object)DBNull.Value : model.graXYval23 ;
			parameters[86].Value = model.graXYres23 == null ? (object)DBNull.Value : model.graXYres23 ;
			parameters[87].Value = model.graXYname24 == null ? (object)DBNull.Value : model.graXYname24 ;
			parameters[88].Value = model.graXYval24 == null ? (object)DBNull.Value : model.graXYval24 ;
			parameters[89].Value = model.graXYres24 == null ? (object)DBNull.Value : model.graXYres24 ;
			parameters[90].Value = model.graXYname25 == null ? (object)DBNull.Value : model.graXYname25 ;
			parameters[91].Value = model.graXYval25 == null ? (object)DBNull.Value : model.graXYval25 ;
			parameters[92].Value = model.graXYres25 == null ? (object)DBNull.Value : model.graXYres25 ;
			parameters[93].Value = model.graXYname26 == null ? (object)DBNull.Value : model.graXYname26 ;
			parameters[94].Value = model.graXYval26 == null ? (object)DBNull.Value : model.graXYval26 ;
			parameters[95].Value = model.graXYres26 == null ? (object)DBNull.Value : model.graXYres26 ;
			parameters[96].Value = model.graXYname27 == null ? (object)DBNull.Value : model.graXYname27 ;
			parameters[97].Value = model.graXYval27 == null ? (object)DBNull.Value : model.graXYval27 ;
			parameters[98].Value = model.graXYres27 == null ? (object)DBNull.Value : model.graXYres27 ;
			parameters[99].Value = model.graXYname28 == null ? (object)DBNull.Value : model.graXYname28 ;
			parameters[100].Value = model.graXYval28 == null ? (object)DBNull.Value : model.graXYval28 ;
			parameters[101].Value = model.graXYres28 == null ? (object)DBNull.Value : model.graXYres28 ;
			parameters[102].Value = model.graXYname29 == null ? (object)DBNull.Value : model.graXYname29 ;
			parameters[103].Value = model.graXYval29 == null ? (object)DBNull.Value : model.graXYval29 ;
			parameters[104].Value = model.graXYres29 == null ? (object)DBNull.Value : model.graXYres29 ;
			parameters[105].Value = model.graXYname30 == null ? (object)DBNull.Value : model.graXYname30 ;
			parameters[106].Value = model.graXYval30 == null ? (object)DBNull.Value : model.graXYval30 ;
			parameters[107].Value = model.graXYres30 == null ? (object)DBNull.Value : model.graXYres30 ;
			parameters[108].Value = model.overallResult == null ? (object)DBNull.Value : model.overallResult ;
			parameters[109].Value = model.totalJig == null ? (object)DBNull.Value : model.totalJig ;

			 return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Common.Model.LMMSClientVisionLog_His_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSClientVisionLog_His set ");
			strSql.Append("id=@id,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("UpdatedTime=@UpdatedTime,");
			strSql.Append("TransType=@TransType,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("partNumber=@partNumber,");
			strSql.Append("jobNumber=@jobNumber,");
			strSql.Append("modeStatus=@modeStatus,");
			strSql.Append("currentStatus=@currentStatus,");
			strSql.Append("totalGraphic=@totalGraphic,");
			strSql.Append("currentQuantity=@currentQuantity,");
			strSql.Append("totalQuantity=@totalQuantity,");
			strSql.Append("mainResult=@mainResult,");
			strSql.Append("vSystemOk=@vSystemOk,");
			strSql.Append("runningOk=@runningOk,");
			strSql.Append("inspectOk=@inspectOk,");
			strSql.Append("passOk=@passOk,");
			strSql.Append("failOk=@failOk,");
			strSql.Append("graXYname1=@graXYname1,");
			strSql.Append("graXYval1=@graXYval1,");
			strSql.Append("graXYres1=@graXYres1,");
			strSql.Append("graXYname2=@graXYname2,");
			strSql.Append("graXYval2=@graXYval2,");
			strSql.Append("graXYres2=@graXYres2,");
			strSql.Append("graXYname3=@graXYname3,");
			strSql.Append("graXYval3=@graXYval3,");
			strSql.Append("graXYres3=@graXYres3,");
			strSql.Append("graXYname4=@graXYname4,");
			strSql.Append("graXYval4=@graXYval4,");
			strSql.Append("graXYres4=@graXYres4,");
			strSql.Append("graXYname5=@graXYname5,");
			strSql.Append("graXYval5=@graXYval5,");
			strSql.Append("graXYres5=@graXYres5,");
			strSql.Append("graXYname6=@graXYname6,");
			strSql.Append("graXYval6=@graXYval6,");
			strSql.Append("graXYres6=@graXYres6,");
			strSql.Append("graXYname7=@graXYname7,");
			strSql.Append("graXYval7=@graXYval7,");
			strSql.Append("graXYres7=@graXYres7,");
			strSql.Append("graXYname8=@graXYname8,");
			strSql.Append("graXYval8=@graXYval8,");
			strSql.Append("graXYres8=@graXYres8,");
			strSql.Append("graXYname9=@graXYname9,");
			strSql.Append("graXYval9=@graXYval9,");
			strSql.Append("graXYres9=@graXYres9,");
			strSql.Append("graXYname10=@graXYname10,");
			strSql.Append("graXYval10=@graXYval10,");
			strSql.Append("graXYres10=@graXYres10,");
			strSql.Append("graXYname11=@graXYname11,");
			strSql.Append("graXYval11=@graXYval11,");
			strSql.Append("graXYres11=@graXYres11,");
			strSql.Append("graXYname12=@graXYname12,");
			strSql.Append("graXYval12=@graXYval12,");
			strSql.Append("graXYres12=@graXYres12,");
			strSql.Append("graXYname13=@graXYname13,");
			strSql.Append("graXYval13=@graXYval13,");
			strSql.Append("graXYres13=@graXYres13,");
			strSql.Append("graXYname14=@graXYname14,");
			strSql.Append("graXYval14=@graXYval14,");
			strSql.Append("graXYres14=@graXYres14,");
			strSql.Append("graXYname15=@graXYname15,");
			strSql.Append("graXYval15=@graXYval15,");
			strSql.Append("graXYres15=@graXYres15,");
			strSql.Append("graXYname16=@graXYname16,");
			strSql.Append("graXYval16=@graXYval16,");
			strSql.Append("graXYres16=@graXYres16,");
			strSql.Append("graXYname17=@graXYname17,");
			strSql.Append("graXYval17=@graXYval17,");
			strSql.Append("graXYres17=@graXYres17,");
			strSql.Append("graXYname18=@graXYname18,");
			strSql.Append("graXYval18=@graXYval18,");
			strSql.Append("graXYres18=@graXYres18,");
			strSql.Append("graXYname19=@graXYname19,");
			strSql.Append("graXYval19=@graXYval19,");
			strSql.Append("graXYres19=@graXYres19,");
			strSql.Append("graXYname20=@graXYname20,");
			strSql.Append("graXYval20=@graXYval20,");
			strSql.Append("graXYres20=@graXYres20,");
			strSql.Append("graXYname21=@graXYname21,");
			strSql.Append("graXYval21=@graXYval21,");
			strSql.Append("graXYres21=@graXYres21,");
			strSql.Append("graXYname22=@graXYname22,");
			strSql.Append("graXYval22=@graXYval22,");
			strSql.Append("graXYres22=@graXYres22,");
			strSql.Append("graXYname23=@graXYname23,");
			strSql.Append("graXYval23=@graXYval23,");
			strSql.Append("graXYres23=@graXYres23,");
			strSql.Append("graXYname24=@graXYname24,");
			strSql.Append("graXYval24=@graXYval24,");
			strSql.Append("graXYres24=@graXYres24,");
			strSql.Append("graXYname25=@graXYname25,");
			strSql.Append("graXYval25=@graXYval25,");
			strSql.Append("graXYres25=@graXYres25,");
			strSql.Append("graXYname26=@graXYname26,");
			strSql.Append("graXYval26=@graXYval26,");
			strSql.Append("graXYres26=@graXYres26,");
			strSql.Append("graXYname27=@graXYname27,");
			strSql.Append("graXYval27=@graXYval27,");
			strSql.Append("graXYres27=@graXYres27,");
			strSql.Append("graXYname28=@graXYname28,");
			strSql.Append("graXYval28=@graXYval28,");
			strSql.Append("graXYres28=@graXYres28,");
			strSql.Append("graXYname29=@graXYname29,");
			strSql.Append("graXYval29=@graXYval29,");
			strSql.Append("graXYres29=@graXYres29,");
			strSql.Append("graXYname30=@graXYname30,");
			strSql.Append("graXYval30=@graXYval30,");
			strSql.Append("graXYres30=@graXYres30,");
			strSql.Append("overallResult=@overallResult,");
			strSql.Append("totalJig=@totalJig");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@UpdatedTime", SqlDbType.DateTime2,8),
					new SqlParameter("@TransType", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@jobNumber", SqlDbType.VarChar,50),
					new SqlParameter("@modeStatus", SqlDbType.VarChar,50),
					new SqlParameter("@currentStatus", SqlDbType.VarChar,50),
					new SqlParameter("@totalGraphic", SqlDbType.Int,4),
					new SqlParameter("@currentQuantity", SqlDbType.Int,4),
					new SqlParameter("@totalQuantity", SqlDbType.Int,4),
					new SqlParameter("@mainResult", SqlDbType.VarChar,50),
					new SqlParameter("@vSystemOk", SqlDbType.VarChar,50),
					new SqlParameter("@runningOk", SqlDbType.VarChar,50),
					new SqlParameter("@inspectOk", SqlDbType.VarChar,50),
					new SqlParameter("@passOk", SqlDbType.VarChar,50),
					new SqlParameter("@failOk", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname30", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval30", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres30", SqlDbType.VarChar,50),
					new SqlParameter("@overallResult", SqlDbType.VarChar,50),
					new SqlParameter("@totalJig", SqlDbType.Int,4)};
			parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id ;
			parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[2].Value = model.UpdatedTime == null ? (object)DBNull.Value : model.UpdatedTime ;
			parameters[3].Value = model.TransType == null ? (object)DBNull.Value : model.TransType ;
			parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[5].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber ;
			parameters[6].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber ;
			parameters[7].Value = model.modeStatus == null ? (object)DBNull.Value : model.modeStatus ;
			parameters[8].Value = model.currentStatus == null ? (object)DBNull.Value : model.currentStatus ;
			parameters[9].Value = model.totalGraphic == null ? (object)DBNull.Value : model.totalGraphic ;
			parameters[10].Value = model.currentQuantity == null ? (object)DBNull.Value : model.currentQuantity ;
			parameters[11].Value = model.totalQuantity == null ? (object)DBNull.Value : model.totalQuantity ;
			parameters[12].Value = model.mainResult == null ? (object)DBNull.Value : model.mainResult ;
			parameters[13].Value = model.vSystemOk == null ? (object)DBNull.Value : model.vSystemOk ;
			parameters[14].Value = model.runningOk == null ? (object)DBNull.Value : model.runningOk ;
			parameters[15].Value = model.inspectOk == null ? (object)DBNull.Value : model.inspectOk ;
			parameters[16].Value = model.passOk == null ? (object)DBNull.Value : model.passOk ;
			parameters[17].Value = model.failOk == null ? (object)DBNull.Value : model.failOk ;
			parameters[18].Value = model.graXYname1 == null ? (object)DBNull.Value : model.graXYname1 ;
			parameters[19].Value = model.graXYval1 == null ? (object)DBNull.Value : model.graXYval1 ;
			parameters[20].Value = model.graXYres1 == null ? (object)DBNull.Value : model.graXYres1 ;
			parameters[21].Value = model.graXYname2 == null ? (object)DBNull.Value : model.graXYname2 ;
			parameters[22].Value = model.graXYval2 == null ? (object)DBNull.Value : model.graXYval2 ;
			parameters[23].Value = model.graXYres2 == null ? (object)DBNull.Value : model.graXYres2 ;
			parameters[24].Value = model.graXYname3 == null ? (object)DBNull.Value : model.graXYname3 ;
			parameters[25].Value = model.graXYval3 == null ? (object)DBNull.Value : model.graXYval3 ;
			parameters[26].Value = model.graXYres3 == null ? (object)DBNull.Value : model.graXYres3 ;
			parameters[27].Value = model.graXYname4 == null ? (object)DBNull.Value : model.graXYname4 ;
			parameters[28].Value = model.graXYval4 == null ? (object)DBNull.Value : model.graXYval4 ;
			parameters[29].Value = model.graXYres4 == null ? (object)DBNull.Value : model.graXYres4 ;
			parameters[30].Value = model.graXYname5 == null ? (object)DBNull.Value : model.graXYname5 ;
			parameters[31].Value = model.graXYval5 == null ? (object)DBNull.Value : model.graXYval5 ;
			parameters[32].Value = model.graXYres5 == null ? (object)DBNull.Value : model.graXYres5 ;
			parameters[33].Value = model.graXYname6 == null ? (object)DBNull.Value : model.graXYname6 ;
			parameters[34].Value = model.graXYval6 == null ? (object)DBNull.Value : model.graXYval6 ;
			parameters[35].Value = model.graXYres6 == null ? (object)DBNull.Value : model.graXYres6 ;
			parameters[36].Value = model.graXYname7 == null ? (object)DBNull.Value : model.graXYname7 ;
			parameters[37].Value = model.graXYval7 == null ? (object)DBNull.Value : model.graXYval7 ;
			parameters[38].Value = model.graXYres7 == null ? (object)DBNull.Value : model.graXYres7 ;
			parameters[39].Value = model.graXYname8 == null ? (object)DBNull.Value : model.graXYname8 ;
			parameters[40].Value = model.graXYval8 == null ? (object)DBNull.Value : model.graXYval8 ;
			parameters[41].Value = model.graXYres8 == null ? (object)DBNull.Value : model.graXYres8 ;
			parameters[42].Value = model.graXYname9 == null ? (object)DBNull.Value : model.graXYname9 ;
			parameters[43].Value = model.graXYval9 == null ? (object)DBNull.Value : model.graXYval9 ;
			parameters[44].Value = model.graXYres9 == null ? (object)DBNull.Value : model.graXYres9 ;
			parameters[45].Value = model.graXYname10 == null ? (object)DBNull.Value : model.graXYname10 ;
			parameters[46].Value = model.graXYval10 == null ? (object)DBNull.Value : model.graXYval10 ;
			parameters[47].Value = model.graXYres10 == null ? (object)DBNull.Value : model.graXYres10 ;
			parameters[48].Value = model.graXYname11 == null ? (object)DBNull.Value : model.graXYname11 ;
			parameters[49].Value = model.graXYval11 == null ? (object)DBNull.Value : model.graXYval11 ;
			parameters[50].Value = model.graXYres11 == null ? (object)DBNull.Value : model.graXYres11 ;
			parameters[51].Value = model.graXYname12 == null ? (object)DBNull.Value : model.graXYname12 ;
			parameters[52].Value = model.graXYval12 == null ? (object)DBNull.Value : model.graXYval12 ;
			parameters[53].Value = model.graXYres12 == null ? (object)DBNull.Value : model.graXYres12 ;
			parameters[54].Value = model.graXYname13 == null ? (object)DBNull.Value : model.graXYname13 ;
			parameters[55].Value = model.graXYval13 == null ? (object)DBNull.Value : model.graXYval13 ;
			parameters[56].Value = model.graXYres13 == null ? (object)DBNull.Value : model.graXYres13 ;
			parameters[57].Value = model.graXYname14 == null ? (object)DBNull.Value : model.graXYname14 ;
			parameters[58].Value = model.graXYval14 == null ? (object)DBNull.Value : model.graXYval14 ;
			parameters[59].Value = model.graXYres14 == null ? (object)DBNull.Value : model.graXYres14 ;
			parameters[60].Value = model.graXYname15 == null ? (object)DBNull.Value : model.graXYname15 ;
			parameters[61].Value = model.graXYval15 == null ? (object)DBNull.Value : model.graXYval15 ;
			parameters[62].Value = model.graXYres15 == null ? (object)DBNull.Value : model.graXYres15 ;
			parameters[63].Value = model.graXYname16 == null ? (object)DBNull.Value : model.graXYname16 ;
			parameters[64].Value = model.graXYval16 == null ? (object)DBNull.Value : model.graXYval16 ;
			parameters[65].Value = model.graXYres16 == null ? (object)DBNull.Value : model.graXYres16 ;
			parameters[66].Value = model.graXYname17 == null ? (object)DBNull.Value : model.graXYname17 ;
			parameters[67].Value = model.graXYval17 == null ? (object)DBNull.Value : model.graXYval17 ;
			parameters[68].Value = model.graXYres17 == null ? (object)DBNull.Value : model.graXYres17 ;
			parameters[69].Value = model.graXYname18 == null ? (object)DBNull.Value : model.graXYname18 ;
			parameters[70].Value = model.graXYval18 == null ? (object)DBNull.Value : model.graXYval18 ;
			parameters[71].Value = model.graXYres18 == null ? (object)DBNull.Value : model.graXYres18 ;
			parameters[72].Value = model.graXYname19 == null ? (object)DBNull.Value : model.graXYname19 ;
			parameters[73].Value = model.graXYval19 == null ? (object)DBNull.Value : model.graXYval19 ;
			parameters[74].Value = model.graXYres19 == null ? (object)DBNull.Value : model.graXYres19 ;
			parameters[75].Value = model.graXYname20 == null ? (object)DBNull.Value : model.graXYname20 ;
			parameters[76].Value = model.graXYval20 == null ? (object)DBNull.Value : model.graXYval20 ;
			parameters[77].Value = model.graXYres20 == null ? (object)DBNull.Value : model.graXYres20 ;
			parameters[78].Value = model.graXYname21 == null ? (object)DBNull.Value : model.graXYname21 ;
			parameters[79].Value = model.graXYval21 == null ? (object)DBNull.Value : model.graXYval21 ;
			parameters[80].Value = model.graXYres21 == null ? (object)DBNull.Value : model.graXYres21 ;
			parameters[81].Value = model.graXYname22 == null ? (object)DBNull.Value : model.graXYname22 ;
			parameters[82].Value = model.graXYval22 == null ? (object)DBNull.Value : model.graXYval22 ;
			parameters[83].Value = model.graXYres22 == null ? (object)DBNull.Value : model.graXYres22 ;
			parameters[84].Value = model.graXYname23 == null ? (object)DBNull.Value : model.graXYname23 ;
			parameters[85].Value = model.graXYval23 == null ? (object)DBNull.Value : model.graXYval23 ;
			parameters[86].Value = model.graXYres23 == null ? (object)DBNull.Value : model.graXYres23 ;
			parameters[87].Value = model.graXYname24 == null ? (object)DBNull.Value : model.graXYname24 ;
			parameters[88].Value = model.graXYval24 == null ? (object)DBNull.Value : model.graXYval24 ;
			parameters[89].Value = model.graXYres24 == null ? (object)DBNull.Value : model.graXYres24 ;
			parameters[90].Value = model.graXYname25 == null ? (object)DBNull.Value : model.graXYname25 ;
			parameters[91].Value = model.graXYval25 == null ? (object)DBNull.Value : model.graXYval25 ;
			parameters[92].Value = model.graXYres25 == null ? (object)DBNull.Value : model.graXYres25 ;
			parameters[93].Value = model.graXYname26 == null ? (object)DBNull.Value : model.graXYname26 ;
			parameters[94].Value = model.graXYval26 == null ? (object)DBNull.Value : model.graXYval26 ;
			parameters[95].Value = model.graXYres26 == null ? (object)DBNull.Value : model.graXYres26 ;
			parameters[96].Value = model.graXYname27 == null ? (object)DBNull.Value : model.graXYname27 ;
			parameters[97].Value = model.graXYval27 == null ? (object)DBNull.Value : model.graXYval27 ;
			parameters[98].Value = model.graXYres27 == null ? (object)DBNull.Value : model.graXYres27 ;
			parameters[99].Value = model.graXYname28 == null ? (object)DBNull.Value : model.graXYname28 ;
			parameters[100].Value = model.graXYval28 == null ? (object)DBNull.Value : model.graXYval28 ;
			parameters[101].Value = model.graXYres28 == null ? (object)DBNull.Value : model.graXYres28 ;
			parameters[102].Value = model.graXYname29 == null ? (object)DBNull.Value : model.graXYname29 ;
			parameters[103].Value = model.graXYval29 == null ? (object)DBNull.Value : model.graXYval29 ;
			parameters[104].Value = model.graXYres29 == null ? (object)DBNull.Value : model.graXYres29 ;
			parameters[105].Value = model.graXYname30 == null ? (object)DBNull.Value : model.graXYname30 ;
			parameters[106].Value = model.graXYval30 == null ? (object)DBNull.Value : model.graXYval30 ;
			parameters[107].Value = model.graXYres30 == null ? (object)DBNull.Value : model.graXYres30 ;
			parameters[108].Value = model.overallResult == null ? (object)DBNull.Value : model.overallResult ;
			parameters[109].Value = model.totalJig == null ? (object)DBNull.Value : model.totalJig ;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSClientVisionLog_His_DAL" , "Function:		public bool Update(Common.Model.LMMSClientVisionLog_His_Model model)"  + "TableName:LMMSClientVisionLog_His" , ";id = "+ (model.id == null ? "" : model.id.ToString() ) + ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString() ) + ";UpdatedTime = "+ (model.UpdatedTime == null ? "" : model.UpdatedTime.ToString() ) + ";TransType = "+ (model.TransType == null ? "" : model.TransType.ToString() ) + ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString() ) + ";partNumber = "+ (model.partNumber == null ? "" : model.partNumber.ToString() ) + ";jobNumber = "+ (model.jobNumber == null ? "" : model.jobNumber.ToString() ) + ";modeStatus = "+ (model.modeStatus == null ? "" : model.modeStatus.ToString() ) + ";currentStatus = "+ (model.currentStatus == null ? "" : model.currentStatus.ToString() ) + ";totalGraphic = "+ (model.totalGraphic == null ? "" : model.totalGraphic.ToString() ) + ";currentQuantity = "+ (model.currentQuantity == null ? "" : model.currentQuantity.ToString() ) + ";totalQuantity = "+ (model.totalQuantity == null ? "" : model.totalQuantity.ToString() ) + ";mainResult = "+ (model.mainResult == null ? "" : model.mainResult.ToString() ) + ";vSystemOk = "+ (model.vSystemOk == null ? "" : model.vSystemOk.ToString() ) + ";runningOk = "+ (model.runningOk == null ? "" : model.runningOk.ToString() ) + ";inspectOk = "+ (model.inspectOk == null ? "" : model.inspectOk.ToString() ) + ";passOk = "+ (model.passOk == null ? "" : model.passOk.ToString() ) + ";failOk = "+ (model.failOk == null ? "" : model.failOk.ToString() ) + ";graXYname1 = "+ (model.graXYname1 == null ? "" : model.graXYname1.ToString() ) + ";graXYval1 = "+ (model.graXYval1 == null ? "" : model.graXYval1.ToString() ) + ";graXYres1 = "+ (model.graXYres1 == null ? "" : model.graXYres1.ToString() ) + ";graXYname2 = "+ (model.graXYname2 == null ? "" : model.graXYname2.ToString() ) + ";graXYval2 = "+ (model.graXYval2 == null ? "" : model.graXYval2.ToString() ) + ";graXYres2 = "+ (model.graXYres2 == null ? "" : model.graXYres2.ToString() ) + ";graXYname3 = "+ (model.graXYname3 == null ? "" : model.graXYname3.ToString() ) + ";graXYval3 = "+ (model.graXYval3 == null ? "" : model.graXYval3.ToString() ) + ";graXYres3 = "+ (model.graXYres3 == null ? "" : model.graXYres3.ToString() ) + ";graXYname4 = "+ (model.graXYname4 == null ? "" : model.graXYname4.ToString() ) + ";graXYval4 = "+ (model.graXYval4 == null ? "" : model.graXYval4.ToString() ) + ";graXYres4 = "+ (model.graXYres4 == null ? "" : model.graXYres4.ToString() ) + ";graXYname5 = "+ (model.graXYname5 == null ? "" : model.graXYname5.ToString() ) + ";graXYval5 = "+ (model.graXYval5 == null ? "" : model.graXYval5.ToString() ) + ";graXYres5 = "+ (model.graXYres5 == null ? "" : model.graXYres5.ToString() ) + ";graXYname6 = "+ (model.graXYname6 == null ? "" : model.graXYname6.ToString() ) + ";graXYval6 = "+ (model.graXYval6 == null ? "" : model.graXYval6.ToString() ) + ";graXYres6 = "+ (model.graXYres6 == null ? "" : model.graXYres6.ToString() ) + ";graXYname7 = "+ (model.graXYname7 == null ? "" : model.graXYname7.ToString() ) + ";graXYval7 = "+ (model.graXYval7 == null ? "" : model.graXYval7.ToString() ) + ";graXYres7 = "+ (model.graXYres7 == null ? "" : model.graXYres7.ToString() ) + ";graXYname8 = "+ (model.graXYname8 == null ? "" : model.graXYname8.ToString() ) + ";graXYval8 = "+ (model.graXYval8 == null ? "" : model.graXYval8.ToString() ) + ";graXYres8 = "+ (model.graXYres8 == null ? "" : model.graXYres8.ToString() ) + ";graXYname9 = "+ (model.graXYname9 == null ? "" : model.graXYname9.ToString() ) + ";graXYval9 = "+ (model.graXYval9 == null ? "" : model.graXYval9.ToString() ) + ";graXYres9 = "+ (model.graXYres9 == null ? "" : model.graXYres9.ToString() ) + ";graXYname10 = "+ (model.graXYname10 == null ? "" : model.graXYname10.ToString() ) + ";graXYval10 = "+ (model.graXYval10 == null ? "" : model.graXYval10.ToString() ) + ";graXYres10 = "+ (model.graXYres10 == null ? "" : model.graXYres10.ToString() ) + ";graXYname11 = "+ (model.graXYname11 == null ? "" : model.graXYname11.ToString() ) + ";graXYval11 = "+ (model.graXYval11 == null ? "" : model.graXYval11.ToString() ) + ";graXYres11 = "+ (model.graXYres11 == null ? "" : model.graXYres11.ToString() ) + ";graXYname12 = "+ (model.graXYname12 == null ? "" : model.graXYname12.ToString() ) + ";graXYval12 = "+ (model.graXYval12 == null ? "" : model.graXYval12.ToString() ) + ";graXYres12 = "+ (model.graXYres12 == null ? "" : model.graXYres12.ToString() ) + ";graXYname13 = "+ (model.graXYname13 == null ? "" : model.graXYname13.ToString() ) + ";graXYval13 = "+ (model.graXYval13 == null ? "" : model.graXYval13.ToString() ) + ";graXYres13 = "+ (model.graXYres13 == null ? "" : model.graXYres13.ToString() ) + ";graXYname14 = "+ (model.graXYname14 == null ? "" : model.graXYname14.ToString() ) + ";graXYval14 = "+ (model.graXYval14 == null ? "" : model.graXYval14.ToString() ) + ";graXYres14 = "+ (model.graXYres14 == null ? "" : model.graXYres14.ToString() ) + ";graXYname15 = "+ (model.graXYname15 == null ? "" : model.graXYname15.ToString() ) + ";graXYval15 = "+ (model.graXYval15 == null ? "" : model.graXYval15.ToString() ) + ";graXYres15 = "+ (model.graXYres15 == null ? "" : model.graXYres15.ToString() ) + ";graXYname16 = "+ (model.graXYname16 == null ? "" : model.graXYname16.ToString() ) + ";graXYval16 = "+ (model.graXYval16 == null ? "" : model.graXYval16.ToString() ) + ";graXYres16 = "+ (model.graXYres16 == null ? "" : model.graXYres16.ToString() ) + ";graXYname17 = "+ (model.graXYname17 == null ? "" : model.graXYname17.ToString() ) + ";graXYval17 = "+ (model.graXYval17 == null ? "" : model.graXYval17.ToString() ) + ";graXYres17 = "+ (model.graXYres17 == null ? "" : model.graXYres17.ToString() ) + ";graXYname18 = "+ (model.graXYname18 == null ? "" : model.graXYname18.ToString() ) + ";graXYval18 = "+ (model.graXYval18 == null ? "" : model.graXYval18.ToString() ) + ";graXYres18 = "+ (model.graXYres18 == null ? "" : model.graXYres18.ToString() ) + ";graXYname19 = "+ (model.graXYname19 == null ? "" : model.graXYname19.ToString() ) + ";graXYval19 = "+ (model.graXYval19 == null ? "" : model.graXYval19.ToString() ) + ";graXYres19 = "+ (model.graXYres19 == null ? "" : model.graXYres19.ToString() ) + ";graXYname20 = "+ (model.graXYname20 == null ? "" : model.graXYname20.ToString() ) + ";graXYval20 = "+ (model.graXYval20 == null ? "" : model.graXYval20.ToString() ) + ";graXYres20 = "+ (model.graXYres20 == null ? "" : model.graXYres20.ToString() ) + ";graXYname21 = "+ (model.graXYname21 == null ? "" : model.graXYname21.ToString() ) + ";graXYval21 = "+ (model.graXYval21 == null ? "" : model.graXYval21.ToString() ) + ";graXYres21 = "+ (model.graXYres21 == null ? "" : model.graXYres21.ToString() ) + ";graXYname22 = "+ (model.graXYname22 == null ? "" : model.graXYname22.ToString() ) + ";graXYval22 = "+ (model.graXYval22 == null ? "" : model.graXYval22.ToString() ) + ";graXYres22 = "+ (model.graXYres22 == null ? "" : model.graXYres22.ToString() ) + ";graXYname23 = "+ (model.graXYname23 == null ? "" : model.graXYname23.ToString() ) + ";graXYval23 = "+ (model.graXYval23 == null ? "" : model.graXYval23.ToString() ) + ";graXYres23 = "+ (model.graXYres23 == null ? "" : model.graXYres23.ToString() ) + ";graXYname24 = "+ (model.graXYname24 == null ? "" : model.graXYname24.ToString() ) + ";graXYval24 = "+ (model.graXYval24 == null ? "" : model.graXYval24.ToString() ) + ";graXYres24 = "+ (model.graXYres24 == null ? "" : model.graXYres24.ToString() ) + ";graXYname25 = "+ (model.graXYname25 == null ? "" : model.graXYname25.ToString() ) + ";graXYval25 = "+ (model.graXYval25 == null ? "" : model.graXYval25.ToString() ) + ";graXYres25 = "+ (model.graXYres25 == null ? "" : model.graXYres25.ToString() ) + ";graXYname26 = "+ (model.graXYname26 == null ? "" : model.graXYname26.ToString() ) + ";graXYval26 = "+ (model.graXYval26 == null ? "" : model.graXYval26.ToString() ) + ";graXYres26 = "+ (model.graXYres26 == null ? "" : model.graXYres26.ToString() ) + ";graXYname27 = "+ (model.graXYname27 == null ? "" : model.graXYname27.ToString() ) + ";graXYval27 = "+ (model.graXYval27 == null ? "" : model.graXYval27.ToString() ) + ";graXYres27 = "+ (model.graXYres27 == null ? "" : model.graXYres27.ToString() ) + ";graXYname28 = "+ (model.graXYname28 == null ? "" : model.graXYname28.ToString() ) + ";graXYval28 = "+ (model.graXYval28 == null ? "" : model.graXYval28.ToString() ) + ";graXYres28 = "+ (model.graXYres28 == null ? "" : model.graXYres28.ToString() ) + ";graXYname29 = "+ (model.graXYname29 == null ? "" : model.graXYname29.ToString() ) + ";graXYval29 = "+ (model.graXYval29 == null ? "" : model.graXYval29.ToString() ) + ";graXYres29 = "+ (model.graXYres29 == null ? "" : model.graXYres29.ToString() ) + ";graXYname30 = "+ (model.graXYname30 == null ? "" : model.graXYname30.ToString() ) + ";graXYval30 = "+ (model.graXYval30 == null ? "" : model.graXYval30.ToString() ) + ";graXYres30 = "+ (model.graXYres30 == null ? "" : model.graXYres30.ToString() ) + ";overallResult = "+ (model.overallResult == null ? "" : model.overallResult.ToString() ) + ";totalJig = "+ (model.totalJig == null ? "" : model.totalJig.ToString() ) + "");
			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public SqlCommand UpdateCommand(Common.Model.LMMSClientVisionLog_His_Model model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LMMSClientVisionLog_His set ");
			strSql.Append("id=@id,");
			strSql.Append("dateTime=@dateTime,");
			strSql.Append("UpdatedTime=@UpdatedTime,");
			strSql.Append("TransType=@TransType,");
			strSql.Append("machineID=@machineID,");
			strSql.Append("partNumber=@partNumber,");
			strSql.Append("jobNumber=@jobNumber,");
			strSql.Append("modeStatus=@modeStatus,");
			strSql.Append("currentStatus=@currentStatus,");
			strSql.Append("totalGraphic=@totalGraphic,");
			strSql.Append("currentQuantity=@currentQuantity,");
			strSql.Append("totalQuantity=@totalQuantity,");
			strSql.Append("mainResult=@mainResult,");
			strSql.Append("vSystemOk=@vSystemOk,");
			strSql.Append("runningOk=@runningOk,");
			strSql.Append("inspectOk=@inspectOk,");
			strSql.Append("passOk=@passOk,");
			strSql.Append("failOk=@failOk,");
			strSql.Append("graXYname1=@graXYname1,");
			strSql.Append("graXYval1=@graXYval1,");
			strSql.Append("graXYres1=@graXYres1,");
			strSql.Append("graXYname2=@graXYname2,");
			strSql.Append("graXYval2=@graXYval2,");
			strSql.Append("graXYres2=@graXYres2,");
			strSql.Append("graXYname3=@graXYname3,");
			strSql.Append("graXYval3=@graXYval3,");
			strSql.Append("graXYres3=@graXYres3,");
			strSql.Append("graXYname4=@graXYname4,");
			strSql.Append("graXYval4=@graXYval4,");
			strSql.Append("graXYres4=@graXYres4,");
			strSql.Append("graXYname5=@graXYname5,");
			strSql.Append("graXYval5=@graXYval5,");
			strSql.Append("graXYres5=@graXYres5,");
			strSql.Append("graXYname6=@graXYname6,");
			strSql.Append("graXYval6=@graXYval6,");
			strSql.Append("graXYres6=@graXYres6,");
			strSql.Append("graXYname7=@graXYname7,");
			strSql.Append("graXYval7=@graXYval7,");
			strSql.Append("graXYres7=@graXYres7,");
			strSql.Append("graXYname8=@graXYname8,");
			strSql.Append("graXYval8=@graXYval8,");
			strSql.Append("graXYres8=@graXYres8,");
			strSql.Append("graXYname9=@graXYname9,");
			strSql.Append("graXYval9=@graXYval9,");
			strSql.Append("graXYres9=@graXYres9,");
			strSql.Append("graXYname10=@graXYname10,");
			strSql.Append("graXYval10=@graXYval10,");
			strSql.Append("graXYres10=@graXYres10,");
			strSql.Append("graXYname11=@graXYname11,");
			strSql.Append("graXYval11=@graXYval11,");
			strSql.Append("graXYres11=@graXYres11,");
			strSql.Append("graXYname12=@graXYname12,");
			strSql.Append("graXYval12=@graXYval12,");
			strSql.Append("graXYres12=@graXYres12,");
			strSql.Append("graXYname13=@graXYname13,");
			strSql.Append("graXYval13=@graXYval13,");
			strSql.Append("graXYres13=@graXYres13,");
			strSql.Append("graXYname14=@graXYname14,");
			strSql.Append("graXYval14=@graXYval14,");
			strSql.Append("graXYres14=@graXYres14,");
			strSql.Append("graXYname15=@graXYname15,");
			strSql.Append("graXYval15=@graXYval15,");
			strSql.Append("graXYres15=@graXYres15,");
			strSql.Append("graXYname16=@graXYname16,");
			strSql.Append("graXYval16=@graXYval16,");
			strSql.Append("graXYres16=@graXYres16,");
			strSql.Append("graXYname17=@graXYname17,");
			strSql.Append("graXYval17=@graXYval17,");
			strSql.Append("graXYres17=@graXYres17,");
			strSql.Append("graXYname18=@graXYname18,");
			strSql.Append("graXYval18=@graXYval18,");
			strSql.Append("graXYres18=@graXYres18,");
			strSql.Append("graXYname19=@graXYname19,");
			strSql.Append("graXYval19=@graXYval19,");
			strSql.Append("graXYres19=@graXYres19,");
			strSql.Append("graXYname20=@graXYname20,");
			strSql.Append("graXYval20=@graXYval20,");
			strSql.Append("graXYres20=@graXYres20,");
			strSql.Append("graXYname21=@graXYname21,");
			strSql.Append("graXYval21=@graXYval21,");
			strSql.Append("graXYres21=@graXYres21,");
			strSql.Append("graXYname22=@graXYname22,");
			strSql.Append("graXYval22=@graXYval22,");
			strSql.Append("graXYres22=@graXYres22,");
			strSql.Append("graXYname23=@graXYname23,");
			strSql.Append("graXYval23=@graXYval23,");
			strSql.Append("graXYres23=@graXYres23,");
			strSql.Append("graXYname24=@graXYname24,");
			strSql.Append("graXYval24=@graXYval24,");
			strSql.Append("graXYres24=@graXYres24,");
			strSql.Append("graXYname25=@graXYname25,");
			strSql.Append("graXYval25=@graXYval25,");
			strSql.Append("graXYres25=@graXYres25,");
			strSql.Append("graXYname26=@graXYname26,");
			strSql.Append("graXYval26=@graXYval26,");
			strSql.Append("graXYres26=@graXYres26,");
			strSql.Append("graXYname27=@graXYname27,");
			strSql.Append("graXYval27=@graXYval27,");
			strSql.Append("graXYres27=@graXYres27,");
			strSql.Append("graXYname28=@graXYname28,");
			strSql.Append("graXYval28=@graXYval28,");
			strSql.Append("graXYres28=@graXYres28,");
			strSql.Append("graXYname29=@graXYname29,");
			strSql.Append("graXYval29=@graXYval29,");
			strSql.Append("graXYres29=@graXYres29,");
			strSql.Append("graXYname30=@graXYname30,");
			strSql.Append("graXYval30=@graXYval30,");
			strSql.Append("graXYres30=@graXYres30,");
			strSql.Append("overallResult=@overallResult,");
			strSql.Append("totalJig=@totalJig");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@dateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@UpdatedTime", SqlDbType.DateTime2,8),
					new SqlParameter("@TransType", SqlDbType.VarChar,50),
					new SqlParameter("@machineID", SqlDbType.VarChar,50),
					new SqlParameter("@partNumber", SqlDbType.VarChar,50),
					new SqlParameter("@jobNumber", SqlDbType.VarChar,50),
					new SqlParameter("@modeStatus", SqlDbType.VarChar,50),
					new SqlParameter("@currentStatus", SqlDbType.VarChar,50),
					new SqlParameter("@totalGraphic", SqlDbType.Int,4),
					new SqlParameter("@currentQuantity", SqlDbType.Int,4),
					new SqlParameter("@totalQuantity", SqlDbType.Int,4),
					new SqlParameter("@mainResult", SqlDbType.VarChar,50),
					new SqlParameter("@vSystemOk", SqlDbType.VarChar,50),
					new SqlParameter("@runningOk", SqlDbType.VarChar,50),
					new SqlParameter("@inspectOk", SqlDbType.VarChar,50),
					new SqlParameter("@passOk", SqlDbType.VarChar,50),
					new SqlParameter("@failOk", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres1", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres2", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres3", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres4", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres5", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres6", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres7", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres8", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres9", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres10", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres11", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres12", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres13", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres14", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres15", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres16", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres17", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres18", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres19", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres20", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres21", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres22", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres23", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres24", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres25", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres26", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres27", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres28", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres29", SqlDbType.VarChar,50),
					new SqlParameter("@graXYname30", SqlDbType.VarChar,50),
					new SqlParameter("@graXYval30", SqlDbType.VarChar,50),
					new SqlParameter("@graXYres30", SqlDbType.VarChar,50),
					new SqlParameter("@overallResult", SqlDbType.VarChar,50),
					new SqlParameter("@totalJig", SqlDbType.Int,4)};
			parameters[0].Value = model.id == null ? (object)DBNull.Value : model.id ;
			parameters[1].Value = model.dateTime == null ? (object)DBNull.Value : model.dateTime ;
			parameters[2].Value = model.UpdatedTime == null ? (object)DBNull.Value : model.UpdatedTime ;
			parameters[3].Value = model.TransType == null ? (object)DBNull.Value : model.TransType ;
			parameters[4].Value = model.machineID == null ? (object)DBNull.Value : model.machineID ;
			parameters[5].Value = model.partNumber == null ? (object)DBNull.Value : model.partNumber ;
			parameters[6].Value = model.jobNumber == null ? (object)DBNull.Value : model.jobNumber ;
			parameters[7].Value = model.modeStatus == null ? (object)DBNull.Value : model.modeStatus ;
			parameters[8].Value = model.currentStatus == null ? (object)DBNull.Value : model.currentStatus ;
			parameters[9].Value = model.totalGraphic == null ? (object)DBNull.Value : model.totalGraphic ;
			parameters[10].Value = model.currentQuantity == null ? (object)DBNull.Value : model.currentQuantity ;
			parameters[11].Value = model.totalQuantity == null ? (object)DBNull.Value : model.totalQuantity ;
			parameters[12].Value = model.mainResult == null ? (object)DBNull.Value : model.mainResult ;
			parameters[13].Value = model.vSystemOk == null ? (object)DBNull.Value : model.vSystemOk ;
			parameters[14].Value = model.runningOk == null ? (object)DBNull.Value : model.runningOk ;
			parameters[15].Value = model.inspectOk == null ? (object)DBNull.Value : model.inspectOk ;
			parameters[16].Value = model.passOk == null ? (object)DBNull.Value : model.passOk ;
			parameters[17].Value = model.failOk == null ? (object)DBNull.Value : model.failOk ;
			parameters[18].Value = model.graXYname1 == null ? (object)DBNull.Value : model.graXYname1 ;
			parameters[19].Value = model.graXYval1 == null ? (object)DBNull.Value : model.graXYval1 ;
			parameters[20].Value = model.graXYres1 == null ? (object)DBNull.Value : model.graXYres1 ;
			parameters[21].Value = model.graXYname2 == null ? (object)DBNull.Value : model.graXYname2 ;
			parameters[22].Value = model.graXYval2 == null ? (object)DBNull.Value : model.graXYval2 ;
			parameters[23].Value = model.graXYres2 == null ? (object)DBNull.Value : model.graXYres2 ;
			parameters[24].Value = model.graXYname3 == null ? (object)DBNull.Value : model.graXYname3 ;
			parameters[25].Value = model.graXYval3 == null ? (object)DBNull.Value : model.graXYval3 ;
			parameters[26].Value = model.graXYres3 == null ? (object)DBNull.Value : model.graXYres3 ;
			parameters[27].Value = model.graXYname4 == null ? (object)DBNull.Value : model.graXYname4 ;
			parameters[28].Value = model.graXYval4 == null ? (object)DBNull.Value : model.graXYval4 ;
			parameters[29].Value = model.graXYres4 == null ? (object)DBNull.Value : model.graXYres4 ;
			parameters[30].Value = model.graXYname5 == null ? (object)DBNull.Value : model.graXYname5 ;
			parameters[31].Value = model.graXYval5 == null ? (object)DBNull.Value : model.graXYval5 ;
			parameters[32].Value = model.graXYres5 == null ? (object)DBNull.Value : model.graXYres5 ;
			parameters[33].Value = model.graXYname6 == null ? (object)DBNull.Value : model.graXYname6 ;
			parameters[34].Value = model.graXYval6 == null ? (object)DBNull.Value : model.graXYval6 ;
			parameters[35].Value = model.graXYres6 == null ? (object)DBNull.Value : model.graXYres6 ;
			parameters[36].Value = model.graXYname7 == null ? (object)DBNull.Value : model.graXYname7 ;
			parameters[37].Value = model.graXYval7 == null ? (object)DBNull.Value : model.graXYval7 ;
			parameters[38].Value = model.graXYres7 == null ? (object)DBNull.Value : model.graXYres7 ;
			parameters[39].Value = model.graXYname8 == null ? (object)DBNull.Value : model.graXYname8 ;
			parameters[40].Value = model.graXYval8 == null ? (object)DBNull.Value : model.graXYval8 ;
			parameters[41].Value = model.graXYres8 == null ? (object)DBNull.Value : model.graXYres8 ;
			parameters[42].Value = model.graXYname9 == null ? (object)DBNull.Value : model.graXYname9 ;
			parameters[43].Value = model.graXYval9 == null ? (object)DBNull.Value : model.graXYval9 ;
			parameters[44].Value = model.graXYres9 == null ? (object)DBNull.Value : model.graXYres9 ;
			parameters[45].Value = model.graXYname10 == null ? (object)DBNull.Value : model.graXYname10 ;
			parameters[46].Value = model.graXYval10 == null ? (object)DBNull.Value : model.graXYval10 ;
			parameters[47].Value = model.graXYres10 == null ? (object)DBNull.Value : model.graXYres10 ;
			parameters[48].Value = model.graXYname11 == null ? (object)DBNull.Value : model.graXYname11 ;
			parameters[49].Value = model.graXYval11 == null ? (object)DBNull.Value : model.graXYval11 ;
			parameters[50].Value = model.graXYres11 == null ? (object)DBNull.Value : model.graXYres11 ;
			parameters[51].Value = model.graXYname12 == null ? (object)DBNull.Value : model.graXYname12 ;
			parameters[52].Value = model.graXYval12 == null ? (object)DBNull.Value : model.graXYval12 ;
			parameters[53].Value = model.graXYres12 == null ? (object)DBNull.Value : model.graXYres12 ;
			parameters[54].Value = model.graXYname13 == null ? (object)DBNull.Value : model.graXYname13 ;
			parameters[55].Value = model.graXYval13 == null ? (object)DBNull.Value : model.graXYval13 ;
			parameters[56].Value = model.graXYres13 == null ? (object)DBNull.Value : model.graXYres13 ;
			parameters[57].Value = model.graXYname14 == null ? (object)DBNull.Value : model.graXYname14 ;
			parameters[58].Value = model.graXYval14 == null ? (object)DBNull.Value : model.graXYval14 ;
			parameters[59].Value = model.graXYres14 == null ? (object)DBNull.Value : model.graXYres14 ;
			parameters[60].Value = model.graXYname15 == null ? (object)DBNull.Value : model.graXYname15 ;
			parameters[61].Value = model.graXYval15 == null ? (object)DBNull.Value : model.graXYval15 ;
			parameters[62].Value = model.graXYres15 == null ? (object)DBNull.Value : model.graXYres15 ;
			parameters[63].Value = model.graXYname16 == null ? (object)DBNull.Value : model.graXYname16 ;
			parameters[64].Value = model.graXYval16 == null ? (object)DBNull.Value : model.graXYval16 ;
			parameters[65].Value = model.graXYres16 == null ? (object)DBNull.Value : model.graXYres16 ;
			parameters[66].Value = model.graXYname17 == null ? (object)DBNull.Value : model.graXYname17 ;
			parameters[67].Value = model.graXYval17 == null ? (object)DBNull.Value : model.graXYval17 ;
			parameters[68].Value = model.graXYres17 == null ? (object)DBNull.Value : model.graXYres17 ;
			parameters[69].Value = model.graXYname18 == null ? (object)DBNull.Value : model.graXYname18 ;
			parameters[70].Value = model.graXYval18 == null ? (object)DBNull.Value : model.graXYval18 ;
			parameters[71].Value = model.graXYres18 == null ? (object)DBNull.Value : model.graXYres18 ;
			parameters[72].Value = model.graXYname19 == null ? (object)DBNull.Value : model.graXYname19 ;
			parameters[73].Value = model.graXYval19 == null ? (object)DBNull.Value : model.graXYval19 ;
			parameters[74].Value = model.graXYres19 == null ? (object)DBNull.Value : model.graXYres19 ;
			parameters[75].Value = model.graXYname20 == null ? (object)DBNull.Value : model.graXYname20 ;
			parameters[76].Value = model.graXYval20 == null ? (object)DBNull.Value : model.graXYval20 ;
			parameters[77].Value = model.graXYres20 == null ? (object)DBNull.Value : model.graXYres20 ;
			parameters[78].Value = model.graXYname21 == null ? (object)DBNull.Value : model.graXYname21 ;
			parameters[79].Value = model.graXYval21 == null ? (object)DBNull.Value : model.graXYval21 ;
			parameters[80].Value = model.graXYres21 == null ? (object)DBNull.Value : model.graXYres21 ;
			parameters[81].Value = model.graXYname22 == null ? (object)DBNull.Value : model.graXYname22 ;
			parameters[82].Value = model.graXYval22 == null ? (object)DBNull.Value : model.graXYval22 ;
			parameters[83].Value = model.graXYres22 == null ? (object)DBNull.Value : model.graXYres22 ;
			parameters[84].Value = model.graXYname23 == null ? (object)DBNull.Value : model.graXYname23 ;
			parameters[85].Value = model.graXYval23 == null ? (object)DBNull.Value : model.graXYval23 ;
			parameters[86].Value = model.graXYres23 == null ? (object)DBNull.Value : model.graXYres23 ;
			parameters[87].Value = model.graXYname24 == null ? (object)DBNull.Value : model.graXYname24 ;
			parameters[88].Value = model.graXYval24 == null ? (object)DBNull.Value : model.graXYval24 ;
			parameters[89].Value = model.graXYres24 == null ? (object)DBNull.Value : model.graXYres24 ;
			parameters[90].Value = model.graXYname25 == null ? (object)DBNull.Value : model.graXYname25 ;
			parameters[91].Value = model.graXYval25 == null ? (object)DBNull.Value : model.graXYval25 ;
			parameters[92].Value = model.graXYres25 == null ? (object)DBNull.Value : model.graXYres25 ;
			parameters[93].Value = model.graXYname26 == null ? (object)DBNull.Value : model.graXYname26 ;
			parameters[94].Value = model.graXYval26 == null ? (object)DBNull.Value : model.graXYval26 ;
			parameters[95].Value = model.graXYres26 == null ? (object)DBNull.Value : model.graXYres26 ;
			parameters[96].Value = model.graXYname27 == null ? (object)DBNull.Value : model.graXYname27 ;
			parameters[97].Value = model.graXYval27 == null ? (object)DBNull.Value : model.graXYval27 ;
			parameters[98].Value = model.graXYres27 == null ? (object)DBNull.Value : model.graXYres27 ;
			parameters[99].Value = model.graXYname28 == null ? (object)DBNull.Value : model.graXYname28 ;
			parameters[100].Value = model.graXYval28 == null ? (object)DBNull.Value : model.graXYval28 ;
			parameters[101].Value = model.graXYres28 == null ? (object)DBNull.Value : model.graXYres28 ;
			parameters[102].Value = model.graXYname29 == null ? (object)DBNull.Value : model.graXYname29 ;
			parameters[103].Value = model.graXYval29 == null ? (object)DBNull.Value : model.graXYval29 ;
			parameters[104].Value = model.graXYres29 == null ? (object)DBNull.Value : model.graXYres29 ;
			parameters[105].Value = model.graXYname30 == null ? (object)DBNull.Value : model.graXYname30 ;
			parameters[106].Value = model.graXYval30 == null ? (object)DBNull.Value : model.graXYval30 ;
			parameters[107].Value = model.graXYres30 == null ? (object)DBNull.Value : model.graXYres30 ;
			parameters[108].Value = model.overallResult == null ? (object)DBNull.Value : model.overallResult ;
			parameters[109].Value = model.totalJig == null ? (object)DBNull.Value : model.totalJig ;

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSClientVisionLog_His_DAL" , "Function:		public SqlCommand UpdateCommand(Common.Model.LMMSClientVisionLog_His_Model model)"  + "TableName:LMMSClientVisionLog_His" , ";id = "+ (model.id == null ? "" : model.id.ToString()) + ";dateTime = "+ (model.dateTime == null ? "" : model.dateTime.ToString()) + ";UpdatedTime = "+ (model.UpdatedTime == null ? "" : model.UpdatedTime.ToString()) + ";TransType = "+ (model.TransType == null ? "" : model.TransType.ToString()) + ";machineID = "+ (model.machineID == null ? "" : model.machineID.ToString()) + ";partNumber = "+ (model.partNumber == null ? "" : model.partNumber.ToString()) + ";jobNumber = "+ (model.jobNumber == null ? "" : model.jobNumber.ToString()) + ";modeStatus = "+ (model.modeStatus == null ? "" : model.modeStatus.ToString()) + ";currentStatus = "+ (model.currentStatus == null ? "" : model.currentStatus.ToString()) + ";totalGraphic = "+ (model.totalGraphic == null ? "" : model.totalGraphic.ToString()) + ";currentQuantity = "+ (model.currentQuantity == null ? "" : model.currentQuantity.ToString()) + ";totalQuantity = "+ (model.totalQuantity == null ? "" : model.totalQuantity.ToString()) + ";mainResult = "+ (model.mainResult == null ? "" : model.mainResult.ToString()) + ";vSystemOk = "+ (model.vSystemOk == null ? "" : model.vSystemOk.ToString()) + ";runningOk = "+ (model.runningOk == null ? "" : model.runningOk.ToString()) + ";inspectOk = "+ (model.inspectOk == null ? "" : model.inspectOk.ToString()) + ";passOk = "+ (model.passOk == null ? "" : model.passOk.ToString()) + ";failOk = "+ (model.failOk == null ? "" : model.failOk.ToString()) + ";graXYname1 = "+ (model.graXYname1 == null ? "" : model.graXYname1.ToString()) + ";graXYval1 = "+ (model.graXYval1 == null ? "" : model.graXYval1.ToString()) + ";graXYres1 = "+ (model.graXYres1 == null ? "" : model.graXYres1.ToString()) + ";graXYname2 = "+ (model.graXYname2 == null ? "" : model.graXYname2.ToString()) + ";graXYval2 = "+ (model.graXYval2 == null ? "" : model.graXYval2.ToString()) + ";graXYres2 = "+ (model.graXYres2 == null ? "" : model.graXYres2.ToString()) + ";graXYname3 = "+ (model.graXYname3 == null ? "" : model.graXYname3.ToString()) + ";graXYval3 = "+ (model.graXYval3 == null ? "" : model.graXYval3.ToString()) + ";graXYres3 = "+ (model.graXYres3 == null ? "" : model.graXYres3.ToString()) + ";graXYname4 = "+ (model.graXYname4 == null ? "" : model.graXYname4.ToString()) + ";graXYval4 = "+ (model.graXYval4 == null ? "" : model.graXYval4.ToString()) + ";graXYres4 = "+ (model.graXYres4 == null ? "" : model.graXYres4.ToString()) + ";graXYname5 = "+ (model.graXYname5 == null ? "" : model.graXYname5.ToString()) + ";graXYval5 = "+ (model.graXYval5 == null ? "" : model.graXYval5.ToString()) + ";graXYres5 = "+ (model.graXYres5 == null ? "" : model.graXYres5.ToString()) + ";graXYname6 = "+ (model.graXYname6 == null ? "" : model.graXYname6.ToString()) + ";graXYval6 = "+ (model.graXYval6 == null ? "" : model.graXYval6.ToString()) + ";graXYres6 = "+ (model.graXYres6 == null ? "" : model.graXYres6.ToString()) + ";graXYname7 = "+ (model.graXYname7 == null ? "" : model.graXYname7.ToString()) + ";graXYval7 = "+ (model.graXYval7 == null ? "" : model.graXYval7.ToString()) + ";graXYres7 = "+ (model.graXYres7 == null ? "" : model.graXYres7.ToString()) + ";graXYname8 = "+ (model.graXYname8 == null ? "" : model.graXYname8.ToString()) + ";graXYval8 = "+ (model.graXYval8 == null ? "" : model.graXYval8.ToString()) + ";graXYres8 = "+ (model.graXYres8 == null ? "" : model.graXYres8.ToString()) + ";graXYname9 = "+ (model.graXYname9 == null ? "" : model.graXYname9.ToString()) + ";graXYval9 = "+ (model.graXYval9 == null ? "" : model.graXYval9.ToString()) + ";graXYres9 = "+ (model.graXYres9 == null ? "" : model.graXYres9.ToString()) + ";graXYname10 = "+ (model.graXYname10 == null ? "" : model.graXYname10.ToString()) + ";graXYval10 = "+ (model.graXYval10 == null ? "" : model.graXYval10.ToString()) + ";graXYres10 = "+ (model.graXYres10 == null ? "" : model.graXYres10.ToString()) + ";graXYname11 = "+ (model.graXYname11 == null ? "" : model.graXYname11.ToString()) + ";graXYval11 = "+ (model.graXYval11 == null ? "" : model.graXYval11.ToString()) + ";graXYres11 = "+ (model.graXYres11 == null ? "" : model.graXYres11.ToString()) + ";graXYname12 = "+ (model.graXYname12 == null ? "" : model.graXYname12.ToString()) + ";graXYval12 = "+ (model.graXYval12 == null ? "" : model.graXYval12.ToString()) + ";graXYres12 = "+ (model.graXYres12 == null ? "" : model.graXYres12.ToString()) + ";graXYname13 = "+ (model.graXYname13 == null ? "" : model.graXYname13.ToString()) + ";graXYval13 = "+ (model.graXYval13 == null ? "" : model.graXYval13.ToString()) + ";graXYres13 = "+ (model.graXYres13 == null ? "" : model.graXYres13.ToString()) + ";graXYname14 = "+ (model.graXYname14 == null ? "" : model.graXYname14.ToString()) + ";graXYval14 = "+ (model.graXYval14 == null ? "" : model.graXYval14.ToString()) + ";graXYres14 = "+ (model.graXYres14 == null ? "" : model.graXYres14.ToString()) + ";graXYname15 = "+ (model.graXYname15 == null ? "" : model.graXYname15.ToString()) + ";graXYval15 = "+ (model.graXYval15 == null ? "" : model.graXYval15.ToString()) + ";graXYres15 = "+ (model.graXYres15 == null ? "" : model.graXYres15.ToString()) + ";graXYname16 = "+ (model.graXYname16 == null ? "" : model.graXYname16.ToString()) + ";graXYval16 = "+ (model.graXYval16 == null ? "" : model.graXYval16.ToString()) + ";graXYres16 = "+ (model.graXYres16 == null ? "" : model.graXYres16.ToString()) + ";graXYname17 = "+ (model.graXYname17 == null ? "" : model.graXYname17.ToString()) + ";graXYval17 = "+ (model.graXYval17 == null ? "" : model.graXYval17.ToString()) + ";graXYres17 = "+ (model.graXYres17 == null ? "" : model.graXYres17.ToString()) + ";graXYname18 = "+ (model.graXYname18 == null ? "" : model.graXYname18.ToString()) + ";graXYval18 = "+ (model.graXYval18 == null ? "" : model.graXYval18.ToString()) + ";graXYres18 = "+ (model.graXYres18 == null ? "" : model.graXYres18.ToString()) + ";graXYname19 = "+ (model.graXYname19 == null ? "" : model.graXYname19.ToString()) + ";graXYval19 = "+ (model.graXYval19 == null ? "" : model.graXYval19.ToString()) + ";graXYres19 = "+ (model.graXYres19 == null ? "" : model.graXYres19.ToString()) + ";graXYname20 = "+ (model.graXYname20 == null ? "" : model.graXYname20.ToString()) + ";graXYval20 = "+ (model.graXYval20 == null ? "" : model.graXYval20.ToString()) + ";graXYres20 = "+ (model.graXYres20 == null ? "" : model.graXYres20.ToString()) + ";graXYname21 = "+ (model.graXYname21 == null ? "" : model.graXYname21.ToString()) + ";graXYval21 = "+ (model.graXYval21 == null ? "" : model.graXYval21.ToString()) + ";graXYres21 = "+ (model.graXYres21 == null ? "" : model.graXYres21.ToString()) + ";graXYname22 = "+ (model.graXYname22 == null ? "" : model.graXYname22.ToString()) + ";graXYval22 = "+ (model.graXYval22 == null ? "" : model.graXYval22.ToString()) + ";graXYres22 = "+ (model.graXYres22 == null ? "" : model.graXYres22.ToString()) + ";graXYname23 = "+ (model.graXYname23 == null ? "" : model.graXYname23.ToString()) + ";graXYval23 = "+ (model.graXYval23 == null ? "" : model.graXYval23.ToString()) + ";graXYres23 = "+ (model.graXYres23 == null ? "" : model.graXYres23.ToString()) + ";graXYname24 = "+ (model.graXYname24 == null ? "" : model.graXYname24.ToString()) + ";graXYval24 = "+ (model.graXYval24 == null ? "" : model.graXYval24.ToString()) + ";graXYres24 = "+ (model.graXYres24 == null ? "" : model.graXYres24.ToString()) + ";graXYname25 = "+ (model.graXYname25 == null ? "" : model.graXYname25.ToString()) + ";graXYval25 = "+ (model.graXYval25 == null ? "" : model.graXYval25.ToString()) + ";graXYres25 = "+ (model.graXYres25 == null ? "" : model.graXYres25.ToString()) + ";graXYname26 = "+ (model.graXYname26 == null ? "" : model.graXYname26.ToString()) + ";graXYval26 = "+ (model.graXYval26 == null ? "" : model.graXYval26.ToString()) + ";graXYres26 = "+ (model.graXYres26 == null ? "" : model.graXYres26.ToString()) + ";graXYname27 = "+ (model.graXYname27 == null ? "" : model.graXYname27.ToString()) + ";graXYval27 = "+ (model.graXYval27 == null ? "" : model.graXYval27.ToString()) + ";graXYres27 = "+ (model.graXYres27 == null ? "" : model.graXYres27.ToString()) + ";graXYname28 = "+ (model.graXYname28 == null ? "" : model.graXYname28.ToString()) + ";graXYval28 = "+ (model.graXYval28 == null ? "" : model.graXYval28.ToString()) + ";graXYres28 = "+ (model.graXYres28 == null ? "" : model.graXYres28.ToString()) + ";graXYname29 = "+ (model.graXYname29 == null ? "" : model.graXYname29.ToString()) + ";graXYval29 = "+ (model.graXYval29 == null ? "" : model.graXYval29.ToString()) + ";graXYres29 = "+ (model.graXYres29 == null ? "" : model.graXYres29.ToString()) + ";graXYname30 = "+ (model.graXYname30 == null ? "" : model.graXYname30.ToString()) + ";graXYval30 = "+ (model.graXYval30 == null ? "" : model.graXYval30.ToString()) + ";graXYres30 = "+ (model.graXYres30 == null ? "" : model.graXYres30.ToString()) + ";overallResult = "+ (model.overallResult == null ? "" : model.overallResult.ToString()) + ";totalJig = "+ (model.totalJig == null ? "" : model.totalJig.ToString()) + "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSClientVisionLog_His ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSClientVisionLog_His_DAL" , "Function:		public bool Delete()"  + "TableName:LMMSClientVisionLog_His" , "");
			int rows=DBHelp.SqlDB.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteCommand()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSClientVisionLog_His ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSClientVisionLog_His_DAL" , "Function:		public SqlCommand DeleteCommand()"  + "TableName:LMMSClientVisionLog_His" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public SqlCommand DeleteAllCommand( )
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LMMSClientVisionLog_His ");
			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSClientVisionLog_His_DAL" , "Function:		public SqlCommand DeleteAllCommand( )"  + "TableName:LMMSClientVisionLog_His" , "");
			return DBHelp.SqlDB.generateCommand(strSql.ToString(), new SqlParameter[0]);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Common.Model.LMMSClientVisionLog_His_Model GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,dateTime,UpdatedTime,TransType,machineID,partNumber,jobNumber,modeStatus,currentStatus,totalGraphic,currentQuantity,totalQuantity,mainResult,vSystemOk,runningOk,inspectOk,passOk,failOk,graXYname1,graXYval1,graXYres1,graXYname2,graXYval2,graXYres2,graXYname3,graXYval3,graXYres3,graXYname4,graXYval4,graXYres4,graXYname5,graXYval5,graXYres5,graXYname6,graXYval6,graXYres6,graXYname7,graXYval7,graXYres7,graXYname8,graXYval8,graXYres8,graXYname9,graXYval9,graXYres9,graXYname10,graXYval10,graXYres10,graXYname11,graXYval11,graXYres11,graXYname12,graXYval12,graXYres12,graXYname13,graXYval13,graXYres13,graXYname14,graXYval14,graXYres14,graXYname15,graXYval15,graXYres15,graXYname16,graXYval16,graXYres16,graXYname17,graXYval17,graXYres17,graXYname18,graXYval18,graXYres18,graXYname19,graXYval19,graXYres19,graXYname20,graXYval20,graXYres20,graXYname21,graXYval21,graXYres21,graXYname22,graXYval22,graXYres22,graXYname23,graXYval23,graXYres23,graXYname24,graXYval24,graXYres24,graXYname25,graXYval25,graXYres25,graXYname26,graXYval26,graXYres26,graXYname27,graXYval27,graXYres27,graXYname28,graXYval28,graXYres28,graXYname29,graXYval29,graXYres29,graXYname30,graXYval30,graXYres30,overallResult,totalJig from LMMSClientVisionLog_His ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			 DBHelp.Reports.LogFile.DebugLog("AUTOCODE","NameSpace:Common.DAL" , "Class:LMMSClientVisionLog_His_DAL" , "Function:		public Common.Model.LMMSClientVisionLog_His_Model GetModel()"  + "TableName:LMMSClientVisionLog_His" , "");
			Common.Model.LMMSClientVisionLog_His_Model model=new Common.Model.LMMSClientVisionLog_His_Model();
			DataSet ds=DBHelp.SqlDB.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["dateTime"].ToString()!="")
				{
					model.dateTime=DateTime.Parse(ds.Tables[0].Rows[0]["dateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdatedTime"].ToString()!="")
				{
					model.UpdatedTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedTime"].ToString());
				}
				model.TransType=ds.Tables[0].Rows[0]["TransType"].ToString();
				model.machineID=ds.Tables[0].Rows[0]["machineID"].ToString();
				model.partNumber=ds.Tables[0].Rows[0]["partNumber"].ToString();
				model.jobNumber=ds.Tables[0].Rows[0]["jobNumber"].ToString();
				model.modeStatus=ds.Tables[0].Rows[0]["modeStatus"].ToString();
				model.currentStatus=ds.Tables[0].Rows[0]["currentStatus"].ToString();
				if(ds.Tables[0].Rows[0]["totalGraphic"].ToString()!="")
				{
					model.totalGraphic=int.Parse(ds.Tables[0].Rows[0]["totalGraphic"].ToString());
				}
				if(ds.Tables[0].Rows[0]["currentQuantity"].ToString()!="")
				{
					model.currentQuantity=int.Parse(ds.Tables[0].Rows[0]["currentQuantity"].ToString());
				}
				if(ds.Tables[0].Rows[0]["totalQuantity"].ToString()!="")
				{
					model.totalQuantity=int.Parse(ds.Tables[0].Rows[0]["totalQuantity"].ToString());
				}
				model.mainResult=ds.Tables[0].Rows[0]["mainResult"].ToString();
				model.vSystemOk=ds.Tables[0].Rows[0]["vSystemOk"].ToString();
				model.runningOk=ds.Tables[0].Rows[0]["runningOk"].ToString();
				model.inspectOk=ds.Tables[0].Rows[0]["inspectOk"].ToString();
				model.passOk=ds.Tables[0].Rows[0]["passOk"].ToString();
				model.failOk=ds.Tables[0].Rows[0]["failOk"].ToString();
				model.graXYname1=ds.Tables[0].Rows[0]["graXYname1"].ToString();
				model.graXYval1=ds.Tables[0].Rows[0]["graXYval1"].ToString();
				model.graXYres1=ds.Tables[0].Rows[0]["graXYres1"].ToString();
				model.graXYname2=ds.Tables[0].Rows[0]["graXYname2"].ToString();
				model.graXYval2=ds.Tables[0].Rows[0]["graXYval2"].ToString();
				model.graXYres2=ds.Tables[0].Rows[0]["graXYres2"].ToString();
				model.graXYname3=ds.Tables[0].Rows[0]["graXYname3"].ToString();
				model.graXYval3=ds.Tables[0].Rows[0]["graXYval3"].ToString();
				model.graXYres3=ds.Tables[0].Rows[0]["graXYres3"].ToString();
				model.graXYname4=ds.Tables[0].Rows[0]["graXYname4"].ToString();
				model.graXYval4=ds.Tables[0].Rows[0]["graXYval4"].ToString();
				model.graXYres4=ds.Tables[0].Rows[0]["graXYres4"].ToString();
				model.graXYname5=ds.Tables[0].Rows[0]["graXYname5"].ToString();
				model.graXYval5=ds.Tables[0].Rows[0]["graXYval5"].ToString();
				model.graXYres5=ds.Tables[0].Rows[0]["graXYres5"].ToString();
				model.graXYname6=ds.Tables[0].Rows[0]["graXYname6"].ToString();
				model.graXYval6=ds.Tables[0].Rows[0]["graXYval6"].ToString();
				model.graXYres6=ds.Tables[0].Rows[0]["graXYres6"].ToString();
				model.graXYname7=ds.Tables[0].Rows[0]["graXYname7"].ToString();
				model.graXYval7=ds.Tables[0].Rows[0]["graXYval7"].ToString();
				model.graXYres7=ds.Tables[0].Rows[0]["graXYres7"].ToString();
				model.graXYname8=ds.Tables[0].Rows[0]["graXYname8"].ToString();
				model.graXYval8=ds.Tables[0].Rows[0]["graXYval8"].ToString();
				model.graXYres8=ds.Tables[0].Rows[0]["graXYres8"].ToString();
				model.graXYname9=ds.Tables[0].Rows[0]["graXYname9"].ToString();
				model.graXYval9=ds.Tables[0].Rows[0]["graXYval9"].ToString();
				model.graXYres9=ds.Tables[0].Rows[0]["graXYres9"].ToString();
				model.graXYname10=ds.Tables[0].Rows[0]["graXYname10"].ToString();
				model.graXYval10=ds.Tables[0].Rows[0]["graXYval10"].ToString();
				model.graXYres10=ds.Tables[0].Rows[0]["graXYres10"].ToString();
				model.graXYname11=ds.Tables[0].Rows[0]["graXYname11"].ToString();
				model.graXYval11=ds.Tables[0].Rows[0]["graXYval11"].ToString();
				model.graXYres11=ds.Tables[0].Rows[0]["graXYres11"].ToString();
				model.graXYname12=ds.Tables[0].Rows[0]["graXYname12"].ToString();
				model.graXYval12=ds.Tables[0].Rows[0]["graXYval12"].ToString();
				model.graXYres12=ds.Tables[0].Rows[0]["graXYres12"].ToString();
				model.graXYname13=ds.Tables[0].Rows[0]["graXYname13"].ToString();
				model.graXYval13=ds.Tables[0].Rows[0]["graXYval13"].ToString();
				model.graXYres13=ds.Tables[0].Rows[0]["graXYres13"].ToString();
				model.graXYname14=ds.Tables[0].Rows[0]["graXYname14"].ToString();
				model.graXYval14=ds.Tables[0].Rows[0]["graXYval14"].ToString();
				model.graXYres14=ds.Tables[0].Rows[0]["graXYres14"].ToString();
				model.graXYname15=ds.Tables[0].Rows[0]["graXYname15"].ToString();
				model.graXYval15=ds.Tables[0].Rows[0]["graXYval15"].ToString();
				model.graXYres15=ds.Tables[0].Rows[0]["graXYres15"].ToString();
				model.graXYname16=ds.Tables[0].Rows[0]["graXYname16"].ToString();
				model.graXYval16=ds.Tables[0].Rows[0]["graXYval16"].ToString();
				model.graXYres16=ds.Tables[0].Rows[0]["graXYres16"].ToString();
				model.graXYname17=ds.Tables[0].Rows[0]["graXYname17"].ToString();
				model.graXYval17=ds.Tables[0].Rows[0]["graXYval17"].ToString();
				model.graXYres17=ds.Tables[0].Rows[0]["graXYres17"].ToString();
				model.graXYname18=ds.Tables[0].Rows[0]["graXYname18"].ToString();
				model.graXYval18=ds.Tables[0].Rows[0]["graXYval18"].ToString();
				model.graXYres18=ds.Tables[0].Rows[0]["graXYres18"].ToString();
				model.graXYname19=ds.Tables[0].Rows[0]["graXYname19"].ToString();
				model.graXYval19=ds.Tables[0].Rows[0]["graXYval19"].ToString();
				model.graXYres19=ds.Tables[0].Rows[0]["graXYres19"].ToString();
				model.graXYname20=ds.Tables[0].Rows[0]["graXYname20"].ToString();
				model.graXYval20=ds.Tables[0].Rows[0]["graXYval20"].ToString();
				model.graXYres20=ds.Tables[0].Rows[0]["graXYres20"].ToString();
				model.graXYname21=ds.Tables[0].Rows[0]["graXYname21"].ToString();
				model.graXYval21=ds.Tables[0].Rows[0]["graXYval21"].ToString();
				model.graXYres21=ds.Tables[0].Rows[0]["graXYres21"].ToString();
				model.graXYname22=ds.Tables[0].Rows[0]["graXYname22"].ToString();
				model.graXYval22=ds.Tables[0].Rows[0]["graXYval22"].ToString();
				model.graXYres22=ds.Tables[0].Rows[0]["graXYres22"].ToString();
				model.graXYname23=ds.Tables[0].Rows[0]["graXYname23"].ToString();
				model.graXYval23=ds.Tables[0].Rows[0]["graXYval23"].ToString();
				model.graXYres23=ds.Tables[0].Rows[0]["graXYres23"].ToString();
				model.graXYname24=ds.Tables[0].Rows[0]["graXYname24"].ToString();
				model.graXYval24=ds.Tables[0].Rows[0]["graXYval24"].ToString();
				model.graXYres24=ds.Tables[0].Rows[0]["graXYres24"].ToString();
				model.graXYname25=ds.Tables[0].Rows[0]["graXYname25"].ToString();
				model.graXYval25=ds.Tables[0].Rows[0]["graXYval25"].ToString();
				model.graXYres25=ds.Tables[0].Rows[0]["graXYres25"].ToString();
				model.graXYname26=ds.Tables[0].Rows[0]["graXYname26"].ToString();
				model.graXYval26=ds.Tables[0].Rows[0]["graXYval26"].ToString();
				model.graXYres26=ds.Tables[0].Rows[0]["graXYres26"].ToString();
				model.graXYname27=ds.Tables[0].Rows[0]["graXYname27"].ToString();
				model.graXYval27=ds.Tables[0].Rows[0]["graXYval27"].ToString();
				model.graXYres27=ds.Tables[0].Rows[0]["graXYres27"].ToString();
				model.graXYname28=ds.Tables[0].Rows[0]["graXYname28"].ToString();
				model.graXYval28=ds.Tables[0].Rows[0]["graXYval28"].ToString();
				model.graXYres28=ds.Tables[0].Rows[0]["graXYres28"].ToString();
				model.graXYname29=ds.Tables[0].Rows[0]["graXYname29"].ToString();
				model.graXYval29=ds.Tables[0].Rows[0]["graXYval29"].ToString();
				model.graXYres29=ds.Tables[0].Rows[0]["graXYres29"].ToString();
				model.graXYname30=ds.Tables[0].Rows[0]["graXYname30"].ToString();
				model.graXYval30=ds.Tables[0].Rows[0]["graXYval30"].ToString();
				model.graXYres30=ds.Tables[0].Rows[0]["graXYres30"].ToString();
				model.overallResult=ds.Tables[0].Rows[0]["overallResult"].ToString();
				if(ds.Tables[0].Rows[0]["totalJig"].ToString()!="")
				{
					model.totalJig=int.Parse(ds.Tables[0].Rows[0]["totalJig"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,dateTime,UpdatedTime,TransType,machineID,partNumber,jobNumber,modeStatus,currentStatus,totalGraphic,currentQuantity,totalQuantity,mainResult,vSystemOk,runningOk,inspectOk,passOk,failOk,graXYname1,graXYval1,graXYres1,graXYname2,graXYval2,graXYres2,graXYname3,graXYval3,graXYres3,graXYname4,graXYval4,graXYres4,graXYname5,graXYval5,graXYres5,graXYname6,graXYval6,graXYres6,graXYname7,graXYval7,graXYres7,graXYname8,graXYval8,graXYres8,graXYname9,graXYval9,graXYres9,graXYname10,graXYval10,graXYres10,graXYname11,graXYval11,graXYres11,graXYname12,graXYval12,graXYres12,graXYname13,graXYval13,graXYres13,graXYname14,graXYval14,graXYres14,graXYname15,graXYval15,graXYres15,graXYname16,graXYval16,graXYres16,graXYname17,graXYval17,graXYres17,graXYname18,graXYval18,graXYres18,graXYname19,graXYval19,graXYres19,graXYname20,graXYval20,graXYres20,graXYname21,graXYval21,graXYres21,graXYname22,graXYval22,graXYres22,graXYname23,graXYval23,graXYres23,graXYname24,graXYval24,graXYres24,graXYname25,graXYval25,graXYres25,graXYname26,graXYval26,graXYres26,graXYname27,graXYval27,graXYres27,graXYname28,graXYval28,graXYres28,graXYname29,graXYval29,graXYres29,graXYname30,graXYval30,graXYres30,overallResult,totalJig ");
			strSql.Append(" FROM LMMSClientVisionLog_His ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,dateTime,UpdatedTime,TransType,machineID,partNumber,jobNumber,modeStatus,currentStatus,totalGraphic,currentQuantity,totalQuantity,mainResult,vSystemOk,runningOk,inspectOk,passOk,failOk,graXYname1,graXYval1,graXYres1,graXYname2,graXYval2,graXYres2,graXYname3,graXYval3,graXYres3,graXYname4,graXYval4,graXYres4,graXYname5,graXYval5,graXYres5,graXYname6,graXYval6,graXYres6,graXYname7,graXYval7,graXYres7,graXYname8,graXYval8,graXYres8,graXYname9,graXYval9,graXYres9,graXYname10,graXYval10,graXYres10,graXYname11,graXYval11,graXYres11,graXYname12,graXYval12,graXYres12,graXYname13,graXYval13,graXYres13,graXYname14,graXYval14,graXYres14,graXYname15,graXYval15,graXYres15,graXYname16,graXYval16,graXYres16,graXYname17,graXYval17,graXYres17,graXYname18,graXYval18,graXYres18,graXYname19,graXYval19,graXYres19,graXYname20,graXYval20,graXYres20,graXYname21,graXYval21,graXYres21,graXYname22,graXYval22,graXYres22,graXYname23,graXYval23,graXYres23,graXYname24,graXYval24,graXYres24,graXYname25,graXYval25,graXYres25,graXYname26,graXYval26,graXYres26,graXYname27,graXYval27,graXYres27,graXYname28,graXYval28,graXYres28,graXYname29,graXYval29,graXYres29,graXYname30,graXYval30,graXYres30,overallResult,totalJig ");
			strSql.Append(" FROM LMMSClientVisionLog_His ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DBHelp.SqlDB.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "LMMSClientVisionLog_His";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DBHelp.SqlDB.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

