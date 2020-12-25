using System;

#nullable enable

namespace Model.Town.Building
{
    public readonly struct BuildingData
    {
        public Guid Id { get; }
        public string Name { get; }

        public BuildingData(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}