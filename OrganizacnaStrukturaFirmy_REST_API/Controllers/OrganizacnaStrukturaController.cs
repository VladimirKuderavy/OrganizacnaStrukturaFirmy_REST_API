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
    public class OrganizacnaStrukturaController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public OrganizacnaStrukturaController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var uzlyOrganizacnejStruktury = dbContext.organizacna_struktura.ToList();

                if (uzlyOrganizacnejStruktury.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nenasiel sa ziadny uzol organizacnej struktury!");
                }

                return Ok(uzlyOrganizacnejStruktury);
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
                var najdenyUzol = dbContext.organizacna_struktura.FirstOrDefault(u => u.id == id);

                if (najdenyUzol == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Hladany uzol sa nenasiel!");
                }

                return Ok(najdenyUzol);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }
        }

        [HttpGet("GetVsetkyUzlyPodlaUrovne/{kodUrovne}")]
        public IActionResult GetVsetkyUzlyPodlaUrovne(int kodUrovne)
        {
            try
            {
                if (kodUrovne < 0 || kodUrovne > 4)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Kod urovne je mimo pozadovaneho rozsahu!");
                }

                IQueryable<UzolOrganizacnejStruktury> query = dbContext.organizacna_struktura.Where(u => u.kod_urovne == kodUrovne);
                var uzly = query.ToList();

                if (uzly.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ziadny uzol sa nenachadza v zadanej urovni!");
                }

                return Ok(uzly);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }
        }

        [HttpGet("GetVsetkyNizsieUzly/{id}")]
        public IActionResult GetVsetkyNizsieUzly(int id)
        {
            try
            {
                var najdenyUzol = dbContext.organizacna_struktura.FirstOrDefault(u => u.id == id);

                if (najdenyUzol == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Zadany uzol neexistuje!");
                }

                var query = dbContext.organizacna_struktura;
                var nizsieUzly = query.FromSqlRaw(@"
                    WITH R AS(SELECT os.id, os.kod_urovne, os.nazov, os.veduci, os.vyssi_uzol FROM organizacna_struktura os WHERE os.id = {0}
                        UNION ALL
                        SELECT os2.id, os2.kod_urovne, os2.nazov, os2.veduci, os2.vyssi_uzol FROM organizacna_struktura os2 JOIN R r ON r.id = os2.vyssi_uzol
                    )
                    SELECT * FROM R WHERE id != {1};", id, id).ToList();

                if (nizsieUzly.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Zadany uzol nema ziadne nizsie uzly!");
                }

                return Ok(nizsieUzly);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] UzolOrganizacnejStruktury uzol)
        {
            if(string.IsNullOrEmpty(uzol.nazov))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            UzolOrganizacnejStruktury novyUzol = new UzolOrganizacnejStruktury();

            novyUzol.id = uzol.id;
            novyUzol.kod_urovne = uzol.kod_urovne;
            novyUzol.nazov = uzol.nazov;
            novyUzol.veduci = uzol.veduci;
            novyUzol.vyssi_uzol = uzol.vyssi_uzol;

            try
            {
                dbContext.organizacna_struktura.Add(novyUzol);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UzolOrganizacnejStruktury uzol)
        {
            try
            {
                if (string.IsNullOrEmpty(uzol.nazov))
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                var najdenyUzol = dbContext.organizacna_struktura.FirstOrDefault(u => u.id == uzol.id);

                if (najdenyUzol == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Uzol sa nenasiel!");
                }

                najdenyUzol.kod_urovne = uzol.kod_urovne;
                najdenyUzol.nazov = uzol.nazov;
                najdenyUzol.veduci = uzol.veduci;
                najdenyUzol.vyssi_uzol = uzol.vyssi_uzol;

                dbContext.Entry(najdenyUzol).State = EntityState.Modified;
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
                var najdenyUzol = dbContext.organizacna_struktura.FirstOrDefault(u => u.id == id);

                if (najdenyUzol == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Zadany uzol neexistuje!");
                }

                string query = @"
                    WITH R AS(SELECT os.id, os.kod_urovne, os.nazov, os.veduci, os.vyssi_uzol FROM organizacna_struktura os WHERE os.id = {0}
                        UNION ALL
                        SELECT os2.id, os2.kod_urovne, os2.nazov, os2.veduci, os2.vyssi_uzol FROM organizacna_struktura os2 JOIN R r ON r.id = os2.vyssi_uzol
                    )
                    DELETE FROM organizacna_struktura WHERE id IN (SELECT id FROM R);";

                dbContext.Database.ExecuteSqlRaw(query, id);

                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Nastala chyba servera!");
            }
        }


    }
}
