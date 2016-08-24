﻿
using Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Bussiness.MocManage
{
    /// <summary>
    /// 工单管理
    /// </summary>
    public class OrderManage
    {
        public OrderManage() { }
        /// <summary>
        /// 初始化一个工单
        /// </summary>
        /// <param name="orderId"></param>
        public OrderManage(string orderId)
        {
            this._orderId = orderId;
        }
        /// <summary>
        /// 全局变量 OrderID
        /// </summary>
        string _orderId = string.Empty;
        /// <summary>
        /// 设置工单单号
        /// </summary>
        /// <param name="orderId"></param>
        public void SetOrderId(string orderId)
        {
            _orderId = orderId;
        }
        /// <summary>
        /// 获取工单详情
        /// </summary>
        /// <returns></returns>
        public OrderModel GetOrderDetails()
        {
            return OrderCrudFactory.OrderDetailsDb.GetOrderDetailsBy(_orderId);
        }
        /// <summary>
        /// 获取工单物料列表
        /// </summary>
        /// <returns></returns>
        public List<MaterialModel> GetOrderMaterialList()
        {
            return OrderCrudFactory.OrderMaterialDb.GetOrderMaterialListBy(_orderId);
        }
    }
}