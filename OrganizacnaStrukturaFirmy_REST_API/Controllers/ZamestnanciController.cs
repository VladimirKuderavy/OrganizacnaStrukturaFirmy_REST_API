using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizacnaStrukturaFirmy_REST_API.Data;
using OrganizacnaStrukturaFirmy_REST_API.Models;

namespace OrganizacnaStrukturaFirmy_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZamestnanciController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ZamestnanciController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var zamestnanci = dbContext.zamestnanci.ToList();

                if (zamestnanci.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nenasiel sa ziadny zamestnanec!");
                }

                return Ok(zamestnanci);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            try
            {
                var najdenyZamestnanec = dbContext.zamestnanci.FirstOrDefault(z => z.id == id);

                if (najdenyZamestnanec == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Hladany zamestnanec sa nenasiel!");
                }

                return Ok(najdenyZamestnanec);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Zamestnanec zamestnanec)
        {
            Zamestnanec novyZamestnanec = new Zamestnanec();

            novyZamestnanec.id = zamestnanec.id;
            novyZamestnanec.titul = zamestnanec.titul;
            novyZamestnanec.meno = zamestnanec.meno;
            novyZamestnanec.priezvisko = zamestnanec.priezvisko;
            novyZamestnanec.telefon = zamestnanec.telefon;
            novyZamestnanec.email = zamestnanec.email;

            try
            {
                dbContext.zamestnanci.Add(novyZamestnanec);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Zamestnanec zamestnanec)
        {
            try
            {
                var najdenyZamestnanec = dbContext.zamestnanci.FirstOrDefault(z => z.id == zamestnanec.id);

                if (najdenyZamestnanec == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Zamestnanec sa nenasiel!");
                }

                najdenyZamestnanec.titul = zamestnanec.titul;
                najdenyZamestnanec.meno = zamestnanec.meno;
                najdenyZamestnanec.priezvisko = zamestnanec.priezvisko;
                najdenyZamestnanec.telefon = zamestnanec.telefon;
                najdenyZamestnanec.email = zamestnanec.email;

                dbContext.Entry(najdenyZamestnanec).State = EntityState.Modified;
                dbContext.SaveChanges();
  
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var najdenyZamestnanec = dbContext.zamestnanci.FirstOrDefault(z => z.id == id);

                if (najdenyZamestnanec == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Zamestnanec sa nenasiel!");
                }

                dbContext.Entry(najdenyZamestnanec).State = EntityState.Deleted;
                dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }
        }
    }
}
