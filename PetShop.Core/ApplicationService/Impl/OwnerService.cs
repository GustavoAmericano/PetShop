﻿using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.DomainService;
using PetShop.Core.Entities;

namespace PetShop.Core.ApplicationService.Impl
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private IPetRepository _petRepository;

        public OwnerService(IOwnerRepository ownerRepo,
            IPetRepository petRepository)
        {
            _ownerRepository = ownerRepo;
            _petRepository = petRepository;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _ownerRepository.GetAllOwners();
        }

        public Owner CreateOwner(Owner owner)
        {
            //if (!owner.Email.Contains("@"))
            //{
            //    throw new Exception("Not a valid email.");
            //}
            //else
            {
                return _ownerRepository.CreateOwner(owner);
            }

        }


        public Owner GetOwnerById(int id)
        {
            Owner o = _ownerRepository.GetOwnerById(id);
            if (o == null) return null;
            return o;
        }

        public void SaveOwner(int id, Owner owner)
        {
            _ownerRepository.SaveOwner(id, owner);
        }

        public void DeleteOwner(int id)
        {
            _ownerRepository.DeleteOwner(id);
        }
    }
}