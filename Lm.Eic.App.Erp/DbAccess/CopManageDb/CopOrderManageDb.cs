﻿using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.App.Erp.Domain.CopManageModel;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System.Collections.Generic;
using System;
using System.Data;
using System.Text;


namespace Lm.Eic.App.Erp.DbAccess.CopManageDb
{
    /// <summary>
    /// 销售订单管理Crud工厂
    /// </summary>
    public  class CopOrderCrudFactory
    {
        /// <summary>
        /// 销售订单Crud
        /// </summary>
        public static CopOrderManageDb CopOrderManageDb
        {
            get { return OBulider.BuildInstance<CopOrderManageDb>(); }
        }
    }


    public class CopOrderManageDb
    {
        private string SqlFields
        {
            get { return "SELECT TD001 AS 单别, TD002 AS 单号, TD003 AS 序号, TD004 AS 品号, TD005 AS 品名, TD006 AS 规格, TD007 AS 仓位号,   TD008 AS 计划产量, TD009 AS 已交量  FROM  COPTD"; }
        }
        /// <summary>
        /// MES产品型号
        /// </summary>
        /// <returns></returns>
        public  List<string> MesProductTypeList()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT  DISTINCT ProductTypeCommon  ")
                  .Append("FROM    Para_ProductType ")
                  .Append("WHERE  (TypeVisible = '1') AND (MaterialId IS NOT NULL) ");
                DataTable dt = DbHelper.Mes.LoadTable(sb.ToString());
                List<string> ProductTypeList = new List<string>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductTypeList.Add(dr[0].ToString());
                    }
                }
                return ProductTypeList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
           
        }
        /// <summary>
        /// 未完工的业务订单
        /// </summary>
        /// <param name="containsProductType">所包括品名</param>
        /// <returns></returns>
        public List<CopOrderModel> GetCopOrderBy(string containsProductType)
        {
            string sqlWhere = string.Format(" where (TD005 like'%{0}%' or TD006 LIKE '%{0}%')and (TD016 = 'N') ", containsProductType);
            return ErpDbAccessHelper.FindDataBy<CopOrderModel>(SqlFields, sqlWhere, (dr, m) =>
            {
              
                m.OrderId = string.Format("{0}-{1}", dr["单别"].ToString().Trim(), dr["单号"].ToString().Trim()); ;
                m.OrderDesc = dr["序号"].ToString().Trim();

                m.ProductID = dr["品号"].ToString().Trim();
                m.ProductName = containsProductType;
                m.ProductSpecify = dr["规格"].ToString().Trim();
                m.WarehouseID = (dr["仓位号"].ToString().Trim());

                m.ProductNumber = dr["计划产量"].ToString().Trim().ToDouble();
                m.FinishNumber = dr["已交量"].ToString().Trim().ToDouble();
            });


        }
    }
}