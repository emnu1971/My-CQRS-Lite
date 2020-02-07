using System;
using System.Collections.Generic;
using CQRSCode.ReadModel.Dtos;
using CQRSlite.Queries;

namespace CQRSCode.ReadModel.Queries
{
    public class GetInventoryItems : IQuery<List<InventoryItemListDto>>
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
