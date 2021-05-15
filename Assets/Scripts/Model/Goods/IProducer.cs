using System.Collections.Generic;

# nullable enable

namespace Model.Goods
{
    public interface IProducer
    {
        IEnumerable<Cargo> Produce();
    }
}