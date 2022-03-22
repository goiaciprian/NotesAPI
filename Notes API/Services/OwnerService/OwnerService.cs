using Notes_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes_API.Services.OwnerService
{
    public class OwnerService : IOwnerService
    {
        private static List<Owner> _owners = new List<Owner>()
        {
            new Owner() { Id = new Guid("F7C707CC-BBDE-42D5-ABC0-8CD6FC6A09EF"), Name = "Owner 1"},
            new Owner() { Id = Guid.NewGuid(), Name = "Owner 2"},
            new Owner() { Id = Guid.NewGuid(), Name = "Owner 3"}
        };

        public Owner Create(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            _owners.Add(owner);
            return owner;
        }

        public Owner Delete(Guid Id)
        {
            var owner = _owners.First(o => o.Id == Id);
            if (owner == null)
                return null;
            _owners.Remove(owner);
            return owner;
        }

        public Owner Get(Guid Id)
        {
            return _owners.First(o => o.Id == Id);
        }

        public List<Owner> GetAll()
        {
            return _owners;
        }

        public Owner Update(Guid Id, Owner owner)
        {
            var ownerIndex = _owners.FindIndex(o => o.Id == Id);
            if (ownerIndex == -1)
                return null;
            owner.Id = Id;
            _owners[ownerIndex] = owner;
            return owner;
        }
    }
}
