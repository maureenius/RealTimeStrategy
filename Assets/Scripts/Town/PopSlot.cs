using System;
using Assets.Scripts.Town.Building;
using Assets.Scripts.Town.Terrain;
using ControllerInfo;

namespace Assets.Scripts.Town
{
    public class PopSlot : INamed
    {
        public Guid Id { get; }
        public string Name { get; }
        public string TypeName { get; }
        public IWorkable Workable { get; }
        public Pop Pop { get; private set; }


        public PopSlot()
        {
            Id = Guid.NewGuid();
            Name = "無職";
            TypeName = "無職";
        }
        public PopSlot(IWorkable workable)
        {
            Workable = workable;
            Id = Guid.NewGuid();
            Name = Workable.Name;
            TypeName = Workable.TypeName;
        }

        public void PutPop(Pop targetPop)
        {
            if (Pop != null) throw new InvalidOperationException();
            Pop = targetPop;
        }

        public void RemovePop()
        {
            Pop = null;
        }

        public PopSlotInfo ToInfo()
        {
            return Pop == null ? 
                new PopSlotInfo(Id, Name, TypeName) : 
                new PopSlotInfo(Id, Name, TypeName, 
                    Pop.Id, Pop.Name, Pop.TypeName);
        }
    }
}