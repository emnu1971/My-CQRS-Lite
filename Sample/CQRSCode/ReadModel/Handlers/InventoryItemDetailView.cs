﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRSCode.ReadModel.Dtos;
using CQRSCode.ReadModel.Events;
using CQRSCode.ReadModel.Infrastructure;
using CQRSCode.ReadModel.Queries;
using CQRSlite.Events;
using CQRSlite.Queries;

namespace CQRSCode.ReadModel.Handlers
{
    public class InventoryItemDetailView : ICancellableEventHandler<InventoryItemCreated>,
        ICancellableEventHandler<InventoryItemDeactivated>,
        ICancellableEventHandler<InventoryItemRenamed>,
        ICancellableEventHandler<ItemsRemovedFromInventory>,
        ICancellableEventHandler<ItemsCheckedInToInventory>,
        ICancellableQueryHandler<GetInventoryItemDetails, InventoryItemDetailsDto>
    {
        public Task HandleAsync(InventoryItemCreated message, CancellationToken token)
        {
            InMemoryDatabase.Details.Add(message.Id,
                new InventoryItemDetailsDto(message.Id, message.Name, 0, message.Version));
            return Task.CompletedTask;
        }

        public Task HandleAsync(InventoryItemRenamed message, CancellationToken token)
        {
            var d = GetDetailsItem(message.Id);
            d.Name = message.NewName;
            d.Version = message.Version;
            return Task.CompletedTask;
        }

        private static InventoryItemDetailsDto GetDetailsItem(Guid id)
        {
            if (!InMemoryDatabase.Details.TryGetValue(id, out var dto))
            {
                throw new InvalidOperationException("did not find the original inventory this shouldnt happen");
            }
            return dto;
        }

        public Task HandleAsync(ItemsRemovedFromInventory message, CancellationToken token)
        {
            var dto = GetDetailsItem(message.Id);
            dto.CurrentCount -= message.Count;
            dto.Version = message.Version;
            return Task.CompletedTask;
        }

        public Task HandleAsync(ItemsCheckedInToInventory message, CancellationToken token)
        {
            var dto = GetDetailsItem(message.Id);
            dto.CurrentCount += message.Count;
            dto.Version = message.Version;
            return Task.CompletedTask;
        }

        public Task HandleAsync(InventoryItemDeactivated message, CancellationToken token)
        {
            InMemoryDatabase.Details.Remove(message.Id);
            return Task.CompletedTask;
        }

        public Task<InventoryItemDetailsDto> HandleAsync(GetInventoryItemDetails message, CancellationToken token = default(CancellationToken))
        {
            return Task.FromResult(InMemoryDatabase.Details.SingleOrDefault(x => x.Key == message.Id).Value);
        }
    }
}
