﻿using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManager;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.BoardManager
{
    /// <summary>
    ///物料规格看板
    /// </summary>
    public interface IMaterialSpecBoardRepository : IRepository<MaterialSpecBoardModel> { }
    /// <summary>
    ///物料规格看板
    /// </summary>
    public class MaterialSpecBoardRepository : BpmRepositoryBase<MaterialSpecBoardModel>, IMaterialSpecBoardRepository
    { }

}
