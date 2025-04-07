using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Tarea2p2.Models;

public class NotasController : ApiController
{
    // Lista estática para almacenar notas en memoria
    private static List<Nota> notas = new List<Nota>();
    private static int nextId = 1;

    // GET /api/notas
    public IHttpActionResult Get()
    {
        return Ok(notas); // Devuelve todas las notas con una respuesta 200 OK
    }

    // GET /api/notas/{id}
    public IHttpActionResult Get(int id)
    {
        var nota = notas.FirstOrDefault(n => n.Id == id);
        if (nota == null)
        {
            return NotFound(); // 404 si no se encuentra la nota
        }
        return Ok(nota); // 200 OK si se encuentra la nota
    }

    // POST /api/notas
    public IHttpActionResult Post([FromBody] Nota nuevaNota)
    {
        if (nuevaNota == null || string.IsNullOrEmpty(nuevaNota.Titulo))
        {
            return BadRequest("El título de la nota es obligatorio."); // 400 si el título está vacío o nulo
        }

        // Genera un nuevo ID para la nota y la agrega a la lista
        nuevaNota.Id = nextId++;
        notas.Add(nuevaNota);
        return CreatedAtRoute("DefaultApi", new { id = nuevaNota.Id }, nuevaNota); // 201 Created si se crea la nota
    }

    // PUT /api/notas/{id}
    public IHttpActionResult Put(int id, [FromBody] Nota notaActualizada)
    {
        if (notaActualizada == null || string.IsNullOrEmpty(notaActualizada.Titulo))
        {
            return BadRequest("El título de la nota es obligatorio."); // 400 si el título está vacío o nulo
        }

        var notaExistente = notas.FirstOrDefault(n => n.Id == id);
        if (notaExistente == null)
        {
            return NotFound(); // 404 si no se encuentra la nota
        }

        // Actualiza los campos de la nota
        notaExistente.Titulo = notaActualizada.Titulo;
        notaExistente.Contenido = notaActualizada.Contenido;
        return Ok(notaExistente); // 200 OK si la nota es actualizada exitosamente
    }

    // DELETE /api/notas/{id}
    public IHttpActionResult Delete(int id)
    {
        var nota = notas.FirstOrDefault(n => n.Id == id);
        if (nota == null)
        {
            return NotFound(); // 404 si no se encuentra la nota
        }

        // Elimina la nota de la lista
        notas.Remove(nota);
        return StatusCode(System.Net.HttpStatusCode.NoContent); // 204 No Content si la eliminación fue exitosa
    }
}
