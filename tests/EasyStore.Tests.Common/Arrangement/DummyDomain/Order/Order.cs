namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Order
{
    using System;

    using EasyStore.CommonDomain;

    public class Order : AggregateRoot
    {
        private Order()
            : base(null)
        {
        }

        public Order(Guid aggregateId, int orderNumber)
            : base(aggregateId)
        {
            this.RaiseEvent(new ChangedOrderNumberEvent(orderNumber));
        }

        public int OrderNumber { get; private set; }

        public DateTime ShipmentDate { get; private set; }

        public void SetShipmentDate(DateTime shipmentDate)
        {
            this.RaiseEvent(new SetShipmentDateEvent(shipmentDate));
        }

        public void Apply(SetShipmentDateEvent @event)
        {
            this.ShipmentDate = @event.ShipmentDate;
        }

        public void Apply(ChangedOrderNumberEvent @event)
        {
            this.OrderNumber = @event.OrderNumber;
        }
    }
}
