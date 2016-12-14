﻿
using System.Collections.Generic;
using System.Linq;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
    /// <summary>
    /// 供应商辅导\计划管理
    /// </summary>
    public class SupplierTutorManger
    {
        /// <summary>
        /// 得到考核分数低80分的供应商信息
        /// </summary>
        /// <param name="seasonDateNum"></param>
        /// <returns></returns>
        public List<SupplierSeasonTutorModel> GetWaittingTourSupplier(string seasonDateNum)
        {
            List<SupplierSeasonTutorModel> waittingTourSupplierList = new List<SupplierSeasonTutorModel>();
            //得到低于80分的所以供应商
            var auditModelLsit = SupplierCrudFactory.SuppliersSeasonAuditCrud.GetlimitScoreSupplierAuditInfo(seasonDateNum, 80);
            if (auditModelLsit != null && auditModelLsit.Count > 0)
            {
             auditModelLsit.ForEach(m =>
                {
                    if (SupplierCrudFactory.SuppliersSeasonTutorCrud.IsExist(m.ParameterKey))
                    {
                      waittingTourSupplierList.Add(SupplierCrudFactory.SuppliersSeasonTutorCrud.GetSupplierSeasonTutorModelBy(m.ParameterKey));
                    }
                    else
                    {
                        waittingTourSupplierList.Add(GetlimitScoreSupplierTutorModelTo(m));
                    }
                });
            }
            return waittingTourSupplierList;
        }

        /// <summary>
        /// 保存供应商辅导\计划管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult SaveSupplierTutorModel(SupplierSeasonTutorModel model)
        {
            if (SupplierCrudFactory.SuppliersSeasonTutorCrud.IsExist(model.ParameterKey))
                model.OpSign = "edit";
            else model.OpSign = "add";
            return SupplierCrudFactory.SuppliersSeasonTutorCrud.Store(model);
        }
        #region  Internet
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        SupplierSeasonTutorModel GetlimitScoreSupplierTutorModelTo(SupplierSeasonAuditModel m)
        {
            SupplierSeasonTutorModel model = null;
            if (m != null)
            {
                model = supplierSeasonAuditModelTo(m);
                model.QualityCheckProperty = "考核低于80";

            }
            return model;
        }

        SupplierSeasonTutorModel supplierSeasonAuditModelTo(SupplierSeasonAuditModel m)
        {
            try
            {
                return new SupplierSeasonTutorModel
                {
                    //SupplierId, SupplierShortName, SupplierName, QualityCheck, AuditPrice, DeliveryDate, ActionLiven,
                    //HSFGrade, TotalCheckScore, CheckLevel, RewardsWay, MaterialGrade, ManagerRisk, SubstitutionSupplierId,
                    //SeasonDateNum, ParameterKey,
                    SupplierId = m.SupplierId,
                    SupplierName = m.SupplierName,
                    SuppilerShortName = m.SupplierShortName,
                    QualityCheck = m.QualityCheck,
                    AuditPrice = m.AuditPrice,
                    DeliveryDate = m.DeliveryDate,
                    ActionLiven = m.ActionLiven,
                    HSFGrade = m.HSFGrade,
                    TotalCheckScore = m.TotalCheckScore,
                    CheckLevel = m.CheckLevel,
                    RewardsWay = m.RewardsWay,
                    ManagerRisk = m.ManagerRisk,
                    MaterialGrade = m.MaterialGrade,
                    SeasonNum = m.SeasonDateNum,
                    ParameterKey = m.ParameterKey
                };
            }
            catch (System.Exception)
            {

                throw;
            }
          
        }
        #endregion
    }


}