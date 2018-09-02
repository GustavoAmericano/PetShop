using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService.Impl
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepo)
        {
            _ownerRepository = ownerRepo;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _ownerRepository.GetAllOwners();
        }

        public Owner CreateOwner(Owner owner)
        {
            if (!owner.Email.Contains("@"))
            {
                throw new Exception("Not a valid email.");
            }
            else
            {
                return _ownerRepository.CreateOwner(owner);
            }

        }

        public Owner GetOwnerById(int id)
        {
            Owner o = _ownerRepository.GetOwnerById(id);
            // Check for null etc here..
            return o;
        }
    }
}