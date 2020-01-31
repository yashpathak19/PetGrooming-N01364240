using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGrooming.Models.ViewModels
{
    public class UpdatePet
    {
        //information need to make update pet work
        //info about one pet
        //info about many species

        public Pet pet { get; set; }
        public List<Species> species { get; set; }
    }
}