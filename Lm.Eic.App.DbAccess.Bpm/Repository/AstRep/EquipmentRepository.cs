﻿using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeDbHandler;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.AstRep
{
    public interface IEquipmentRepository : IRepository<EquipmentModel>
    {
    }

    /// <summary>
    /// 设备基础信息 仓储层
    /// </summary>
    public class EquipmentRepository : BpmRepositoryBase<EquipmentModel>, IEquipmentRepository
    {
    }

    /// <summary>
    ///
    /// </summary>
    public interface IEquipmentCheckRepository : IRepository<EquipmentCheckModel> { }

    /// <summary>
    ///设备校验 仓储层
    /// </summary>
    public class EquipmentCheckRepository : BpmRepositoryBase<EquipmentCheckModel>, IEquipmentCheckRepository
    { }
}