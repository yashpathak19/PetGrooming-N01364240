using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        // List method
        public ActionResult List()
        {
            //TO DO: implement search
            // selecting the all species available in db
            Debug.WriteLine("Running the select query from Species/List");
            List<Species>myspecies = db.Species.SqlQuery("Select * from species").ToList();
            // returning the view
            return View(myspecies);
        }

        // Add method
        // Address: /Species/Add
        [HttpPost]
        public ActionResult Add(string SpeciesName)
        {
            //To DO: validate the datatype

            string query = "insert into species (Name) values (@SpeciesName)";
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@SpeciesName", SpeciesName);

            Debug.WriteLine("Running the insert query from Species/Add", query);
            //running the insert query
            db.Database.ExecuteSqlCommand(query, sqlparams);


            // returning the view of Species List
            return RedirectToAction("List");
        }


        public ActionResult Add()
        {
            // this method is executed before the form is submitted
            //selecting all the species available in the db and converting that into list
            List<Species> species = db.Species.SqlQuery("select * from Species").ToList();

            Debug.WriteLine("Running the select query from Species/Add");
            //returning the view
            return View(species);
        }

        // Update method
        public ActionResult Update(int id)
        {
            // this method will fetch the data of the species that we need to update
            string query = "select * from species where speciesid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            // running the select query
            Debug.WriteLine("Running the select query from Species/Update", query);
            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();
            // returning the view of selected species
            return View(selectedspecies);
        }
        // [HttpPost] Update
        // Address: Species/Update/(id)
        [HttpPost]
        public ActionResult Update(int id, string speciesName)
        {
            // this method will update the species
            // To DO: validate before updating
            string query = "update species set Name=@SpeciesName where speciesid = @id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@SpeciesName", speciesName);
            sqlparams[1] = new SqlParameter("@id", id);
            //running query to update
            Debug.WriteLine("Running from the Species/Update, Query:", query);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //returning the view
            return RedirectToAction("List");
        }
        // delete
        public ActionResult Delete(int id)
        {
            //TO DO: this should also delete the associated pets while deleting the species
            string query = "delete from species where speciesid=@id";
            SqlParameter sqlparam = new SqlParameter("@id",id);
            Debug.WriteLine("Running the delete query, Query:", query);
            db.Database.ExecuteSqlCommand(query, sqlparam);
            return RedirectToAction("List");
        }

        //Species/Show/(id)
        public ActionResult Show(int? id)
        {
            //TO DO: also show the associated pets in species in a tabular form
            //returning bad request error when the id is null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //getting the species
            Debug.WriteLine("Running the Species/Show");
            Species species = db.Species.SqlQuery("select * from species where speciesid=@SpeciesID", new SqlParameter("@SpeciesID", id)).FirstOrDefault();
            if (species == null)
            {
                // if species is not found then returning the httpnotfound error
                return HttpNotFound();
            }
            //returning view of species
            return View(species);
        }
    }
}