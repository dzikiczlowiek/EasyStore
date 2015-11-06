namespace EasyStore.Tests.Common.Arrangement.DummyDomain.Order
{
    using System;

    using EasyStore.CommonDomain;

    [Serializable]
    public class SetShipmentDateEvent : IDomainEvent
    {
        public SetShipmentDateEvent(DateTime shipmentDate)
        {
            this.ShipmentDate = shipmentDate;
        }

        public DateTime ShipmentDate { get; private set; }
    }
}
